using Player;
using UnityEngine;

namespace Monsters
{
    public class PochyController : MonsterController
    {
        [SerializeField] private int offsetMultiplier = 4;
        
        public override void SetChaseTarget()
        {
            base.SetChaseTarget();
            
            Vector3 playerPosition = Player.position;

            Vector2 playerDirection = Player.GetComponent<PlayerMovement>().Direction;
            Vector3 offset = -playerDirection * offsetMultiplier;
            
            FinalTarget = playerPosition + offset;
        }
    }
}