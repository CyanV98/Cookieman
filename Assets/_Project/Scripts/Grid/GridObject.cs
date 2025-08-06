using UnityEngine;

namespace Grid
{
    public class GridObject
    {
        private readonly Vector2Int _cellPosition;
        public GridObjectType Type { get; set; } = GridObjectType.Empty;

        public GridObject(Vector2Int cellPosition)
        {
            _cellPosition = cellPosition;
        }

        public override string ToString()
        {
            return "GridObject: " + _cellPosition;
        }

        public Vector2Int GetCellPosition()
        {
            return _cellPosition;
        }
    }

    public enum GridObjectType
    {
        Empty = 0,
        Wall = 1,
        Path = 2
    }
}