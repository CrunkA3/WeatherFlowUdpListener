using System.Net;
using System.Net.Sockets;

namespace WeatherFlowUdpListener.Tests;

public class Tests
{
    WFListener? WFListener;
    UdpClient? udpClient;


    [SetUp]
    public void Setup()
    {
        WFListener = WFListener.Create(() => {
            udpClient = new UdpClient(WFListener.listenPort);
            return udpClient;
        });
    }


    [Test]
    public async Task TestRainStartMessageAsync()
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        Assert.IsNotNull(WFListener);
        WFListener!.OnReceiveRainStartMessage(m =>
        {
            Assert.That(m.Type, Is.EqualTo("evt_precip"));
            cancellationTokenSource.Cancel();
        });

        var listenTask = WFListener.ListenAsync(cancellationToken);

        var sendData = System.Text.Encoding.ASCII.GetBytes("{\"serial_number\":\"SK-00008453\",\"type\":\"evt_precip\",\"hub_sn\":\"HB-00000001\",\"evt\":[1493322445]}");
        await udpClient!.SendAsync(sendData, new IPEndPoint(IPAddress.Loopback, WFListener.listenPort));

        await listenTask;
    }


    [Test]
    public async Task TestLightningStrikeMessageAsync()
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        Assert.IsNotNull(WFListener);
        WFListener!.OnReceiveLightningStrikeMessage(m =>
        {
            Assert.That(m.Type, Is.EqualTo("evt_strike"));
            cancellationTokenSource.Cancel();
        });

        var listenTask = WFListener.ListenAsync(cancellationToken);

        var sendData = System.Text.Encoding.ASCII.GetBytes("{\"serial_number\":\"AR-00004049\",\"type\":\"evt_strike\",\"hub_sn\":\"HB-00000001\",\"evt\":[1493322445,27,3848]}");
        await udpClient!.SendAsync(sendData, new IPEndPoint(IPAddress.Loopback, WFListener.listenPort));

        await listenTask;
    }
}