using System.Text.Json.Serialization;

public class WFEventMessage : WFMessage
{
    private static DateTime UnixStartDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


    [JsonPropertyName("evt")]

    public int[]? Evt { get; set; }

    /// <summary>
    /// Time Epoch in Seconds
    /// </summary>
    public int? TimeEpoche => Evt?[0];


    /// <summary>
    /// Event Time (UTC)
    /// </summary>
    public DateTime? Time => TimeEpoche.HasValue ? UnixStartDate.AddSeconds(TimeEpoche!.Value) : default;
}