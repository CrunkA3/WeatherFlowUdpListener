using System.Text.Json.Serialization;


namespace WeatherFlowUdpListener;

[WFMessageType("hub_status")]
public class WFStatusHubMessage : WFStatusMessage
{
    [JsonPropertyName("firmware_revision")]
    public string? FirmwareRevision { get; set; }

    /// <summary>
    /// Reset Flags
    /// </summary>
    [JsonPropertyName("reset_flags")]
    public string? ResetFlags { get; set; }

    /// <summary>
    /// Seq? maybe sequence
    /// </summary>
    [JsonPropertyName("seq")]
    public int? Seq { get; set; }



    /// <summary>
    /// Radio Stats
    /// </summary>
    [JsonPropertyName("radio_stats")]
    public int[]? RadioStats { get; set; }


    public int? Version => RadioStats?[0];
    public int? RebootCount => RadioStats?[1];
    public int? I2CBusErrorCount => RadioStats?[2];
    public RadioStatus? RadioStatus => (RadioStatus?)RadioStats?[3];
    public int? RadioNetworkId => RadioStats?[4];
}