using Game.Controller;
using JetBrains.Annotations;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [UsedImplicitly]
    public void StartTheGame()
    {
        DataProvider.Instance.GameState.Start();
    }
}
