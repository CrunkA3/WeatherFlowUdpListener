using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WeatherFlowUdpListener;
public class WFListener
{
    const int listenPort = 50222;

    private Action<WFMessage>? onReceiveMessage;
    private Action<WFLightningStrikeMessage>? onReceiveLightningStrikeMessage;
    private Action<WFObservationAirMessage>? onReceiveObservationAirMessage;
    private Action<WFObservationSkyMessage>? onReceiveObservationSkyMessage;
    private Action<WFObservationTempestMessage>? onReceiveObservationTempestMessage;
    private Action<WFRainStartMessage>? onReceiveRainStartMessage;
    private Action<WFRapidWindMessage>? onReceiveRapidWindMessage;
    private Action<WFStatusDeviceMessage>? onReceiveStatusDeviceMessage;
    private Action<WFStatusHubMessage>? onReceiveStatusHubMessage;

    private UdpClient client = new UdpClient(listenPort);
    private IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

    public WFListener()
    {
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


    private void OnReceive(IAsyncResult ar)
    {
        byte[]? bytes = client.EndReceive(ar, ref groupEP!);
        InvokeReceiveMessage(bytes);
    }

    public void StartListen()
    {
        var receiveResult = client.BeginReceive(new AsyncCallback(OnReceive), null);
    }

    public async Task ListenAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var receiveResult = await client.ReceiveAsync(cancellationToken);
            var bytes = receiveResult.Buffer;

            InvokeReceiveMessage(bytes);
        }
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
