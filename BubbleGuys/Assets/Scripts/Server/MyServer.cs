using System;
using System.Globalization;
using Server.PlayerMoves;
using Unity.Mathematics;
using UnityEngine;
using WebSocketServer;

namespace Server
{
    public class MyServer: WebSocketServer.WebSocketServer
    {
        public static event EventHandler OnOpened;
        public override void OnOpen(WebSocketConnection connection) {
            Debug.Log("Player joined: " + connection.id);
            OnOpened?.Invoke(this, EventArgs.Empty);
        }
  
        public override void OnMessage(WebSocketMessage message) {
            if (message.data.StartsWith("Move_"))
            {
                string[] split = message.data.Split("_");
                if (split.Length == 3)
                {
                    float power = float.Parse(split[1], CultureInfo.InvariantCulture.NumberFormat); 
                    float angleFromSocket = float.Parse(split[2], CultureInfo.InvariantCulture.NumberFormat);
                    //angleFromSocket is from 0-2pi, where 0 starts in 2nd quadrant of XY coordinates, need to change it
                    float angle = (angleFromSocket + Mathf.PI / 2) % (2 * Mathf.PI);
                    JoystickMove move = new JoystickMove(power, angle);

                    OnUpdate();
                }
            }
            
            Debug.Log(message.connection.id + " " + message.id + " " + message.data);
        }

        private void OnUpdate()
        {
            MyInputDevice.OnUpdate(default);
        }

        public override void OnClose(WebSocketConnection connection) {
            Debug.Log("Player left: " + connection.id);
        }

    }
}