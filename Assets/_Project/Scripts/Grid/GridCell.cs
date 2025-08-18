using UnityEngine;

namespace Grid
{
    public readonly struct GridCell
    {
        public int X { get; }
        public int Y { get; }

        public Vector2Int Position => new(X, Y);

        public GridCell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public GridCell(Vector2Int position)
        {
            X = position.x;
            Y = position.y;
        }
    }
}