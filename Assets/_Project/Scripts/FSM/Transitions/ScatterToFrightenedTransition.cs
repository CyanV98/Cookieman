using Level;
using UnityEngine;

namespace FSM.Transitions
{
    [CreateAssetMenu(fileName = "ScatterToFrightenedTransition", menuName = "Cookieman/FSM/Transitions/ScatterToFrightened")]
    public class ScatterToFrightenedTransition : Transition
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