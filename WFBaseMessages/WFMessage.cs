
using System.Text.Json.Serialization;

public class WFMessage {

    [JsonPropertyName("serial_number")]
    public string? SerialNumber {get; set;}


    [JsonPropertyName("type")]
    public string? Type { get; set; }

    
    [JsonPropertyName("hub_sn")]
    public string? HubSerialNumber {get; set;}
}