namespace WeatherFlowUdpListener;

[AttributeUsage(AttributeTargets.Class)]
public class WFMessageTypeAttribute : Attribute {
    public string MessageType {get;}

    public WFMessageTypeAttribute(string messageType) {
        MessageType = messageType;
    }
}