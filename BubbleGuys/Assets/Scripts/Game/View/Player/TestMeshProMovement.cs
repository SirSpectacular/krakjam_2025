using UnityEngine;

namespace Game.View.Player
{
    public class TestMeshProMovement : MonoBehaviour
    {
        [SerializeField] private float _yOffset = 2;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = transform.parent.position + Vector3.up * _yOffset;
            transform.rotation = Quaternion.identity;
        }
    }
}
