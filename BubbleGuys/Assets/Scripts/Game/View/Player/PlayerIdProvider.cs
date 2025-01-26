using UnityEngine;

namespace Game.View.Player
{
    public class PlayerIdProvider : MonoBehaviour
    {
        [SerializeField] private int _playerId;
        
        public static implicit operator int(PlayerIdProvider p) => p._playerId;
    }
}