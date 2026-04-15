using System.Net.Sockets;

public class ClientInfo
{
    public string Id { get; set; }                 // IP:port
    public string Folder { get; set; }             // folder klienta
    public List<string> Screenshots { get; set; }  // historia screenów
    public string LastScreenshotPath { get; set; } // ostatni screen
    public DateTime LastSeen { get; set; }         // ping time
}

public class DataSaver
{
    private readonly string baseDir;

    private readonly Dictionary<string, ClientInfo> clients = new();

    public DataSaver()
    {
        baseDir = Path.Combine(AppContext.BaseDirectory, "clients");
        Directory.CreateDirectory(baseDir);
    }

    public ClientInfo RegisterClient(TcpClient client)
    {
        string id = client.Client.RemoteEndPoint.ToString();

        if (!clients.ContainsKey(id))
        {
            string dir = Path.Combine(baseDir, id);
            Directory.CreateDirectory(dir);

            clients[id] = new ClientInfo
            {
                Id = id,
                Folder = dir,
                Screenshots = new List<string>(),
                LastSeen = DateTime.Now
            };
        }

        return clients[id];
    }

    public ClientInfo SaveScreenshot(string clientId, byte[] imageBytes)
    {
        var client = clients[clientId];

        string filePath = Path.Combine(
            client.Folder,
            $"screen_{DateTime.Now.Ticks}.png"
        );

        File.WriteAllBytes(filePath, imageBytes);

        client.Screenshots.Add(filePath);
        client.LastScreenshotPath = filePath;
        client.LastSeen = DateTime.Now;

        return client;
    }

    public List<ClientInfo> GetAllClients()
    {
        return clients.Values.ToList();
    }

    public ClientInfo GetClient(string clientId)
    {
        return clients.ContainsKey(clientId)
            ? clients[clientId]
            : null;
    }
}