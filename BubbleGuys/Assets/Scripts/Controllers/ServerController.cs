using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;

public class Server : MonoBehaviour
{  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 80);

        server.Start();
        Debug.Log("Server has started on 127.0.0.1:80.{0}Waiting for a connection…" + Environment.NewLine);

        TcpClient client = server.AcceptTcpClient();

        Debug.Log("A client connected.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
