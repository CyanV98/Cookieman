using UnityEngine;

namespace Monsters
{
    public class RosyController : MonsterController
    {
        public override void SetChaseTarget()
        {
            base.SetChaseTarget();

            FinalTarget = Player.position;
        }
    }
}