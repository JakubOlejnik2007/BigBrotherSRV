using System;
using System.ComponentModel;
using System.Net.Sockets;

public class ClientData : INotifyPropertyChanged
{
    public string IP { get; set; }
    public TcpClient Client { get; set; }

    DateTime lastSeen;
    public DateTime LastSeen
    {
        get => lastSeen;
        set
        {
            if (lastSeen == value) return;
            lastSeen = value;
            OnPropertyChanged(nameof(LastSeen));
        }
    }

    string screenPath;
    public string ScreenPath
    {
        get => screenPath;
        set
        {
            if (screenPath == value) return;
            screenPath = value;
            OnPropertyChanged(nameof(ScreenPath));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    bool locked;
    public bool Locked
    {
        get => locked;
        set
        {
            if (locked == value) return;
            locked = value;
            OnPropertyChanged(nameof(locked));
        }
    }


    void OnPropertyChanged(string name)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}