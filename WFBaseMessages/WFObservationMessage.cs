
using System.Text.Json.Serialization;

public abstract class WFObservationMessage : WFMessage
{
    private static DateTime UnixStartDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);



    /// <summary>
    /// Observation Data
    /// </summary>
    [JsonPropertyName("obs")]
    public IEnumerable<IEnumerable<double?>>? Obs { get; set; }

    /// <summary>
    /// Firmware Revision
    /// </summary>
    [JsonPropertyName("firmware_revision")]
    public int FirmwareRevision { get; set; }


    /// <summary>
    /// Time Epoch in Seconds
    /// </summary>
    public int? TimeEpoche => ((int?)Obs?.ElementAt(0).ElementAt(0));



    /// <summary>
    /// Event Time (UTC)
    /// </summary>
    public DateTime? Time => TimeEpoche.HasValue ? UnixStartDate.AddSeconds(TimeEpoche!.Value) : default;
}