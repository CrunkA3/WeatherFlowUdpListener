using System.Text.Json.Serialization;


namespace WeatherFlowUdpListener;

[WFMessageType("device_status")]
public class WFStatusDeviceMessage : WFStatusMessage
{    [JsonPropertyName("firmware_revision")]
    public int? FirmwareRevision { get; set; }

    /// <summary>
    /// Voltage
    /// </summary>
    [JsonPropertyName("voltage")]
    public double? Voltage { get; set; }

    /// <summary>
    /// Hub RSSI
    /// </summary>
    [JsonPropertyName("hub_rssi")]
    public int? HubRSSI { get; set; }

    /// <summary>
    /// Sensor status
    /// </summary>
    [JsonPropertyName("sensor_status")]
    public int? SensorStatus { get; set; }

    /// <summary>
    /// Debug enabled
    /// </summary>
    [JsonPropertyName("debug")]
    public bool? Debug { get; set; }


}