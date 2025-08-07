namespace Grid
{
    public class GameGrid
    {
        private readonly GridObject[,] _gridObjects;
        
        public int Width { get; }
        public int Height { get; }
        
        public GameGrid(int width, int height)
        {
            Width = width;
            Height = height;

            _gridObjects = new GridObject[width, height];
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
            {
                GridCell cellPosition = new(x, y);
                _gridObjects[x, y] = new GridObject(cellPosition);
            }
        }

        public GridObject[,] GetGridObjects()
        {
            return _gridObjects;
        }
    }
}