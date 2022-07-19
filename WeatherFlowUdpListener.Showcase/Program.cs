

Console.WriteLine("Initialize Listener");

CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
CancellationToken cancellationToken = cancellationTokenSource.Token;

await WeatherFlowUdpListener.WFListener
    .Create()
    .OnReceiveObservationAirMessage(msg =>
    {
        Console.WriteLine($"Air-Temperature: {msg.AirTemperature}");
        cancellationTokenSource.Cancel();
    })
    .OnReceiveObservationSkyMessage(msg =>
    {
        Console.WriteLine($"UV-Index: {msg.UV}");
        cancellationTokenSource.Cancel();
    })
    .OnReceiveRapidWindMessage(msg =>
    {
        Console.WriteLine($"Wind-Speed: {msg.WindSpeed}");
        cancellationTokenSource.Cancel();
    })
    .ListenAsync(cancellationToken);