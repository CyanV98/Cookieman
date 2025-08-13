using UnityEngine;

namespace Grid
{
    public partial class GridManager
    {
        public bool HasReachedCellCenterInDirection(Vector2 dir, Vector3 currentPos)
        {
            return _gridRenderer.HasReachedCellCenterInDirection(dir, currentPos);
        }

        public bool IsNeighborCellWalkable(Vector3 currentPos, Vector2 direction)
        {
            GridCell neighborCell = GetNeighborCell(currentPos, direction);

            GridObject gridObj = _grid.GetGridObjects()[neighborCell.X, neighborCell.Y];
            return gridObj.Type == GridObjectType.Path;
        }

        public GridCell GetNeighborCell(Vector3 currentPos, Vector2 direction)
        {
            GridCell currentCell = _gridRenderer.GetCell(currentPos);
            Direction dir = _gridRenderer.GetDirection(direction);

            return _grid.GetNeighborCell(currentCell, dir);
        }

        public Vector3 GetNeighborCellPosition(Vector3 currentPos, Vector2 direction)
        {
            GridCell neighborCell = GetNeighborCell(currentPos, direction);

            return _gridRenderer.GetCellCenter(neighborCell);
        }

        public Vector3 GetCellPosition(Vector3 worldPos)
        {
            GridCell cell = _gridRenderer.GetCell(worldPos);
            return _gridRenderer.GetCellCenter(cell);
        }

        public Vector3 GetNonWalkableStartPosition()
        {
            return _gridRenderer.GetWorldPosition(new GridCell(0, 0));
        }
    }
}