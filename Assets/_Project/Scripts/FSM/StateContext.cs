using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class StateContext
    {
        public float Timer = 0;
        
        public List<Vector3> ChamberPoints;
        public Vector3 CurrentChamberPoint => ChamberPoints[CurrentChamberPointIndex];
        public int CurrentChamberPointIndex = 0;
        public int ChamberPointCount => ChamberPoints.Count - 1;
    }
}