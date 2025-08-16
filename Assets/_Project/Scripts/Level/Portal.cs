using UnityEngine;

namespace Level
{
        public class Portal : MonoBehaviour
        {
                public Vector3Int ExitDirection { get; set; }

                private void OnTriggerEnter2D(Collider2D other)
                {
                        other.GetComponentInParent<IPortable>().Teleport(transform.position, ExitDirection);
                }
        }
}