using Game.Controller;
using TMPro;
using UnityEngine;

public class ShowWinner : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    void OnEnable()
    {
        int? playerId = DataProvider.Instance.GameState.WinnerId;
        if (playerId == null)
        {
            _text.text = "You lose";
        }
        else
        {
            _text.text = $"Player {playerId.Value} wins";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
