using UnityEngine;

namespace FSM.Transitions
{
    [CreateAssetMenu(fileName = "ScatterToChaseTransition", menuName = "Cookieman/FSM/Transitions/ScatterToChase")]
    public class ScatterToChaseTransition : Transition
    {
        public override bool ShouldTransition(GameObject owner, StateContext context)
        {
            if (context.LevelManager.CurrentState == MonsterLevelState.Chase)
            {
                return true;
            }
            
            return false;
        }
    }
}