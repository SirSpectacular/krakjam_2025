
using System;
using UnityEngine;

namespace Game.View.Player
{
    public class MaintainWorldScale : MonoBehaviour
    {
        [Tooltip("The desired world scale for this object (X, Y, Z).")]
        public Vector3 targetWorldScale = Vector3.one;

        private void OnValidate()
        {
            targetWorldScale = transform.lossyScale;
        }

        private void Awake()
        {
            targetWorldScale = transform.lossyScale;
        }

        void Update()
        {
            SetWorldScale(targetWorldScale);
        }

        /// <summary>
        /// Adjusts the local scale of the object to match the desired world scale.
        /// </summary>
        /// <param name="desiredWorldScale">The desired scale in world space.</param>
        private void SetWorldScale(Vector3 desiredWorldScale)
        {
            if (transform.parent != null)
            {
                // Calculate the local scale needed to achieve the desired world scale
                Vector3 parentScale = transform.parent.lossyScale;
                transform.localScale = new Vector3(
                    desiredWorldScale.x / parentScale.x,
                    desiredWorldScale.y / parentScale.y,
                    desiredWorldScale.z / parentScale.z
                );
            }
            else
            {
                // If no parent, simply set the local scale to the desired world scale
                transform.localScale = desiredWorldScale;
            }
        }
    }
}