using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WeatherFlowUdpListener;
public class WFListener
{
    public const int listenPort = 50222;

    private Action<WFMessage>? onReceiveMessage;
    private Action<WFLightningStrikeMessage>? onReceiveLightningStrikeMessage;
    private Action<WFObservationAirMessage>? onReceiveObservationAirMessage;
    private Action<WFObservationSkyMessage>? onReceiveObservationSkyMessage;
    private Action<WFObservationTempestMessage>? onReceiveObservationTempestMessage;
    private Action<WFRainStartMessage>? onReceiveRainStartMessage;
    private Action<WFRapidWindMessage>? onReceiveRapidWindMessage;
    private Action<WFStatusDeviceMessage>? onReceiveStatusDeviceMessage;
    private Action<WFStatusHubMessage>? onReceiveStatusHubMessage;

    private readonly Func<UdpClient> clientCreator;
    private UdpClient? client;
    private IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, listenPort);

    private WFListener(Func<UdpClient> clientCreator)
    {
        this.clientCreator = clientCreator;
    }

    public static WFListener Create()
    {
        return new WFListener(() => new UdpClient(listenPort));
    }
    public static WFListener Create(Func<UdpClient> clientCreator)
    {
        return new WFListener(clientCreator);
    }


    public void OnReceiveMessage(Action<WFMessage> action) => onReceiveMessage = action;
    public void OnReceiveLightningStrikeMessage(Action<WFLightningStrikeMessage> action) => onReceiveLightningStrikeMessage = action;
    public void OnReceiveObservationAirMessage(Action<WFObservationAirMessage> action) => onReceiveObservationAirMessage = action;
    public void OnReceiveObservationSkyMessage(Action<WFObservationSkyMessage> action) => onReceiveObservationSkyMessage = action;
    public void OnReceiveObservationTempestMessage(Action<WFObservationTempestMessage> action) => onReceiveObservationTempestMessage = action;
    public void OnReceiveRainStartMessage(Action<WFRainStartMessage> action) => onReceiveRainStartMessage = action;
    public void OnReceiveRapidWindMessage(Action<WFRapidWindMessage> action) => onReceiveRapidWindMessage = action;
    public void OnReceiveStatusDeviceMessage(Action<WFStatusDeviceMessage> action) => onReceiveStatusDeviceMessage = action;
    public void OnReceiveStatusHubMessage(Action<WFStatusHubMessage> action) => onReceiveStatusHubMessage = action;


    public async Task ListenAsync(CancellationToken cancellationToken)
    {
        client = clientCreator.Invoke();
        while (!cancellationToken.IsCancellationRequested)
        {
            var receiveResult = await client.ReceiveAsync(cancellationToken);
            var bytes = receiveResult.Buffer;

            InvokeReceiveMessage(bytes);
        }
        client.Dispose();
        client = null;
    }

    private void InvokeReceiveMessage(byte[] bytes)
    {
        WFMessage? wfMessage = WFMessageSerializer.Deserialize(Encoding.ASCII.GetString(bytes));

        if (wfMessage != null)
        {
            onReceiveMessage?.Invoke(wfMessage);
            Type messageType = wfMessage.GetType();

            if (messageType == typeof(WFLightningStrikeMessage))
                onReceiveLightningStrikeMessage?.Invoke((WFLightningStrikeMessage)wfMessage);

            else if (messageType == typeof(WFRapidWindMessage))
                onReceiveRapidWindMessage?.Invoke((WFRapidWindMessage)wfMessage);

            else if (messageType == typeof(WFObservationAirMessage))
                onReceiveObservationAirMessage?.Invoke((WFObservationAirMessage)wfMessage);

            else if (messageType == typeof(WFObservationSkyMessage))
                onReceiveObservationSkyMessage?.Invoke((WFObservationSkyMessage)wfMessage);

            else if (messageType == typeof(WFObservationTempestMessage))
                onReceiveObservationTempestMessage?.Invoke((WFObservationTempestMessage)wfMessage);

            else if (messageType == typeof(WFRainStartMessage))
                onReceiveRainStartMessage?.Invoke((WFRainStartMessage)wfMessage);

            else if (messageType == typeof(WFRapidWindMessage))
                onReceiveRapidWindMessage?.Invoke((WFRapidWindMessage)wfMessage);

            else if (messageType == typeof(WFStatusDeviceMessage))
                onReceiveStatusDeviceMessage?.Invoke((WFStatusDeviceMessage)wfMessage);

            else if (messageType == typeof(WFStatusHubMessage))
                onReceiveStatusHubMessage?.Invoke((WFStatusHubMessage)wfMessage);
        }
    }
}
