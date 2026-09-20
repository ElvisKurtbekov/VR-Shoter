using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class SocketClient : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;

    void Start()
    {
        ConnectToServer();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SendHit();
        }
    }

    void ConnectToServer()
    {
        try
        {
            client = new TcpClient(
                "127.0.0.1",
                6666
            );

            stream = client.GetStream();

            Debug.Log("[SOCKET] Подключение к серверу установлено");
        }
        catch (Exception e)
        {
            Debug.LogError(
                "[SOCKET] Ошибка подключения: " + e.Message
            );
        }
    }

    void SendHit()
    {
        if (client == null || !client.Connected)
        {
            Debug.LogWarning(
                "[SOCKET] Сервер не подключен"
            );

            return;
        }

        try
        {
            string message = "HIT";

            byte[] data = Encoding.UTF8.GetBytes(message);

            stream.Write(
                data,
                0,
                data.Length
            );

            Debug.Log("[SHOT] Выстрел!");

            byte[] buffer = new byte[1024];

            int bytes = stream.Read(
                buffer,
                0,
                buffer.Length
            );

            string response = Encoding.UTF8.GetString(
                buffer,
                0,
                bytes
            );

            string[] parts = response.Split(';');

            string shot = parts[0].Replace("SHOT:", "");
            string damage = parts[1].Replace("DAMAGE:", "");

            Debug.Log(
                $"[TARGET] Попадание! " +
                $"Выстрел №{shot} | " +
                $"Общий урон: {damage}"
            );
        }
        catch (Exception e)
        {
            Debug.LogError(
                "[SOCKET] Ошибка: " + e.Message
            );
        }
    }

    void OnApplicationQuit()
    {
        if (stream != null)
            stream.Close();

        if (client != null)
            client.Close();
    }
}