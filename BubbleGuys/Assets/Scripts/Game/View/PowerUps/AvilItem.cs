using UnityEngine;
using Game.Player;  // bo PlayerMovement jest w namespace Game.Player

public class AvilItem : MonoBehaviour
{
    [Header("Czas trwania efektu (w sekundach)")]
    public float effectDuration = 5f;

    [Header("Zmiany w parametrach fizyki postaci")]
    public float massMultiplier = 2f;
    public float gravityScaleMultiplier = 1.5f;
    public float linearDragMultiplier = 0.5f;
    public float angularDragMultiplier = 0.5f;

    [Header("Zmiany w parametrach ruchu postaci")]
    public float moveFactorMultiplier = 0.7f;
    public float rotationFactorMultiplier = 0.5f;
    // ewentualnie public float loseVolumeFactorMultiplier = 1.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sprawdzamy, czy dotykającym obiektem jest gracz
        PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            // Uruchamiamy coroutine w PlayerMovement
            playerMovement.StartCoroutine(
                playerMovement.ApplyAnvilEffect(
                    effectDuration,
                    massMultiplier,
                    gravityScaleMultiplier,
                    linearDragMultiplier,
                    angularDragMultiplier,
                    moveFactorMultiplier,
                    rotationFactorMultiplier
                    // tutaj dodałbyś ewentualnie loseVolumeFactorMultiplier
                )
            );

            // Item znika, by nie można było go użyć 2x
            Destroy(gameObject);
        }
    }
}