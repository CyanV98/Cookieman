using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ResetManager : MonoBehaviour
    {
        private List<ResettableBehavior> _objectsToReset = new();

        public void RegisterObjectsToReset(ResettableBehavior resettable)
        {
            _objectsToReset.Add(resettable);
        }
    }
}