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
                    float angle = float.Parse(split[2], CultureInfo.InvariantCulture.NumberFormat);
                    //angleFromSocket is from 0-2pi, where 0 starts in 2nd quadrant of XY coordinates, need to change it
                    // float angle = (angleFromSocket + 2 * Mathf.PI) % (2 * Mathf.PI);
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
            //convert move (2d vector) to short
            Vector2 normalizedVector = move.move.normalized;
            float up = normalizedVector.y >= 0 ? normalizedVector.y : 0;
            float down = normalizedVector.y < 0 ? normalizedVector.y : 0;
            float right = normalizedVector.x >= 0 ? normalizedVector.x : 0;
            float left = normalizedVector.x < 0 ? normalizedVector.x : 0;
            Debug.Log("(X,Y) = (" + normalizedVector.x +"," + normalizedVector.y + ")");
            Debug.Log("(up,down,right,left) = (" + up +"," + down + "," + right + "," + left + ")");

            MyDeviceState moveState;
            if (playerNo == 1)
            {
                moveState = new MyDeviceState
                {
                    player1_up = up,
                    player1_down = down,
                    player1_right = right,
                    player1_left = left,
                };
            } else if (playerNo == 2)
            {
                moveState = new MyDeviceState
                {
                    player2_up = up,
                    player2_down = down,
                    player2_right = right,
                    player2_left = left,
                };
            } else if (playerNo == 3)
            {
                moveState = new MyDeviceState
                {
                    player3_up = up,
                    player3_down = down,
                    player3_right = right,
                    player3_left = left,
                };
            } else if (playerNo == 4)
            {
                moveState = new MyDeviceState
                {
                    player4_up = up,
                    player4_down = down,
                    player4_right = right,
                    player4_left = left,
                };
            } else
            {
                return;
            }
            
            MyInputDevice.OnUpdate(moveState);
        }
        
        private void OnUpdateAction(int playerNo, ActionType action)
        {
            MyDeviceState moveState;
            if (playerNo == 1)
            {
                moveState = new MyDeviceState
                {
                    button1 = 2
                };
            } else if (playerNo == 2)
            {
                moveState = new MyDeviceState
                {
                    button2 = 2
                };
            } else if (playerNo == 3)
            {
                moveState = new MyDeviceState
                {
                    button3 = 2
                };
            } else if (playerNo == 4)
            {
                moveState = new MyDeviceState
                {
                    button4 = 2
                };
            } else
            {
                return;
            }

            MyInputDevice.OnUpdate(moveState);
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