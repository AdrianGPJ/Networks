using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using TMPro;

public class ClientUDP : MonoBehaviour
{
    Socket socket; // UDP socket for communication
    public GameObject UItextObj; // Reference to UI Text object in Unity
    TextMeshProUGUI UItext; // Text component for displaying messages on UI
    string clientText; // String to store and display client-side messages

    void Start()
    {
        UItext = UItextObj.GetComponent<TextMeshProUGUI>(); // Get the TextMeshPro component for updating UI
    }

    public void StartClient()
    {
        Thread mainThread = new Thread(Send); // Create a new thread to handle sending data
        mainThread.Start(); // Start the thread
    }

    void Update()
    {
        // Update the UI text every frame to display any changes to clientText
        UItext.text = clientText;
    }

    void Send()
    {
        // TO DO 2: Initialize the socket and endpoint
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
        socket = new Socket(ipep.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

        // TO DO 2.1: Send a handshake
        string handshake = "DigaMelon from UDP Client";
        byte[] data = Encoding.ASCII.GetBytes(handshake);
        socket.SendTo(data, ipep); 

        
        Thread receive = new Thread(Receive);
        receive.Start();
    }

    void Receive()
    {
        // TO DO 3: Create endpoint for receiving
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        EndPoint Remote = (EndPoint)sender;

        while (true)
        {
            
            byte[] data = new byte[1024];
            int recv = socket.ReceiveFrom(data, ref Remote);
            clientText = $"Message received from {Remote}: " + Encoding.ASCII.GetString(data, 0, recv);
        }
    }
}