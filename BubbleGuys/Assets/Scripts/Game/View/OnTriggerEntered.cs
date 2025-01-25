using UnityEngine;
using UnityEngine.Events;


    public sealed class OnTriggerEntered : MonoBehaviour
    {
        [SerializeField] public UnityEvent OnTriggerEnter;
        [SerializeField] public UnityEvent OnTriggerExit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerEnter.Invoke();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnTriggerExit.Invoke();
        }
    }
