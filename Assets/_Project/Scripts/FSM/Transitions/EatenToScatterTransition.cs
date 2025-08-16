using Monsters;
using UnityEngine;

namespace FSM.Transitions
{
    [CreateAssetMenu(fileName = "EatenToScatterTransition", menuName = "Cookieman/FSM/Transitions/EatenToScatter")]
    public class EatenToScatterTransition : Transition
    {
        public override bool ShouldTransition(GameObject owner, StateContext context)
        {
            MonsterController monsterController = owner.GetComponent<MonsterController>();
            
            if (!monsterController.IsEaten && context.LevelManager.CurrentState == MonsterLevelState.Scatter)
            {
                return true;
            }
            return false;
        }
    }
}