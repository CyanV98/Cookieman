using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Grid
{
    [ExecuteAlways]
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;

        private GameGrid _grid;
        private GridRenderer _gridRenderer;
        private bool _showGrid;

        public static GridManager Instance { get; private set; }

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
            if (!tilemap) return;
            tilemap.CompressBounds();
            _grid = new GameGrid(tilemap.size.x, tilemap.size.y);
            _gridRenderer = new GridRenderer(tilemap.origin);
        }

        public void RegenerateGrid()
        {
            InitializeGrid();
        }

        public void ToggleVisibility()
        {
            _showGrid = !_showGrid;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (tilemap == null || _grid == null || _showGrid == false) return;

            Gizmos.color = Color.cyan;

            GridObject[,] gridObjects = _grid.GetGridObjects();

            foreach (GridObject gridObject in gridObjects)
            {
                int cellPosX = gridObject.GetCellPosition().x;
                int cellPosY = gridObject.GetCellPosition().y;

                int xPos = (int)_gridRenderer.GetWorldPosition(cellPosX, cellPosY).x;
                int yPos = (int)_gridRenderer.GetWorldPosition(cellPosX, cellPosY).y;

                Vector3 startVertical = new(xPos, yPos);
                Vector3 endVertical = new(xPos, yPos + 1);
                Gizmos.DrawLine(startVertical, endVertical);

                Vector3 startHorizontal = new(xPos, yPos);
                Vector3 endHorizontal = new(xPos + 1, yPos);
                Gizmos.DrawLine(startHorizontal, endHorizontal);

                // Draw Grid Position Text
                GUIStyle textStyle = new();
                textStyle.normal.textColor = Color.white;
                textStyle.alignment = TextAnchor.MiddleCenter;

                Vector3 cellCenter = new(xPos + 0.5f, yPos + 0.5f);
                Handles.Label(cellCenter, $"({cellPosX}, {cellPosY})", textStyle);
            }

            int tilemapWidth = _grid.Width;
            int tilemapHeight = _grid.Height;

            Vector3 tilemapEndWorldSpace = _gridRenderer.GetWorldPosition(tilemapWidth, tilemapHeight);

            float originWorldSpaceX = _gridRenderer.GetWorldPosition(0, 0).x;
            float originWorldSpaceY = _gridRenderer.GetWorldPosition(0, 0).y;

            float widthWorldSpace = tilemapEndWorldSpace.x;
            float heightWorldSpace = tilemapEndWorldSpace.y;

            // Draw one more row/column
            Vector3 finalStartVertical = new(widthWorldSpace, originWorldSpaceY);
            Vector3 finalEndVertical = new(widthWorldSpace, heightWorldSpace);
            Gizmos.DrawLine(finalStartVertical, finalEndVertical);

            Vector3 finalStartHorizontal = new(originWorldSpaceX, heightWorldSpace);
            Vector3 finalEndHorizontal = new(widthWorldSpace, heightWorldSpace);
            Gizmos.DrawLine(finalStartHorizontal, finalEndHorizontal);
        }
#endif
    }
}