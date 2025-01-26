using Game.Controller;
using JetBrains.Annotations;
using UnityEngine;

public class LevelGoalView : MonoBehaviour
{
    [UsedImplicitly]
    public void OnPlayerEntered(int playerId)
    {
        DataProvider.Instance.GameState.Finish(playerId);
    }
}
