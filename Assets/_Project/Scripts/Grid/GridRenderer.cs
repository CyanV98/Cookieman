using UnityEngine;

namespace Grid
{
    public class GridRenderer
    {
        private readonly Vector3 _origin;
        private readonly float _gridSize;
        private readonly GameGrid _grid;

        public GridRenderer(Vector3 origin, GameGrid grid, float gridSize)
        {
            _origin = origin;
            _grid = grid;
            _gridSize = gridSize;
        }

        public Vector3 GetWorldPosition(GridCell cell)
        {
            return new Vector3(cell.X, cell.Y, 0) + _origin;
        }

        public GridCell GetCell(Vector3 worldPosition)
        {
            Vector3 originReverted = worldPosition - _origin;
            GridCell cellCandidate = new(Mathf.FloorToInt(originReverted.x), Mathf.FloorToInt(originReverted.y));
            
            return _grid.ConvertToValidCell(cellCandidate);
        }

        public Vector3 GetCellCenter(GridCell cell)
        {
            return GetWorldPosition(cell) + new Vector3(_gridSize / 2, _gridSize / 2, 0);
        }

        public Direction GetDirection(Vector2 directionVector)
        {
            if (directionVector == Vector2.up) return Direction.Up;
            if (directionVector == Vector2.down) return Direction.Down;
            if (directionVector == Vector2.left) return Direction.Left;
            if (directionVector == Vector2.right) return Direction.Right;
            return Direction.Invalid;
        }

        public Vector3 GetPosInGridCoordinates(Vector3 worldPosition)
        {
            return worldPosition - _origin;
        }

        public bool HasReachedCellCenterInDirection(Vector2 dir, Vector3 currentPos)
        {
            Direction direction = GetDirection(dir);

            Vector3 posInGridCorrdinates = GetPosInGridCoordinates(currentPos);
            float xDistanceFromCellStart = posInGridCorrdinates.x - Mathf.Floor(posInGridCorrdinates.x);
            float yDistanceFromCellStart = posInGridCorrdinates.y - Mathf.Floor(posInGridCorrdinates.y);

            float distanceToCenter = _gridSize / 2;

            switch (direction)
            {
                case Direction.Up: return yDistanceFromCellStart >= distanceToCenter;
                case Direction.Down: return yDistanceFromCellStart <= distanceToCenter;
                case Direction.Right: return xDistanceFromCellStart >= distanceToCenter;
                case Direction.Left: return xDistanceFromCellStart <= distanceToCenter;
            }

            return false;
        }
    }
}