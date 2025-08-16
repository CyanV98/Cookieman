using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class StateContext
    {
        public readonly LevelManager LevelManager;

        public bool FrightenedTimeOutSet;
        
        public List<Vector3> ChamberPoints;
        public Vector3 CurrentChamberPoint => ChamberPoints[CurrentChamberPointIndex];
        public int CurrentChamberPointIndex = 0;

        
        public StateContext(LevelManager levelManager)
        {
            this.LevelManager = levelManager;
        }
    }
}