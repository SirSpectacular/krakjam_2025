using System;
using System.Collections.Generic;
using System.Globalization;
using Input;
using Server.PlayerMoves;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using WebSocketServer;

namespace Server
{
    public class MyServer: WebSocketServer.WebSocketServer
    {
        [SerializeField] private List<WebSocketConnection> _playerMap = new List<WebSocketConnection>();
        public static UnityEvent<MyDeviceState> Player1 = new UnityEvent<MyDeviceState>();
        public static UnityEvent<MyDeviceState> Player2 = new UnityEvent<MyDeviceState>();
        public static UnityEvent<MyDeviceState> Player3 = new UnityEvent<MyDeviceState>();
        public static UnityEvent<MyDeviceState> Player4 = new UnityEvent<MyDeviceState>();

        public override void OnOpen(WebSocketConnection connection) {
            if (_playerMap.Count >= 4)
            {
                Debug.Log("Too many players: " + connection.id);
                return;
            }
            
            Debug.Log("Player joined: " + connection.id);
            _playerMap.Add(connection);
        }
  
        public override void OnMessage(WebSocketMessage message)
        {
            int playerNo = GetPlayerId(message.connection.id);
            if (playerNo == 0)
            {
                return;
            }
            
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

                    OnUpdateMove(playerNo, move);
                }
            }

            if (message.data == "Action")
            {
                var action = ActionType.Action;
                OnUpdateAction(playerNo, action);
            }
        }

        private void OnUpdateMove(int playerNo, JoystickMove move)
        {   
            Vector2 normalizedVector = move.move.normalized;
            Debug.Log("(X,Y) = (" + normalizedVector.x +"," + normalizedVector.y + ")");
            
            MyDeviceState state = new MyDeviceState();
            state.MoveVector = normalizedVector;
            
            switch (playerNo)
            {
                case 1:
                    Player1.Invoke(state);
                    break;
                case 2:
                    Player2.Invoke(state);
                    break;
                case 3:
                    Player3.Invoke(state);
                    break;
                case 4:
                    Player4.Invoke(state);
                    break;
            }
        }
        
        private void OnUpdateAction(int playerNo, ActionType action)
        {
            MyDeviceState state = new MyDeviceState();
            state.ButtonClicked = true;
            
            switch (playerNo)
            {
                case 1:
                    Player1.Invoke(state);
                    break;
                case 2:
                    Player2.Invoke(state);
                    break;
                case 3:
                    Player3.Invoke(state);
                    break;
                case 4:
                    Player4.Invoke(state);
                    break;
            }
        }

        public override void OnClose(WebSocketConnection connection) {
            Debug.Log("Player left: " + connection.id);
            _playerMap.Remove(connection);
        }

        private int GetPlayerId(string connectionId)
        {
            for (int i = 0; i < _playerMap.Count; i++)
            {
                if (_playerMap[i] != null && _playerMap[i].id == connectionId)
                {
                    return i + 1;
                }
            }
           
            return 0;
        }
    }
}