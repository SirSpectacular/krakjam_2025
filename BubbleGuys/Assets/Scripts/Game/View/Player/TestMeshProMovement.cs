using UnityEngine;

namespace Game.View.Player
{
    public class TestMeshProMovement : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, transform.parent.rotation.z * -1.0f);
        }
    }
}
