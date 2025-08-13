using FSM;
using UnityEngine;

namespace Monsters.FSM
{
    [CreateAssetMenu(fileName = "new ScatterState", menuName = "Cookieman/FSM/State/Scatter")]
    public class ScatterState : State
    {
        public override void Enter(GameObject owner, StateContext context)
        {
            MonsterController monster = owner.GetComponent<MonsterController>();
            monster.FinalTarget = monster.Configuration.ScatterPosition;
        }

        public override void Tick(GameObject owner, StateContext context)
        {
        
        }

        public override void Exit(GameObject owner, StateContext context)
        {
        
        }
    }
}
