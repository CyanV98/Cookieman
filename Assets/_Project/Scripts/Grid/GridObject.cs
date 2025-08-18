namespace Grid
{
    public class GridObject
    {
        private readonly GridCell _cellPosition;

        public GridCell GetCellPosition()
        {
            return _cellPosition;
        }

        public GridObjectType Type { get; set; } = GridObjectType.Empty;

        public GridObject(GridCell cellPosition)
        {
            _cellPosition = cellPosition;
        }

        public override string ToString()
        {
            return "GridObject: " + _cellPosition;
        }
    }

    public enum GridObjectType
    {
        Empty = 0,
        Wall = 1,
        Path = 2,
        Chamber = 3
    }
}