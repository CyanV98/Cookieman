using System.Collections.Generic;
using Grid;
using Monsters;
using UnityEngine;

namespace FSM.States
{
    [CreateAssetMenu(fileName = "new EatenState", menuName = "Cookieman/FSM/State/Eaten")]
    public class EatenState : State
    {
        public override void Enter(GameObject owner, StateContext context)
        {
            owner.GetComponent<MonsterAnimator>().SetEaten(true);

            MonsterController monsterController = owner.GetComponent<MonsterController>();
            List<Vector3> configuredPositions = monsterController.Configuration.ChamberPoints;

            if (configuredPositions.Count > 0)
            {
                context.ChamberPoints = configuredPositions;
                context.CurrentChamberPointIndex = 0;
            }
            else
            {
                throw new System.Exception("No chamber points");
            }
            
            monsterController.FinalTarget = context.CurrentChamberPoint;

            monsterController.GetNextIntermediateTarget = AINavigation.GetNextEatenTarget;
        }

        public override void Tick(GameObject owner, StateContext context)
        {
            if (HasReachedFinalPosition(context)) return;
            
            MonsterController monsterController = owner.GetComponent<MonsterController>();
            
            Vector3 monsterPosition = owner.transform.position;
                
            if (Vector2.Distance(monsterPosition, context.CurrentChamberPoint) < 0.1f)
            {
                context.CurrentChamberPointIndex++;
                
                if (HasReachedFinalPosition(context))
                {
                    monsterController.IsEaten = false;
                    return;
                }
                
                monsterController.FinalTarget = context.CurrentChamberPoint;
                
            }
        }

        public override void Exit(GameObject owner, StateContext context)
        {
            owner.GetComponent<MonsterAnimator>().SetEaten(false);
        }

        private bool HasReachedFinalPosition(StateContext context)
        {
            return context.CurrentChamberPointIndex == context.ChamberPoints.Count;
        }
    }
}