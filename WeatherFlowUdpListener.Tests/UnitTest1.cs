using System.Net;
using System.Net.Sockets;

namespace WeatherFlowUdpListener.Tests;

public class Tests
{
    WFListener? WFListener;
    WFListenerOptions? udpClientOptions;


    [SetUp]
    public void Setup()
    {
        WFListener = WFListener.Create(options => udpClientOptions = options);
    }

    [Test]
    public async Task TestGetSomeMessageAsync()
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        Assert.IsNotNull(WFListener);
        WFListener!.OnReceiveMessage(m =>
        {
            Assert.AreEqual(m.Type, "evt_precip");
            cancellationTokenSource.Cancel();
        });

        var listenTask = WFListener.ListenAsync(cancellationToken);

        var sendData = System.Text.Encoding.ASCII.GetBytes("{\"serial_number\":\"SK-00008453\",\"type\":\"evt_precip\",\"hub_sn\":\"HB-00000001\",\"evt\":[1493322445]}");
        await udpClientOptions!.Client.SendAsync(sendData, new IPEndPoint(IPAddress.Loopback, WFListenerOptions.listenPort));

        await listenTask;
    }
}