namespace WeatherFlowUdpListener;

[WFMessageType("obs_air")]
public class WFObservationAirMessage : WFObservationMessage
{
    /// <summary>
    /// Station Pressure in MB
    /// </summary>
    public double? StationPressure => Obs?.ElementAt(0).ElementAt(1);


    /// <summary>
    /// Air Temperature in C
    /// </summary>
    public double? AirTemperature => Obs?.ElementAt(0).ElementAt(2);


    /// <summary>
    /// Relative Humidity in %
    /// </summary>
    public int? RelativeHumidity => (int?)(Obs?.ElementAt(0).ElementAt(3));


    /// <summary>
    /// Lightning Strike Count
    /// </summary>
    public int? LightningStrikeCount => (int?)(Obs?.ElementAt(0).ElementAt(4));


    /// <summary>
    /// Lightning Strike Avg Distance in KM
    /// </summary>
    public int? LightningStrikeAvgDistance => (int?)(Obs?.ElementAt(0).ElementAt(5));



    /// <summary>
    /// Battery in Volts
    /// </summary>
    public double? Battery => Obs?.ElementAt(0).ElementAt(6);

    /// <summary>
    /// Report Interval	in Minutes
    /// </summary>
    public int? ReportInterval => (int?)(Obs?.ElementAt(0).ElementAt(7));
}