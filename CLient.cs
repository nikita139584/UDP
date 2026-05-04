using System.Net;
using System.Net.Sockets;
using System.Text;

class Client
{
    private const int PORT = 27015;

    static async Task Main()
    {
        var udp = new UdpClient();


        var server = new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT);

        Console.WriteLine("Клієнт запущено");

  
        _ = Task.Run(async () =>
        {
            while (true)
            {
                var result = await udp.ReceiveAsync();
                var msg = Encoding.UTF8.GetString(result.Buffer);
                Console.WriteLine(msg);
            }
        });

        while (true)
        {
            var text = Console.ReadLine();
            var data = Encoding.UTF8.GetBytes(text!);
            await udp.SendAsync(data, data.Length, server);
        }
    }
}