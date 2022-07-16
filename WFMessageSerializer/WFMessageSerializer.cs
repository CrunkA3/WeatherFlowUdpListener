using System.Reflection;
using System.Text;
using System.Text.Json;

namespace WeatherFlowUdpListener;

public static class WFMessageSerializer
{
    private static Dictionary<string, Type> MessageTypes = new Dictionary<string, Type> {
        { typeof(WFRainStartMessage).GetCustomAttributes().OfType<WFMessageTypeAttribute>().First().MessageType, typeof(WFRainStartMessage) },
        { typeof(WFLightningStrikeMessage).GetCustomAttributes().OfType<WFMessageTypeAttribute>().First().MessageType, typeof(WFLightningStrikeMessage) },
        { typeof(WFRapidWindMessage).GetCustomAttributes().OfType<WFMessageTypeAttribute>().First().MessageType, typeof(WFRapidWindMessage) },
        { typeof(WFObservationAirMessage).GetCustomAttributes().OfType<WFMessageTypeAttribute>().First().MessageType, typeof(WFObservationAirMessage) },
        { typeof(WFObservationSkyMessage).GetCustomAttributes().OfType<WFMessageTypeAttribute>().First().MessageType, typeof(WFObservationSkyMessage) },
        { typeof(WFObservationTempestMessage).GetCustomAttributes().OfType<WFMessageTypeAttribute>().First().MessageType, typeof(WFObservationTempestMessage) },
        { typeof(WFStatusDeviceMessage).GetCustomAttributes().OfType<WFMessageTypeAttribute>().First().MessageType, typeof(WFStatusDeviceMessage) },
        { typeof(WFStatusHubMessage).GetCustomAttributes().OfType<WFMessageTypeAttribute>().First().MessageType, typeof(WFStatusHubMessage) },
    };


    public static WFMessage? Deserialize(string json)
    {
        using JsonDocument? jsonDoc = JsonDocument.Parse(json);
        JsonElement messageTypeElement = jsonDoc.RootElement.GetProperty("type");

        var messageType = messageTypeElement.GetString();
        if (string.IsNullOrEmpty(messageType)) throw new Exception("Message Type is empty");

        if (!MessageTypes.TryGetValue(messageType, out Type? type))
        {
            var sb = new StringBuilder();
            sb.Append("Type '");
            sb.Append(messageType);
            sb.Append("' is unkown");
            throw new JsonException(sb.ToString());
        }

        return (WFMessage?)jsonDoc.RootElement.Deserialize(type!);
    }
}