using Monsters;
using UnityEngine;

namespace FSM.Transitions
{
    [CreateAssetMenu(fileName = "EatenToChaseTransition", menuName = "Cookieman/FSM/Transitions/EatenToChase")]
    public class EatenToChaseTransition : Transition
    {
        public override bool ShouldTransition(GameObject owner, StateContext context)
        {
            MonsterController monsterController = owner.GetComponent<MonsterController>();
            
            if (!monsterController.IsEaten && context.LevelManager.CurrentState == MonsterLevelState.Chase)
            {
                return true;
            }
            return false;
        }
    }
}