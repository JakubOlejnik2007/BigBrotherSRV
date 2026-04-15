using System.Net;
using System.Net.Sockets;
using System.Text;

public class TcpServer
{
    public async Task StartAsync()
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 6767);
        listener.Start();

        System.Diagnostics.Debug.WriteLine("Serwer działa...");

        while (true)
        {
            var client = await listener.AcceptTcpClientAsync();
            _ = HandleClient(client);
        }
    }

    private async Task HandleClient(TcpClient client)
    {
        var stream = client.GetStream();
        byte[] buffer = new byte[10_000_000];

        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        if (message.StartsWith("PING"))
        {
            System.Diagnostics.Debug.WriteLine($"Ping od: {client.Client.RemoteEndPoint}");
        }
        else if (message.StartsWith("SCREENSHOT"))
        {
            var data = message.Substring("SCREENSHOT".Length);

            byte[] imageBytes = Convert.FromBase64String(data);
            File.WriteAllBytes($"screen_{DateTime.Now.Ticks}.png", imageBytes);

            System.Diagnostics.Debug.WriteLine("Zapisano screenshot");
        }

        client.Close();
    }
}