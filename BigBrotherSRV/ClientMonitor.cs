using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public static class ClientMonitor
{
    private static CancellationTokenSource _cts;

    public static void Start()
    {
        _cts = new CancellationTokenSource();

        Task.Run(async () =>
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                RemoveInactiveClients();
                await Task.Delay(TimeSpan.FromSeconds(4), _cts.Token);
            }
        }, _cts.Token);
    }

    public static void Stop()
    {
        _cts?.Cancel();
    }

    private static void RemoveInactiveClients()
    {
        var now = DateTime.Now;

        var toRemove = ClientStore.Clients
            .Where(c => (now - c.LastSeen).TotalSeconds > 8)
            .ToList();

        foreach (var client in toRemove)
        {
            ClientStore.Clients.Remove(client);
        }
    }
}