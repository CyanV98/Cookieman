using Level;
using UnityEngine;

namespace FSM.Transitions
{
    [CreateAssetMenu(fileName = "ChaseToFrightenedTransition", menuName = "Cookieman/FSM/Transitions/ChaseToFrightened")]
    public class ChaseToFrightenedTransition : Transition
    {
        public override bool ShouldTransition(GameObject owner, StateContext context)
        {
            if (context.LevelManager.CurrentState == MonsterLevelState.Frightened)
            {
                return true;
            }
            return false;
        }
    }
}