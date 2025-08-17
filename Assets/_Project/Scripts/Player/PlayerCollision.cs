using Level;
using Monsters;
using UnityEngine;

namespace Player
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] private LevelManager levelManager;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (ShouldConsiderCollision(other) && this.enabled)
            {
                GameEvents.PlayerDead();
            }
        }

        private bool ShouldConsiderCollision(Collider2D other)
        {
            if(!other.CompareTag("Monster")) return false;

            bool isEaten = other.GetComponentInParent<MonsterController>().IsEaten;
            bool isFrighted = levelManager.CurrentState == MonsterLevelState.Frightened;
            
            return !isEaten && !isFrighted;
        }
    }
}