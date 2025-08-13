using Monsters;
using UnityEngine;

namespace FSM.States
{
    [CreateAssetMenu(fileName = "new ChaseState", menuName = "Cookieman/FSM/State/Chase")]
    public class ChaseState : State
    {
        public override void Enter(GameObject owner, StateContext context)
        {
            owner.GetComponent<MonsterAnimator>().SetDefault(true);
        }

        public override void Tick(GameObject owner, StateContext context)
        {
            owner.GetComponent<MonsterController>().SetChaseTarget();
        }

        public override void Exit(GameObject owner, StateContext context)
        {
            owner.GetComponent<MonsterAnimator>().SetDefault(false);
        }
    }
}
