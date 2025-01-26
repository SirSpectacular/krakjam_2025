using Game.View.Player;
using UnityEngine;
using UnityEngine.Events;


    public sealed class OnTriggerEntered : MonoBehaviour
    {
        [SerializeField] public UnityEvent OnTriggerEnter;
        [SerializeField] public UnityEvent OnTriggerExit;
        [SerializeField] public UnityEvent<int> OnPlayerEntered;


        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerEnter.Invoke();
            
            PlayerIdProvider idProvider = other.GetComponentInParent<PlayerIdProvider>();
            if (idProvider == null)
            {
                return;
            }
            OnPlayerEntered.Invoke(idProvider);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnTriggerExit.Invoke();
        }
    }
