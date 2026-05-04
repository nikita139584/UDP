using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    private const int PORT = 27015;
    private static List<IPEndPoint> clients = new List<IPEndPoint>();

    static async Task Main()
    {
        var udp = new UdpClient(PORT);
        Console.WriteLine("UDP сервер запущено");

        while (true)
        {
            var result = await udp.ReceiveAsync();
            var message = Encoding.UTF8.GetString(result.Buffer);
            var sender = result.RemoteEndPoint;

            Console.WriteLine($"[{sender}] {message}");


            if (!clients.Any(c => c.Equals(sender)))
                clients.Add(sender);

            var data = Encoding.UTF8.GetBytes($"{sender}: {message}");

    
            foreach (var client in clients)
            {
                await udp.SendAsync(data, data.Length, client);
            }
        }
    }
}