using Monsters;
using UnityEngine;

namespace FSM.States
{
    [CreateAssetMenu(fileName = "new FrightenedState", menuName = "Cookieman/FSM/State/Frightened")]
    public class FrightenedState : State
    {
        public override void Enter(GameObject owner, StateContext context)
        {
            owner.GetComponent<MonsterController>().RandomMovement = true;
        }

        public override void Tick(GameObject owner, StateContext context)
        {
            
        }

        public override void Exit(GameObject owner, StateContext context)
        {
            owner.GetComponent<MonsterController>().RandomMovement = false;
        }
    }
}