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

        private Vector2 _finalTarget;

        private bool _randomMovement;

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

        public bool RandomMovement
        {
            get => _randomMovement;
            set => _randomMovement = value;
        }

        public virtual void SetChaseTarget()
        {
        }


        private void Start()
        {
            _grid = GridManager.Instance;

            FinalTarget = _grid.GetNonWalkableStartPosition();
            UpdateIntermediateTarget(transform.position, _finalTarget, false);
        }

        void Update()
        {
            if (AINavigation.HasReachedTargetCellCenter(_currentDir, transform.position, _currentTarget))
            {
                UpdateIntermediateTarget(transform.position, _finalTarget, _randomMovement);
            }

            UnityEngine.Debug.DrawRay(transform.position, (Vector3)_currentTarget - transform.position, Color.cyan);
            transform.position = Vector2.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);
        }

        private void UpdateIntermediateTarget(Vector3 currentPos, Vector3 finalTargetPos, bool randomMovement)
        {
            (Vector2 newDir, Vector3 newTarget) result;
            
            if (randomMovement)
                result = AINavigation.GetNextRandomTarget(_currentDir, currentPos);
            else
                result = AINavigation.GetNextIntermediateTarget(_currentDir, currentPos, finalTargetPos);

            CurrentDir = result.newDir;
            _currentTarget = result.newTarget;
        }
    }
}