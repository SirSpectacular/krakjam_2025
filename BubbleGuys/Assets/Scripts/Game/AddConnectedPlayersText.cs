using Server;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class AddConnectedPlayersText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        
        private void Start()
        {
            UnityEvent<string> stateToListen = MyServer._playerNames;
            stateToListen?.AddListener(ServerListener);
        }

        private void ServerListener(string usernameAddOrDelete)
        {
            if (usernameAddOrDelete.StartsWith("Add"))
            text.text += "\n<align=left>  - " + username;
        }
    }
}
