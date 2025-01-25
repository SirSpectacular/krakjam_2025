using System;
using System.Globalization;
using Input;
using Server.PlayerMoves;
using Unity.Mathematics;
using UnityEngine;
using WebSocketServer;

namespace Server
{
    public class Server: WebSocketServer.WebSocketServer {

        public override void OnOpen(WebSocketConnection connection) {
            Debug.Log("Player joined: " + connection.id);
        }
  
        public override void OnMessage(WebSocketMessage message) {
            if (message.data.StartsWith("Move_"))
            {
                var split = message.data.Split("_");
                if (split.Length == 3)
                {
                    var power = float.Parse(split[1], CultureInfo.InvariantCulture.NumberFormat); 
                    var angleFromSocket = float.Parse(split[2], CultureInfo.InvariantCulture.NumberFormat);
                    //angleFromSocket is from 0-2pi, where 0 starts in 2nd quadrant of XY coordinates, need to change it
                    var angle = (angleFromSocket + Mathf.PI / 2) % (2 * Mathf.PI);
                    var move = new JoystickMove(power, angle);
                }
            }
            
            Debug.Log(message.connection.id + " " + message.id + " " + message.data);
        }
  
        public override void OnClose(WebSocketConnection connection) {
            Debug.Log("Player left: " + connection.id);
        }

    }
}