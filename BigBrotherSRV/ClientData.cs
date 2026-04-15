using System;

using System.Net.Sockets;

public class ClientData
{
    public string IP { get; set; }
    public TcpClient Client { get; set; }
    public DateTime LastSeen { get; set; }
    public string ScreenPath { get; set; }
}