using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using TMPro;
using System;

public class ClientTCP : MonoBehaviour
{
    public GameObject UItextObj;
    TextMeshProUGUI UItext;
    string clientText;
    Socket server;

    void Start()
    {
        UItext = UItextObj.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        UItext.text = clientText;
    }

    public void StartClient()
    {
        Thread connect = new Thread(Connect);
        connect.Start();
    }

    void Connect()
    {
        // TO DO 2: Create server endpoint
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
        server = new Socket(ipep.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            server.Connect(ipep);
            clientText = "Connected to server.";
        }
        catch (Exception e)
        {
            clientText = $"Error: {e.Message}";
            return;
        }

        Thread sendThread = new Thread(Send);
        sendThread.Start();

        Thread receiveThread = new Thread(Receive);
        receiveThread.Start();
    }

    void Send()
    {
        // TO DO 4: Send a message
        string message = "Hola caracola from TCP Client";
        byte[] data = Encoding.ASCII.GetBytes(message);
        server.Send(data);
    }

    void Receive()
    {
        // TO DO 7: Receive messages
        byte[] data = new byte[1024];
        while (true)
        {
            int recv = server.Receive(data);
            clientText += "\n" + Encoding.ASCII.GetString(data, 0, recv);
        }
    }
}