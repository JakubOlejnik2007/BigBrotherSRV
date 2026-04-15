using System.Collections.ObjectModel;

public static class ClientStore
{
    public static ObservableCollection<ClientData> Clients { get; } = new();
}