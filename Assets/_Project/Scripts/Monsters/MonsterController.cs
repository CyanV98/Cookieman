using System;
using Grid;
using UnityEngine;

namespace Monsters
{
    public class MonsterController : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private int speed = 3;
        [field: SerializeField] public MonsterConfiguration Configuration { get; private set; }

        public event Action<Vector2> OnDirectionChanged;
        
        private GridManager _grid;
        private Vector2 _currentTarget;
        private Vector2 _currentDir;
        
        public GetNextTarget GetNextIntermediateTarget { get; set; }

        private Vector2 _finalTarget;

        public Transform Player => player;

        public Vector2 FinalTarget
        {
            get => _finalTarget;
            set => _finalTarget = _grid.GetCellPosition(value);
        }

        public Vector2 CurrentTarget
        {
            get => _currentTarget;
            set => _currentTarget = value;
        }

        public Vector2 CurrentDir
        {
            get => _currentDir;
            set
            {
                _currentDir = value;
                OnDirectionChanged?.Invoke(value);
            }
        }

        public virtual void SetChaseTarget()
        {
        }


        private void Start()
        {
            _grid = GridManager.Instance;

            FinalTarget = _grid.GetNonWalkableStartPosition();
            GetNextIntermediateTarget = AINavigation.GetNextDefaultTarget;
            UpdateIntermediateTarget(transform.position, _finalTarget);
        }

        void Update()
        {
            if (AINavigation.HasReachedTargetCellCenter(_currentDir, transform.position, _currentTarget))
            {
                UpdateIntermediateTarget(transform.position, _finalTarget);
            }

            UnityEngine.Debug.DrawRay(transform.position, (Vector3)_currentTarget - transform.position, Color.cyan);
            transform.position = Vector2.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);
        }

        private void UpdateIntermediateTarget(Vector3 currentPos, Vector3 finalTargetPos)
        {
            (Vector2 newDir, Vector3 newTarget) result = GetNextIntermediateTarget.Invoke(_currentDir, currentPos, finalTargetPos);

            CurrentDir = result.newDir;
            _currentTarget = result.newTarget;
        }
    }
}