using Level;
using UnityEngine;

namespace FSM.Transitions
{
    [CreateAssetMenu(fileName = "FrightenedToChaseTransition", menuName = "Cookieman/FSM/Transitions/FrightenedToChase")]
    public class FrightenedToChaseTransition : Transition
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