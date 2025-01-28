using System;
using System.Collections.Generic;
using System.Globalization;
using Game;
using Input;
using Server.PlayerMoves;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using WebSocketServer;

namespace Server
{
    public class MyServer: WebSocketServer.WebSocketServer
    {
        [SerializeField] private List<WebSocketConnection> _playerMap = new List<WebSocketConnection>();
        private static UnityEvent<MyDeviceState> _player1 = new UnityEvent<MyDeviceState>();
        private static UnityEvent<MyDeviceState> _player2 = new UnityEvent<MyDeviceState>();
        private static UnityEvent<MyDeviceState> _player3 = new UnityEvent<MyDeviceState>();
        private static UnityEvent<MyDeviceState> _player4 = new UnityEvent<MyDeviceState>();
        public static UnityEvent<string> _playerNames = new UnityEvent<string>();
        private Dictionary<WebSocketConnection, string> playerNameMap = new Dictionary<WebSocketConnection, string>();
        [SerializeField] public TextMeshPro playersJoined;

        public override void OnOpen(WebSocketConnection connection) {
            if (_playerMap.Count >= 4)
            {
                Debug.Log("Too many players: " + connection.id);
                return;
            }
            
            Debug.Log("Player joined: " + connection.id);
            _playerMap.Add(connection);
            _playerNames.Invoke("Add_" + _address);
        }
  
        public override void OnMessage(WebSocketMessage message)
        {
            int playerNo = GetPlayerId(message.connection.id);
            if (playerNo == 0)
            {
                return;
            }

            if (message.data.StartsWith("Name_"))
            {
                string[] split = message.data.Split("_");
                if (split.Length == 2)
                {
                    OnUpdateUserName(playerNo, split[1]);
                    playerNameMap.Add(message.connection, split[1]);
                    _playerNames.Invoke("Add_" + split[1]);
                }
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
            
            MyDeviceState state = new MyDeviceState();
            state.MoveVector = normalizedVector;
            
            switch (playerNo)
            {
                case 1:
                    _player1.Invoke(state);
                    break;
                case 2:
                    _player2.Invoke(state);
                    break;
                case 3:
                    _player3.Invoke(state);
                    break;
                case 4:
                    _player4.Invoke(state);
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
                    _player1.Invoke(state);
                    break;
                case 2:
                    _player2.Invoke(state);
                    break;
                case 3:
                    _player3.Invoke(state);
                    break;
                case 4:
                    _player4.Invoke(state);
                    break;
            }
        }
        
        private void OnUpdateUserName(int playerNo, string username)
        {
            MyDeviceState state = new MyDeviceState();
            state.UserName = username;
            
            switch (playerNo)
            {
                case 1:
                    _player1.Invoke(state);
                    break;
                case 2:
                    _player2.Invoke(state);
                    break;
                case 3:
                    _player3.Invoke(state);
                    break;
                case 4:
                    _player4.Invoke(state);
                    break;
            }
        }

        public override void OnClose(WebSocketConnection connection) {
            Debug.Log("Player left: " + connection.id);
            _playerNames.Invoke("Delete_" + playerNameMap[connection]);
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

        public static UnityEvent<MyDeviceState> GetPlayerDeviceState(int playerId)
        {
            switch (playerId)
            {
                case 1:
                    return _player1;
                case 2:
                    return _player2;
                case 3:
                    return _player3;
                case 4:
                    return _player4;
                default:
                    return null;
            }
        }
    }
}