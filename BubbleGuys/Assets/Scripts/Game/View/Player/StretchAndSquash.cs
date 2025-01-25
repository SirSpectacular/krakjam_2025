using UnityEngine;
    
namespace Game.View.Player
{
    public class StretchAndSquash : MonoBehaviour
    {
        public Transform sprite;
        public float stretch = 0.1f;
        public Rigidbody2D rigidbody;

 
        private Vector3 _originalScale;
 
        private void Start()
        {
            _originalScale = sprite.transform.localScale;
        }
 
        private void Update()
        {
            sprite.localScale = Vector3.one;
            sprite.position = transform.position;
 
            Vector3 velocity = rigidbody.linearVelocity;
            if (velocity.sqrMagnitude > 0.01f)
            {
                sprite.rotation = Quaternion.Lerp(sprite.rotation, Quaternion.FromToRotation(Vector3.right, velocity), 1);
            }
 
            var scaleX = 1.0f + (velocity.magnitude * stretch);
            var scaleY = 1.0f / scaleX;
            sprite.parent = sprite;
            sprite.localScale = Vector3.Lerp(sprite.localScale, new Vector3(scaleX, scaleY, 1.0f), Time.deltaTime * 100);
        }
    }
}