namespace WeatherFlowUdpListener;


[WFMessageType("obs_st")]
public class WFObservationTempestMessage : WFObservationMessage
{

    /// <summary>
    /// Wind Lull (minimum 3 second sample) in m/s
    /// </summary>
    public double? WindLull => Obs?.ElementAt(0).ElementAt(1);

    /// <summary>
    /// Wind Avg (average over report interval) in m/s
    /// </summary>
    public double? WindAvg => Obs?.ElementAt(0).ElementAt(2);

    /// <summary>
    /// Wind Gust (maximum 3 second sample) in m/s
    /// </summary>
    public double? WindGust => Obs?.ElementAt(0).ElementAt(3);

    /// <summary>
    /// Wind Direction in Degrees
    /// </summary>
    public int? WindDirection => (int?)(Obs?.ElementAt(0).ElementAt(4));
    
    /// <summary>
    /// Wind Sample Interval in seconds
    /// </summary>
    public int? WindSampleInterval => (int?)(Obs?.ElementAt(0).ElementAt(5));

    /// <summary>
    /// Station Pressure in MB
    /// </summary>
    public double? StationPressure => Obs?.ElementAt(0).ElementAt(6);

    /// <summary>
    /// Air Temperature in C
    /// </summary>
    public double? AirTemperature => Obs?.ElementAt(0).ElementAt(7);


    /// <summary>
    /// Relative Humidity in %
    /// </summary>
    public int? RelativeHumidity => (int?)(Obs?.ElementAt(0).ElementAt(8));



    /// <summary>
    /// Illuminance in Lux
    /// </summary>
    public int? Illuminance => (int?)(Obs?.ElementAt(0).ElementAt(9));

    /// <summary>
    /// UV Index
    /// </summary>
    public int? UV => (int?)(Obs?.ElementAt(0).ElementAt(10));

    /// <summary>
    /// Solar Radiation in W/m²
    /// </summary>
    public double? SolarRadiation => Obs?.ElementAt(0).ElementAt(11);

    /// <summary>
    /// Rain amount over previous minute in mm
    /// </summary>
    public double? RainAmountPreviosMinute => Obs?.ElementAt(0).ElementAt(12);   

    /// <summary>
    /// PercipationType
    /// </summary>
    public PercipationType? PercipationType => (PercipationType?)(Obs?.ElementAt(0).ElementAt(13));

    /// <summary>
    /// Lightning Strike Avg Distance in KM
    /// </summary>
    public int? LightningStrikeAvgDistance => (int?)(Obs?.ElementAt(0).ElementAt(14));

    /// <summary>
    /// Lightning Strike Count
    /// </summary>
    public int? LightningStrikeCount => (int?)(Obs?.ElementAt(0).ElementAt(15));

    /// <summary>
    /// Battery in Volts
    /// </summary>
    public double? Battery => Obs?.ElementAt(0).ElementAt(16);

    /// <summary>
    /// Report Interval in Minutes
    /// </summary>
    public int? ReportInterval => (int?)(Obs?.ElementAt(0).ElementAt(17));

}