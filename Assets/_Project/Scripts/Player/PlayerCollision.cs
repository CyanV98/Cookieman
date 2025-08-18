using Game;
using Level;
using Monsters;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] private LevelManager levelManager;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (ShouldConsiderCollision(other) && this.enabled)
            {
                GetComponentInParent<PlayerMovement>().enabled = false;
                GetComponentInParent<PlayerInput>().enabled = false;
                GameEvents.PlayerDead();
            }
        }

        private bool ShouldConsiderCollision(Collider2D other)
        {
            if (!other.CompareTag("Monster")) return false;

            bool isEaten = other.GetComponentInParent<MonsterController>().IsEaten;
            bool isFrighted = levelManager.CurrentState == MonsterLevelState.Frightened;

            return !isEaten && !isFrighted;
        }
    }
}