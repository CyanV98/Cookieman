using Monsters;
using UnityEngine;

namespace FSM.States
{
    [CreateAssetMenu(fileName = "new ScatterState", menuName = "Cookieman/FSM/State/Scatter")]
    public class ScatterState : State
    {
        public override void Enter(GameObject owner, StateContext context)
        {
            MonsterController monster = owner.GetComponent<MonsterController>();
            
            monster.FinalTarget = monster.Configuration.ScatterPosition;
            owner.GetComponent<MonsterAnimator>().SetDefault(true);
            
            monster.GetNextIntermediateTarget = AINavigation.GetNextDefaultTarget;
        }

        public override void Tick(GameObject owner, StateContext context)
        {
        
        }

        public override void Exit(GameObject owner, StateContext context)
        {
            owner.GetComponent<MonsterAnimator>().SetDefault(false);
        }
    }
}
