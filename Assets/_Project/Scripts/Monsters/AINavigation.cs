using System.Collections.Generic;
using Grid;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Monsters
{
    public static class AINavigation
    {
        public static (Vector2 newDir, Vector3 newTarget) GetNextIntermediateTarget(Vector2 currentDir,
            Vector3 currentPos, Vector3 finalTargetPos)
        {
            GridManager grid = GridManager.Instance;

            List<Vector2> walkableDirs = new();

            foreach (Vector2 possibleDir in GetValidDirections())
            {
                if (Is180Turn(currentDir, possibleDir)) continue;

                if (grid.IsNeighborCellWalkable(currentPos, possibleDir))
                {
                    walkableDirs.Add(possibleDir);
                }
            }

            Vector3 intermediateTarget = currentPos;
            Vector2 nextDir = default;

            float minDistance = float.MaxValue;

            foreach (Vector2 dir in walkableDirs)
            {
                Vector3 neighborPosition = grid.GetNeighborCellPosition(currentPos, dir);
                float distanceToTarget = Vector3.Distance(neighborPosition, finalTargetPos);

                if (distanceToTarget < minDistance)
                {
                    minDistance = distanceToTarget;
                    intermediateTarget = neighborPosition;
                    nextDir = dir;
                }
            }

            //if (intermediateTarget == currentPos) throw new Exception("Check intermediate target navigation");
            return (nextDir, intermediateTarget);
        }

        public static bool Is180Turn(Vector2 oldDir, Vector2 newDir)
        {
            return newDir == -oldDir;
        }

        public static List<Vector2> GetValidDirections()
        {
            return new List<Vector2>()
            {
                new(1, 0),
                new(0, 1),
                new(-1, 0),
                new(0, -1)
            };
        }

        public static bool HasReachedTargetCellCenter(Vector2 dir, Vector3 currentPos, Vector3 targetPos)
        {
            GridManager grid = GridManager.Instance;

            Vector2 currentCellPos = grid.GetCellPosition(currentPos);
            Vector2 targetCellPos = grid.GetCellPosition(targetPos);

            if (currentCellPos != targetCellPos) return false;

            return grid.HasReachedCellCenterInDirection(dir, currentPos);
        }

        public static (Vector2 newDir, Vector3 newTarget) GetNextRandomTarget(Vector2 currentDir,
            Vector3 currentPos)
        {
            GridManager grid = GridManager.Instance;

            List<Vector2> walkableDirs = new();

            foreach (Vector2 possibleDir in GetValidDirections())
            {
                if (Is180Turn(currentDir, possibleDir)) continue;

                if (grid.IsNeighborCellAIWalkable(currentPos, possibleDir))
                {
                    walkableDirs.Add(possibleDir);
                }
            }

            Vector3 intermediateTarget = currentPos;

            Vector2 decidedDirection = walkableDirs[Random.Range(0, walkableDirs.Count)];

            intermediateTarget = grid.GetNeighborCellPosition(currentPos, decidedDirection);

            return (decidedDirection, intermediateTarget);
        }
    }
}