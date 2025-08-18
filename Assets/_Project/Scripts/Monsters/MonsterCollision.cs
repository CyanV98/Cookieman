using UnityEngine;

namespace Monsters
{
    public class MonsterCollision : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && this.enabled)
            {
                UnityEngine.Debug.Log("Collision with cookieman");
                transform.parent.GetComponent<MonsterController>().IsEaten = true;
            }
        }
    }
}

