using System.Text.Json.Serialization;

public class WFStatusMessage : WFMessage
{
    private static DateTime UnixStartDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


    [JsonPropertyName("uptime")]
    public int? UptimeSeconds { get; set; }

    public TimeSpan? Uptime => UptimeSeconds.HasValue ? TimeSpan.FromSeconds(UptimeSeconds.Value) : default;


    [JsonPropertyName("rssi")]
    public int? RSSI { get; set; }

    /// <summary>
    /// Timestamp in Seconds from 01/01/1970
    /// </summary>
    [JsonPropertyName("timestamp")]
    public int? TimeStampEpoche { get; set; }

    /// <summary>
    /// Timestamp (UTC)
    /// </summary>
    public DateTime? TimeStamp => TimeStampEpoche.HasValue ? UnixStartDate.AddSeconds(TimeStampEpoche.Value) : default;
}