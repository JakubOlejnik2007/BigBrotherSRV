using System.Net.Sockets;
using System.Text;

namespace BigBrotherSRV
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = ClientStore.Clients;
        }


          private void OnLockClicked(object sender, TappedEventArgs e)
        {
            var image = sender as Image;
            var client = image.BindingContext as ClientData;

            if (client == null)
                return;

            string ip = client.IP;

            // tutaj wysyłasz komendę do serwera
           SendCommand(client.Client, "CMD|LOCK");
        }
        async Task SendCommand(TcpClient client, string msg)
        {
            var stream = client.GetStream();
            byte[] data = Encoding.UTF8.GetBytes(msg);
            await stream.WriteAsync(data, 0, data.Length);
        }



    }

}

