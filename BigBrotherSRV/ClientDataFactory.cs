using System.Net.Sockets;

public static class ClientDataFactory
{
    public static ClientData AddOrGet(TcpClient client)
    {
        string ip = client.Client.RemoteEndPoint.ToString();

        var existing = ClientStore.Clients.FirstOrDefault(x => x.IP == ip);

        if (existing != null)
        {
            existing.Client = client;
            return existing;
        }

        var newClient = new ClientData
        {
            IP = ip,
            Client = client,
            LastSeen = DateTime.Now,
            ScreenPath = null
        };

        ClientStore.Clients.Add(newClient);

        return newClient;
    }

    public static void UpdateLastSeen(string ip)
    {
        var client = ClientStore.Clients.FirstOrDefault(x => x.IP == ip);

        if (client != null)
        {
            client.LastSeen = DateTime.Now;
        }
    }

    public static void UpdateScreenPath(string ip, string path)
    {
        var client = ClientStore.Clients.FirstOrDefault(x => x.IP == ip);

        if (client != null)
        {
            client.ScreenPath = path;
        }
    }

    public static ClientData GetByIp(string ip)
    {
        return ClientStore.Clients.FirstOrDefault(x => x.IP == ip);
    }

    public static List<ClientData> GetAll()
    {
        return ClientStore.Clients;
    }
}