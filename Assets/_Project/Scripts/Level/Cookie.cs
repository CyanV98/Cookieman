using Game;
using UnityEngine;

namespace Level
{
    public class Cookie : MonoBehaviour
    {
        [SerializeField] private bool isSuperCookie;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.CompareTag("Player") || !enabled) return;
            
            if(isSuperCookie)
                GameEvents.SuperCookieEaten();
            else
                GameEvents.CookieEaten();

            Destroy(gameObject);
        }
    }
}
