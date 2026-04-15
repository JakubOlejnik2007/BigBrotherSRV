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

        string ip = client.Client.RemoteEndPoint.ToString();
        var clientData = new ClientData
        {
            IP = ip,
            Client = client,
            LastSeen = DateTime.Now,
           ScreenPath = null
        };
        try
        {
            while (true)
            {
                byte[] lengthBytes = new byte[4];
                int read = await ReadExact(stream, lengthBytes, 4);
                if (read == 0) return;

                int length = BitConverter.ToInt32(lengthBytes, 0);

                byte[] buffer = new byte[length];
                await ReadExact(stream, buffer, length);

                string message = Encoding.UTF8.GetString(buffer);

                if (message.StartsWith("PING"))
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"Ping od: {client.Client.RemoteEndPoint}");

                    ClientDataFactory.AddOrGet(client);

                }
                else if (message.StartsWith("SCREENSHOT|"))
                {

                    string base64 = message.Substring("SCREENSHOT|".Length);

                    byte[] imageBytes = Convert.FromBase64String(base64);

                    string path = Path.Combine(
                        AppContext.BaseDirectory,
                        $"screen_{DateTime.Now.Ticks}.png");

                    File.WriteAllBytes(path, imageBytes);

                    ClientDataFactory.UpdateScreenPath(ip, path);

                    System.Diagnostics.Debug.WriteLine(path);
                    System.Diagnostics.Debug.WriteLine("Zapisano screenshot");
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }
    private async Task<int> ReadExact(NetworkStream stream, byte[] buffer, int size)
    {
        int offset = 0;

        while (offset < size)
        {
            int read = await stream.ReadAsync(buffer, offset, size - offset);
            if (read == 0) return 0;

            offset += read;
        }

        return offset;
    }

    private async Task SendCommand(TcpClient client, string msg)
    {
        var stream = client.GetStream();
        byte[] data = Encoding.UTF8.GetBytes(msg);
        await stream.WriteAsync(data, 0, data.Length);
    }

}