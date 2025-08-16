using UnityEngine;

namespace Level
{
    public class Cookie : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.CompareTag("Player") || !enabled) return;
            
            Destroy(gameObject);
        }
    }
}
