using System.Net.Sockets;

public class FakeUdpClient : UdpClient
{
    private byte[]? bytesToSend;
    public void SendFakeData(string data)
    {
        bytesToSend = System.Text.Encoding.ASCII.GetBytes(data);
    }

    public new async Task<byte[]> ReceiveAsync(CancellationToken cancellationToken)
    {
        var waitForDataTask = Task.Run(() =>
        {
            while (bytesToSend == null)
            {
                Thread.Sleep(100);
            }
        }
        );

        await waitForDataTask;

        byte[] bytes = new byte[] { };
        bytesToSend!.CopyTo(bytes, 0);
        bytesToSend = null;
        return bytes;
    }

}