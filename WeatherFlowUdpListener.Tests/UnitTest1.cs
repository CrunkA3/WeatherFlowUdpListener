using System.Net.Sockets;

namespace WeatherFlowUdpListener.Tests;

public class Tests
{
    WFListener? WFListener;
    FakeUdpClient? fakeUdpClient;

    [SetUp]
    public void Setup()
    {
        fakeUdpClient = new FakeUdpClient();
        WFListener = WFListener.Create(options =>
        {
            options.Client = fakeUdpClient;
        });
    }

    [Test]
    public async Task TestGetSomeMessageAsync()
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        Assert.IsNotNull(WFListener);
        WFListener!.OnReceiveMessage(m =>
        {
            Assert.IsNotEmpty(m.Type);
            cancellationTokenSource.Cancel();
        });

        var listenTask = WFListener.ListenAsync(cancellationToken);
        fakeUdpClient!.SendFakeData("{\"serial_number\":\"SK-00008453\",\"type\":\"evt_precip\",\"hub_sn\":\"HB-00000001\",\"evt\":[1493322445]}");

        await listenTask;
    }
}