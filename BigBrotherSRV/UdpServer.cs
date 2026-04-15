using System.Net;
using System.Net.Sockets;
using System.Text;

public class UdpServer
{
    private readonly UdpClient udp;
    private readonly IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);

    public UdpServer()
    {
        udp = new UdpClient(6768);
    }

    public async Task StartAsync()
    {
        while (true)
        {
            var result = await udp.ReceiveAsync();
            string msg = Encoding.UTF8.GetString(result.Buffer);

            if (msg == "DISCOVER_SERVER")
            {
                string response = "SERVER:10.10.10.114:6767";
                byte[] data = Encoding.UTF8.GetBytes(response);

                await udp.SendAsync(data, data.Length, result.RemoteEndPoint);
            }
        }
    }
}