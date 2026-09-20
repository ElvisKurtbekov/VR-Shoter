using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main()
    {
        int port = 6666;

        TcpListener server = new TcpListener(
            IPAddress.Any,
            port
        );

        server.Start();

        Console.WriteLine("=== VR SHOOTING RANGE SERVER ===");
        Console.WriteLine("Ожидание подключения Unity...");

        TcpClient client = server.AcceptTcpClient();

        Console.WriteLine("Unity подключилась!");
        Console.WriteLine();

        NetworkStream stream = client.GetStream();

        byte[] buffer = new byte[1024];

        int damage = 0;
        int shotNumber = 0;

        while (true)
        {
            int bytes = stream.Read(
                buffer,
                0,
                buffer.Length
            );

            if (bytes == 0)
                break;

            string message = Encoding.UTF8.GetString(
                buffer,
                0,
                bytes
            );

            if (message == "HIT")
            {
                shotNumber++;
                damage += 10;

                Console.WriteLine(
                    $"Выстрел №{shotNumber} | Урон: {damage}"
                );

                string response =
                    $"SHOT:{shotNumber};DAMAGE:{damage}";

                byte[] data = Encoding.UTF8.GetBytes(response);

                stream.Write(
                    data,
                    0,
                    data.Length
                );
            }
        }

        client.Close();
        server.Stop();
    }
}