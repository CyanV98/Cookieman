using System.Collections.Generic;
using UnityEngine;

namespace Monsters.Debug
{
    public class MonsterDebugger : MonoBehaviour
    {
        [SerializeField] private PathArrow pathPrefab;
        [SerializeField,Min(1)] private int pathCount = 20;
    
        private MonsterController _monster;
        private readonly List<(Vector2 direction,Vector3 position)> _pathPositions = new();
        private readonly List<PathArrow> _pathVisualizers = new();
        private Color _pathColor;

        private Transform _parent;

        private void Start()
        {
            _monster = GetComponent<MonsterController>();
            _pathColor = _monster.Configuration.Color;
            _parent = new GameObject("Path Visualization").transform;
        }

        void Update()
        {
            CleanUp();
            UpdatePathProjection();
            UpdateVisualization();
        }

        private void UpdatePathProjection()
        {
            if(_monster.RandomMovement) return;
            
            Vector2 finalTarget = _monster.FinalTarget;
            (Vector2 newDir, Vector2 newTarget) nextResult = (_monster.CurrentDir, _monster.CurrentTarget);

            int counter = pathCount;
        
            while (finalTarget != nextResult.newTarget && counter > 0)
            {
                nextResult = AINavigation.GetNextIntermediateTarget(nextResult.newDir, nextResult.newTarget, finalTarget);
                _pathPositions.Add(nextResult);

                counter--;
            }
        }

        private void UpdateVisualization()
        {
            foreach ((Vector2 direction, Vector3 position) path in _pathPositions)
            {
                PathArrow newPathObject = Instantiate(pathPrefab, path.position, Quaternion.identity);
                newPathObject.transform.SetParent(_parent);
                _pathVisualizers.Add(newPathObject);
                
                newPathObject.Setup(_pathColor,path.direction);
            }
        }

        private void CleanUp()
        {
            _pathPositions.Clear();
            foreach (PathArrow path in _pathVisualizers)
            {
                if(path)
                    Destroy(path.gameObject);
            }
            _pathVisualizers.Clear();
        }

        private void OnDisable()
        {
            CleanUp();
        }
    }
}
