using UnityEngine;

namespace Monsters
{
    public class FreezyController : MonsterController
    {
        [SerializeField] private float distance = 6f;
        
        public override void SetChaseTarget()
        {
            base.SetChaseTarget();
            Vector3 playerPosition = Player.position;

            if (Vector2.Distance(playerPosition, transform.position) < distance)
            {
                FinalTarget = Configuration.ChaseDefaultPosition;
            }
            else
                FinalTarget = Player.position;
        }

    }
}