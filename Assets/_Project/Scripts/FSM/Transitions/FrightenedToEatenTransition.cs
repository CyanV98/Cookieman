using Monsters;
using UnityEngine;

namespace FSM.Transitions
{
    [CreateAssetMenu(fileName = "FrightenedToEatenTransition", menuName = "Cookieman/FSM/Transitions/FrightenedToEaten")]
    public class FrightenedToEatenTransition : Transition
    {
        public override bool ShouldTransition(GameObject owner, StateContext context)
        {
            if (owner.GetComponent<MonsterController>().IsEaten)
            {
                return true;
            }
            return false;
        }
    }
}