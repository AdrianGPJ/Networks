using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using TMPro;

public class ServerTCP : MonoBehaviour
{
    Socket socket;
    Thread mainThread = null;

    public GameObject UItextObj;
    TextMeshProUGUI UItext;
    string serverText;

    public struct User
    {
        public string name;
        public Socket socket;
    }

    void Start()
    {
        UItext = UItextObj.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        UItext.text = serverText;
    }

    public void startServer()
    {
        serverText = "Starting TCP Server...";

        // TO DO 1: Create and bind the socket
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 9050);
        socket.Bind(localEndPoint);
        socket.Listen(10);
        serverText += "\nServer started, waiting for connections...";

        mainThread = new Thread(CheckNewConnections);
        mainThread.Start();
    }

    void CheckNewConnections()
    {
        while (true)
        {
            User newUser = new User();
            newUser.name = "";
            // TO DO 3: Accept any incoming clients
            newUser.socket = socket.Accept();

            IPEndPoint clientep = (IPEndPoint)newUser.socket.RemoteEndPoint;
            serverText += $"\nConnected with {clientep.Address} at port {clientep.Port}";

            Thread newConnection = new Thread(() => Receive(newUser));
            newConnection.Start();
        }
    }

    void Receive(User user)
    {
        // TO DO 5: Receiving messages
        byte[] data = new byte[1024];
        int recv = 0;

        while (true)
        {
            recv = user.socket.Receive(data);
            if (recv == 0) break;

            serverText += "\n" + Encoding.ASCII.GetString(data, 0, recv);

            // TO DO 6: Send a ping back
            Thread answer = new Thread(() => Send(user));
            answer.Start();
        }
    }

    void Send(User user)
    {
        // TO DO 4: Send a "ping"
        string message = "Ping";
        byte[] data = Encoding.ASCII.GetBytes(message);
        user.socket.Send(data);
    }
}