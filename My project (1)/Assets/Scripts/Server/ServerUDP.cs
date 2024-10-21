using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using TMPro;

public class ServerUDP : MonoBehaviour
{
    Socket socket;

    public GameObject UItextObj;
    TextMeshProUGUI UItext;
    string serverText;

    void Start()
    {
        UItext = UItextObj.GetComponent<TextMeshProUGUI>();
    }

    public void startServer()
    {
        serverText = "Starting UDP Server...";

        // TO DO 1: Create and bind the socket
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
        socket = new Socket(ipep.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
        socket.Bind(ipep);
        serverText += "\nUDP Server started, waiting for clients...";

        Thread newConnection = new Thread(Receive);
        newConnection.Start();
    }

    void Update()
    {
        UItext.text = serverText;
    }

    void Receive()
    {
        // TO DO 3: Create endpoint for receiving
        int recv;
        byte[] data = new byte[1024];

        serverText += "\nWaiting for new Client...";
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        EndPoint Remote = (EndPoint)sender;

        while (true)
        {
            // TO DO 3: Receive messages
            recv = socket.ReceiveFrom(data, ref Remote);
            serverText += $"\nMessage received from {Remote}: " + Encoding.ASCII.GetString(data, 0, recv);

            // TO DO 4: Send a ping back
            Thread sendThread = new Thread(() => Send(Remote));
            sendThread.Start();
        }
    }

    void Send(EndPoint Remote)
    {
        // TO DO 4: Send a "ping"
        string welcome = "Ping";
        byte[] data = Encoding.ASCII.GetBytes(welcome);
        socket.SendTo(data, Remote);
    }
}