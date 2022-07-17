using System.Text.Json.Serialization;


namespace WeatherFlowUdpListener;

[WFMessageType("rapid_wind")]
public class WFRapidWindMessage : WFMessage
{
    private static DateTime UnixStartDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    [JsonPropertyName("ob")]
    public double[]? Ob { get; set; }



    /// Time Epoch in Seconds
    public double? TimeEpoche => Ob?[0];

    /// <summary>
    ///     Wind Speed in mps
    /// </summary>
    public double? WindSpeed => Ob?[1];

    /// <summary>
    /// WindDirection in Degrees
    /// </summary>
    public int? WindDirection => (int?)Ob?[2];

    /// <summary>
    /// Event Time (UTC)
    /// </summary>
    public DateTime? Time => TimeEpoche.HasValue ? UnixStartDate.AddSeconds(TimeEpoche!.Value) : default;
}