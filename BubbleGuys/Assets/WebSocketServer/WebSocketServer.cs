// Networking libs
using System.Net;
using System.Net.Sockets;
// For creating a thread
using System.Threading;
// For List & ConcurrentQueue
using System.Collections.Concurrent;
using System.IO;
// Unity & Unity events
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

namespace WebSocketServer {
    [System.Serializable]
    public class WebSocketOpenEvent : UnityEvent<WebSocketConnection> {}

    [System.Serializable]
    public class WebSocketMessageEvent : UnityEvent<WebSocketMessage> {}

    [System.Serializable]
    public class WebSocketCloseEvent : UnityEvent<WebSocketConnection> {}

    public class WebSocketServer : MonoBehaviour
    {
        // The tcpListenerThread listens for incoming WebSocket connections, then assigns the client to handler threads;
        private TcpListener _tcpListener;
        private Thread _tcpListenerThread;
        private TcpClient _connectedTcpClient;

        public ConcurrentQueue<WebSocketEvent> Events;

        public static readonly string Address = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
        public static readonly int Port = 8080;
        private const string IPAddressSavePath = "../gamepad-main/your_ip_address";
        public WebSocketOpenEvent onOpen;
        public WebSocketMessageEvent onMessage;
        public WebSocketCloseEvent onClose;
        
        void Awake() {
            if (onMessage == null) onMessage = new WebSocketMessageEvent();
        }

        void Start() {
            Events = new ConcurrentQueue<WebSocketEvent>();

            _tcpListenerThread = new Thread (ListenForTcpConnection)
            {
                IsBackground = true
            };
            _tcpListenerThread.Start();

            using StreamWriter writer = new StreamWriter(IPAddressSavePath);
            writer.WriteLine(Address);
        }
        
        void OnApplicationQuit()
        {
            _tcpListenerThread.Abort();
            Debug.Log("WebSocket server shutting down");
        }
       
        void Update() {
            while (Events.TryDequeue(out var wsEvent)) {
                if (wsEvent.type == WebSocketEventType.Open) {
                    onOpen.Invoke(wsEvent.connection);
                    OnOpen(wsEvent.connection);
                } else if (wsEvent.type == WebSocketEventType.Close) {
                    onClose.Invoke(wsEvent.connection);
                    OnClose(wsEvent.connection);
                } else if (wsEvent.type == WebSocketEventType.Message) {
                    WebSocketMessage message = new WebSocketMessage(wsEvent.connection, wsEvent.data);
                    onMessage.Invoke(message);
                    OnMessage(message);
                }
            }
        }

        private void ListenForTcpConnection () { 		
            try {
                // Create listener on <address>:<port>.
                _tcpListener = new TcpListener(IPAddress.Parse(Address), Port);
                _tcpListener.Start();
                Debug.Log("WebSocket server is listening for incoming connections.");
                while (true) {
                    // Accept a new client, then open a stream for reading and writing.
                    _connectedTcpClient = _tcpListener.AcceptTcpClient();
                    // Create a new connection
                    WebSocketConnection connection = new WebSocketConnection(_connectedTcpClient, this);
                    // Establish connection
                    connection.Establish();
                    // Start a new thread to handle the connection.
                    // Thread worker = new Thread (new ParameterizedThreadStart(HandleConnection));
                    // worker.IsBackground = true;
                    // worker.Start(connection);
                    // // Add it to the thread list. TODO: delete thread when disconnecting.
                    // workerThreads.Add(worker);
                }
            }
            catch (SocketException socketException) {
                Debug.Log("SocketException " + socketException);
            }
        }

        // private void HandleConnection (object parameter) {
        //     WebSocketConnection connection = (WebSocketConnection)parameter;
        //     while (true) {
        //         string message = ReceiveMessage(connection.client, connection.stream);
        //         connection.queue.Enqueue(message);
        //     }
        // }

        // private string ReceiveMessage(TcpClient client, NetworkStream stream) {
        //     // Wait for data to be available, then read the data.
        //     while (!stream.DataAvailable);
        //     Byte[] bytes = new Byte[client.Available];
        //     stream.Read(bytes, 0, bytes.Length);

        //     return WebSocketProtocol.DecodeMessage(bytes);
        // }

        public virtual void OnOpen(WebSocketConnection connection) {}

        public virtual void OnMessage(WebSocketMessage message) {}

        public virtual void OnClose(WebSocketConnection connection) {}

        public virtual void OnError(WebSocketConnection connection) {}


        // private void SendMessage() {
        //     if (connectedTcpClient == null) {
        //         return;
        //     }

        //     try {
        //         // Get a stream object for writing.
        //         NetworkStream stream = connectedTcpClient.GetStream();
        //         if (stream.CanWrite) {
        //             string serverMessage = "This is a message from your server.";
        //             // Convert string message to byte array.
        //             byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(serverMessage);
        //             // Write byte array to socketConnection stream.
        //             stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
        //             Debug.Log("Server sent his message - should be received by client");
        //         }
        //     }
        //     catch (SocketException socketException) {
        //         Debug.Log("Socket exception: " + socketException);
        //     }
        // }
    }
}

