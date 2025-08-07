using System;

namespace Grid
{
    public class GameGrid
    {
        private readonly int _width;
        private readonly int _height;
        private readonly GridObject[,] _gridObjects;

        public int Width => _width;
        public int Height => _height;

        public GridObject[,] GetGridObjects()
        {
            return _gridObjects;
        }

        public GameGrid(int width, int height)
        {
            _width = width;
            _height = height;

            _gridObjects = new GridObject[width, height];
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            for (int x = 0; x < _width; x++)
            for (int y = 0; y < _height; y++)
            {
                GridCell cellPosition = new(x, y);
                _gridObjects[x, y] = new GridObject(cellPosition);
            }
        }

        public bool IsValidCell(GridCell cellToCheck)
        {
            if (cellToCheck.X > _width || cellToCheck.Y > _height) return false;
            if (cellToCheck.X < 0 || cellToCheck.Y < 0) return false;

            return true;
        }

        public GridCell GetNeighborCell(GridCell currentCell, Direction direction)
        {
            GridCell neighborCell;

            switch (direction)
            {
                case Direction.Up:
                    neighborCell = new GridCell(currentCell.X, currentCell.Y + 1);
                    break;
                case Direction.Right:
                    neighborCell = new GridCell(currentCell.X + 1, currentCell.Y);
                    break;
                case Direction.Down:
                    neighborCell = new GridCell(currentCell.X, currentCell.Y - 1);
                    break;
                case Direction.Left:
                    neighborCell = new GridCell(currentCell.X - 1, currentCell.Y);
                    break;
                default:
                    throw new Exception("Invalid direction in GetNeighborCell");
            }

            if (!IsValidCell(neighborCell)) throw new Exception("Neighbor Cell is outside of the grid");

            return neighborCell;
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        Invalid
    }
}