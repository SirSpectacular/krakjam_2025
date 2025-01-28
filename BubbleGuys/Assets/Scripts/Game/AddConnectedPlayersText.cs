using System.Collections.Generic;
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
        
        private void Start()
        {
            UnityEvent<string> stateToListen = MyServer._playerNames;
            stateToListen?.AddListener(ServerListener);
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
            text.text = "Connected players:";
            
            foreach (var username in _playerNames)
            {
                text.text += "\n  - " + username;
            }
        }
    }
}
