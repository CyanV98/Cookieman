using UnityEngine;

namespace Grid
{
    public class GridRenderer
    {
        private readonly Vector3 _origin;

        public GridRenderer(Vector3 origin)
        {
            _origin = origin;
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y, 0) + _origin;
        }
    }
}