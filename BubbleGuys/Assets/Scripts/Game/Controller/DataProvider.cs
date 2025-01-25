using UnityEngine;

namespace Game.Controller
{
    public class DataProvider : MonoBehaviour
    {
        private static DataProvider _instance; 
        public static DataProvider Instance => _instance;

        public Model.Player Player;

        private void Awake()
        {
            Player = new Model.Player(10);
            _instance = this;
        }
        
    }
}