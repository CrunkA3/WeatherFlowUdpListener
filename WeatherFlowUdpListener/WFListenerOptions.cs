using System.Net;
using System.Net.Sockets;

public class WFListenerOptions
{
    const int listenPort = 50222;
    public UdpClient Client { get; set; } = new UdpClient(listenPort);
    public IPEndPoint EndPoint { get; set; } = new IPEndPoint(IPAddress.Any, listenPort);
}