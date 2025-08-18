using UnityEngine;

namespace Level
{
    public interface IPortable
    {
        public void Teleport(Vector3 portalPosition, Vector3 exitDirection);
    }
}