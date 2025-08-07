using System;
using Tiles;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Grid
{
    [ExecuteAlways]
    public partial class GridManager : MonoBehaviour //TODO Remove after DI
    {
        public static GridManager Instance { get; private set; } //TODO Remove after DI

        [SerializeField] private Tilemap tilemap;

        private GameGrid _grid;
        private GridRenderer _gridRenderer;
        private bool _showGrid;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            InitializeGrid();
        }

        private void InitializeGrid()
        {
            if (tilemap == null) return;
            tilemap.CompressBounds();

            _grid = new GameGrid(tilemap.size.x, tilemap.size.y);
            _gridRenderer = new GridRenderer(tilemap.origin, _grid, tilemap.cellSize.x);

            foreach (GridObject gridObject in _grid.GetGridObjects())
            {
                Vector3 objectWorldPosition = _gridRenderer.GetWorldPosition(gridObject.GetCellPosition());

                int xVal = Mathf.FloorToInt(objectWorldPosition.x);
                int yVal = Mathf.FloorToInt(objectWorldPosition.y);
                TileBase tile = tilemap.GetTile(new Vector3Int(xVal, yVal, 0));

                if (tile == null) continue;

                Type tyleType = tile.GetType();

                if (tyleType == typeof(Wall)) gridObject.Type = GridObjectType.Wall;

                if (tyleType == typeof(Path)) gridObject.Type = GridObjectType.Path;
            }
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            if (tilemap == null || _grid == null || _showGrid == false) return;

            Gizmos.color = Color.cyan;

            GridObject[,] gridObjects = _grid.GetGridObjects();

            foreach (GridObject gridObject in gridObjects)
            {
                GridCell currentCell = gridObject.GetCellPosition();

                Vector3 cellWorldPosition = _gridRenderer.GetWorldPosition(currentCell);

                int xPos = (int)cellWorldPosition.x;
                int yPos = (int)cellWorldPosition.y;

                Vector3 startVertical = new(xPos, yPos);
                Vector3 endVertical = new(xPos, yPos + 1);
                Gizmos.DrawLine(startVertical, endVertical);

                Vector3 startHorizontal = new(xPos, yPos);
                Vector3 endHorizontal = new(xPos + 1, yPos);
                Gizmos.DrawLine(startHorizontal, endHorizontal);

                Vector3 cellCenter = new(xPos + 0.5f, yPos + 0.5f);

                if (gridObject.Type == GridObjectType.Wall)
                {
                    // Mark Wall Cells
                    GUIStyle wallTextStyle = new();
                    wallTextStyle.normal.textColor = Color.orange;
                    wallTextStyle.fontSize = 12;
                    wallTextStyle.fontStyle = FontStyle.Bold;
                    wallTextStyle.alignment = TextAnchor.MiddleCenter;
                    Handles.Label(cellCenter, $"Wall", wallTextStyle);
                }

                if (gridObject.Type == GridObjectType.Path)
                {
                    // Draw Grid Position Text
                    GUIStyle textStyle = new();
                    textStyle.normal.textColor = Color.white;
                    textStyle.alignment = TextAnchor.MiddleCenter;
                    Handles.Label(cellCenter, $"({currentCell.X}, {currentCell.Y})", textStyle);
                }
            }

            int tilemapWidth = _grid.Width;
            int tilemapHeight = _grid.Height;

            Vector3 firstCellWorldSpace = _gridRenderer.GetWorldPosition(new GridCell(0, 0));
            Vector3 lastCellWorldSpace = _gridRenderer.GetWorldPosition(new GridCell(tilemapWidth, tilemapHeight));

            float originWorldSpaceX = firstCellWorldSpace.x;
            float originWorldSpaceY = firstCellWorldSpace.y;

            float widthWorldSpace = lastCellWorldSpace.x;
            float heightWorldSpace = lastCellWorldSpace.y;

            // Draw one more row/column
            Vector3 finalStartVertical = new(widthWorldSpace, originWorldSpaceY);
            Vector3 finalEndVertical = new(widthWorldSpace, heightWorldSpace);
            Gizmos.DrawLine(finalStartVertical, finalEndVertical);

            Vector3 finalStartHorizontal = new(originWorldSpaceX, heightWorldSpace);
            Vector3 finalEndHorizontal = new(widthWorldSpace, heightWorldSpace);
            Gizmos.DrawLine(finalStartHorizontal, finalEndHorizontal);
        }

#endif

        public void RegenerateGrid()
        {
            InitializeGrid();
        }

        public void ToggleVisibility()
        {
            _showGrid = !_showGrid;
        }
    }
}