using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class UdpServer
{
    public static async Task Main()
    {
        using var udp = new UdpClient(9999);

        Console.WriteLine("Serwer nasłuchuje...");

        while (true)
        {
            var result = await udp.ReceiveAsync();
            _ = HandleRequestAsync(udp, result);
        }
    }

    static async Task HandleRequestAsync(UdpClient udp, UdpReceiveResult result)
    {
        string message = Encoding.UTF8.GetString(result.Buffer);

        if (message == "DISCOVER_SERVER")
        {
            Console.WriteLine($"Discovery od {result.RemoteEndPoint}");

            string response = "10.10.10.114:6767";
            byte[] data = Encoding.UTF8.GetBytes(response);

            await udp.SendAsync(data, data.Length, result.RemoteEndPoint);
        }
    }
}