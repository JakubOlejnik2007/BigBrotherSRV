using System;
using System.Net;
using System.Net.NetworkInformation;
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
    static string GetLocalIPv4()
    {
        return NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(i => i.OperationalStatus == OperationalStatus.Up)
            .SelectMany(i => i.GetIPProperties().UnicastAddresses)
            .Where(a =>
                a.Address.AddressFamily == AddressFamily.InterNetwork &&
                a.Address.ToString().StartsWith("10.10.10."))
            .Select(a => a.Address.ToString())
            .FirstOrDefault();
    }

    static async Task HandleRequestAsync(UdpClient udp, UdpReceiveResult result)
    {
        string message = Encoding.UTF8.GetString(result.Buffer);

        if (message == "DISCOVER_SERVER")
        {
            Console.WriteLine($"Discovery od {result.RemoteEndPoint}");

            string response = $"{GetLocalIPv4()}:6767";
            byte[] data = Encoding.UTF8.GetBytes(response);

            await udp.SendAsync(data, data.Length, result.RemoteEndPoint);
        }
    }
}