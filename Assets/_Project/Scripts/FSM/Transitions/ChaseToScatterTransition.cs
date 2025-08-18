using Level;
using UnityEngine;

namespace FSM.Transitions
{
    [CreateAssetMenu(fileName = "ChaseToScatterTransition", menuName = "Cookieman/FSM/Transitions/ChaseToScatter")]
    public class ChaseToScatterTransition : Transition
    {
        public override bool ShouldTransition(GameObject owner, StateContext context)
        {
            if (context.LevelManager.CurrentState == MonsterLevelState.Scatter)
            {
                return true;
            }
            
            return false;
        }
    }
}