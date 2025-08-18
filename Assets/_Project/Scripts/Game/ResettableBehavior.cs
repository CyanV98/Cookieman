using UnityEngine;

namespace Game
{
    public abstract class ResettableBehavior : MonoBehaviour
    {
        private ResetManager _resetManager;

        private void Awake()
        {
            _resetManager = FindFirstObjectByType<ResetManager>();
            RegisterReset();
        }

        private void RegisterReset()
        {
            _resetManager.RegisterObjectsToReset(this);
        }

        public abstract void Reset();
    }
}