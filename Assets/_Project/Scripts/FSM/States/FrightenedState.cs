using Monsters;
using UnityEngine;

namespace FSM.States
{
    [CreateAssetMenu(fileName = "new FrightenedState", menuName = "Cookieman/FSM/State/Frightened")]
    public class FrightenedState : State
    {
        [SerializeField] private float frightenedTime = 10f;
        [SerializeField] private float frightenedTimeout = 3f;
        
        public override void Enter(GameObject owner, StateContext context)
        {
            owner.GetComponent<MonsterController>().GetNextIntermediateTarget = AINavigation.GetNextRandomTarget;
            owner.GetComponent<MonsterAnimator>().EnterFrightened();

            context.Timer = frightenedTime;
        }

        public override void Tick(GameObject owner, StateContext context)
        {
            context.Timer -= Time.deltaTime;

            if (context.Timer <= frightenedTimeout)
            {
                owner.GetComponent<MonsterAnimator>().EnterFrightenedTimeout();
            }
        }

        public override void Exit(GameObject owner, StateContext context)
        {
            context.Timer = 0f;
        }
    }
}