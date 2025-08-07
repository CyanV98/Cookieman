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

        public Vector3 GetWorldPosition(GridCell cell)
        {
            return new Vector3(cell.X, cell.Y, 0) + _origin;
        }
    }
}