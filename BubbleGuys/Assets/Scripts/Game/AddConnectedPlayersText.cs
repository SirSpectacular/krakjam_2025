using System.Collections.Generic;
using System.Net.Sockets;
using System.Timers;
using Server;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class AddConnectedPlayersText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        private readonly List<string> _playerNames = new();
        private string _address = "";
        private readonly int _port = 5173;
        
        private void Start()
        {
            UnityEvent<string> stateToListen = MyServer._playerNames;
            stateToListen?.AddListener(ServerListener);
            _address = WebSocketServer.WebSocketServer.Address;
            ConstructText();
        }

        private void ServerListener(string usernameAddOrDelete)
        {
            if (usernameAddOrDelete.StartsWith("Add_"))
            {
                string[] split = usernameAddOrDelete.Split("_");
                if (split.Length == 2)
                {
                    _playerNames.Add(split[1]);
                }
            } else if (usernameAddOrDelete.StartsWith("Delete_"))
            {
                string[] split = usernameAddOrDelete.Split("_");
                if (split.Length == 2)
                {
                    _playerNames.Remove(split[1]);
                }
            }
            
            ConstructText();
        }

        private void ConstructText()
        {
            text.text = "To join visit: " + _address + ":" + _port + "\n";
            text.text += PingHost(_address, _port) ? "<color=\"green\">(running)</color>" : "<color=\"red\">(down)</color>";
            text.text += "\n\nConnected players:";
            
            foreach (var username in _playerNames)
            {
                text.text += "\n  - " + username;
            }
            
            Timer aTimer = new Timer();
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Interval = 5000; 
            aTimer.Enabled = true;
        }
        
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            ConstructText();
        }
        
        private static bool PingHost(string hostUri, int portNumber)
        {
            try
            {
                using var client = new TcpClient(hostUri, portNumber);
                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }
    }
}
