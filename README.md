# WeatherFlowUdpListener
Listen to WeatherFlow Tempest UDP Port

## Example usage

```c#
await WeatherFlowUdpListener.WFListener
    .Create()
    .OnReceiveObservationAirMessage(msg =>
    {
        Console.WriteLine($"Air-Temperature: {msg.AirTemperature}");
    })
    .OnReceiveObservationSkyMessage(msg =>
    {
        Console.WriteLine($"UV-Index: {msg.UV}");
    })
    .OnReceiveRapidWindMessage(msg =>
    {
        Console.WriteLine($"Wind-Speed: {msg.WindSpeed}");
    })
    .ListenAsync(cancellationToken);
```
