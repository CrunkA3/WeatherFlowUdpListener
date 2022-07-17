namespace WeatherFlowUdpListener;

[WFMessageType("evt_strike")]
public class WFLightningStrikeMessage : WFEventMessage
{


    /// Distance in KM
    public int? Distance => Evt?[1];


    public int? Energy => Evt?[0];
}