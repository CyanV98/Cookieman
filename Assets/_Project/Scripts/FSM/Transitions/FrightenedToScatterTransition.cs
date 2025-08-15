using UnityEngine;

namespace FSM.Transitions
{
    [CreateAssetMenu(fileName = "FrightenedToScatterTransition", menuName = "Cookieman/FSM/Transitions/FrightenedToScatter")]
    public class FrightenedToScatterTransition : Transition
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