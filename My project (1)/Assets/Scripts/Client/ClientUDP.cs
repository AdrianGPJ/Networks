using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using TMPro;

public class ClientUDP : MonoBehaviour
{
    Socket socket;
    public GameObject UItextObj;
    TextMeshProUGUI UItext;
    string clientText;

    void Start()
    {
        UItext = UItextObj.GetComponent<TextMeshProUGUI>();
    }

    public void StartClient()
    {
        Thread mainThread = new Thread(Send);
        mainThread.Start();
    }

    void Update()
    {
        UItext.text = clientText;
    }

    void Send()
    {
        // TO DO 2: Initialize the socket and endpoint
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("192.168.206.17"), 9050);
        socket = new Socket(ipep.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

        // TO DO 2.1: Send a handshake
        string handshake = "Hello World";
        byte[] data = Encoding.ASCII.GetBytes(handshake);
        socket.SendTo(data, ipep);

        // Start the receive thread
        Thread receive = new Thread(Receive);
        receive.Start();
    }

    void Receive()
    {
        // TO DO 3: Create endpoint for receiving
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 9050);
        EndPoint Remote = (EndPoint)sender;

        while (true)
        {
            byte[] data = new byte[1024];
            int recv = socket.ReceiveFrom(data, ref Remote);
            clientText = $"Message received from {Remote}: " + Encoding.ASCII.GetString(data, 0, recv);
        }
    }
}