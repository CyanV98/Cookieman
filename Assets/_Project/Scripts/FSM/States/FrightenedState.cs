using Monsters;
using UnityEngine;

namespace FSM.States
{
    [CreateAssetMenu(fileName = "new FrightenedState", menuName = "Cookieman/FSM/State/Frightened")]
    public class FrightenedState : State
    {
        [SerializeField] private float frightenedTimeout = 3f;
        
        public override void Enter(GameObject owner, StateContext context)
        {
            owner.GetComponent<MonsterController>().GetNextIntermediateTarget = AINavigation.GetNextRandomTarget;
            owner.GetComponent<MonsterAnimator>().EnterFrightened();
            
            owner.GetComponentInChildren<MonsterCollision>().enabled = true;
        }

        public override void Tick(GameObject owner, StateContext context)
        {
            if (context.LevelManager.FrightenedTimer <= frightenedTimeout && !context.FrightenedTimeOutSet)
            {
                owner.GetComponent<MonsterAnimator>().EnterFrightenedTimeout();
                context.FrightenedTimeOutSet = true;
            }
        }

        public override void Exit(GameObject owner, StateContext context)
        {
            context.FrightenedTimeOutSet = false;
            owner.GetComponent<MonsterAnimator>().SetFrightened(false);
            
            owner.GetComponentInChildren<MonsterCollision>().enabled = false;
        }
    }
}