namespace WeatherFlowUdpListener.Tests;

public class Tests
{
    WFListener? WFListener;

    [SetUp]
    public void Setup()
    {
        WFListener = new WFListener();
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

        await WFListener.ListenAsync(cancellationToken);
    }
}