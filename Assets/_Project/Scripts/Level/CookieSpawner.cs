using System.Linq;
using Grid;
using UnityEngine;

namespace Level
{
    public class CookieSpawner : MonoBehaviour //TODO Static
    {
        [SerializeField] private Cookie cookiePrefab;
        [SerializeField] private LevelManager levelManager;

        [SerializeField] private GridManager grid;

        private void Start()
        {
            SpawnCookie();
        }

        private void SpawnCookie()
        {
            foreach (GridObject gridObject in grid.GetGridObjects())
            {
                if (gridObject.Type == GridObjectType.Path)
                {
                    Vector3 cellPosition = grid.GetWorldPosition(gridObject.GetCellPosition());

                    if (levelManager.SuperCookieConfiguration.SuperCookiePositions.Contains(cellPosition))
                    {
                        Cookie cookie = Instantiate(levelManager.SuperCookieConfiguration.SuperCookiePrefab, cellPosition, Quaternion.identity);
                        cookie.transform.SetParent(this.transform);
                        continue;
                    }
                    else
                    {
                        if (IsPortalPosition(cellPosition)) continue;

                        Cookie cookie = Instantiate(cookiePrefab, cellPosition, Quaternion.identity);
                        cookie.transform.SetParent(this.transform);
                    }
                        
                }
            }
        }


        private bool IsPortalPosition(Vector3 cellPosition)
        {
            return cellPosition == levelManager.PortalsConfiguration.PortalOne || cellPosition == levelManager.PortalsConfiguration.PortalTwo;
        }
    }
}