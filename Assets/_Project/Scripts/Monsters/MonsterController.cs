using System;
using Grid;
using Level;
using UnityEngine;

namespace Monsters
{
    public class MonsterController : MonoBehaviour, IPortable
    {
        [SerializeField] private LevelManager level;
        [SerializeField] private Transform player;
        [SerializeField] private int speed = 3;
        [field: SerializeField] public MonsterConfiguration Configuration { get; private set; }

        public event Action<Vector2> OnDirectionChanged;
        
        public GetNextTarget GetNextIntermediateTarget { get; set; }

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
            private set
            {
                _currentDir = value;
                OnDirectionChanged?.Invoke(value);
            }
        }

        public virtual void SetChaseTarget()
        {
        }
        
        public bool IsEaten { get; set; }

        private GridManager _grid;
        private Vector2 _currentTarget;
        private Vector2 _currentDir;
        private Vector2 _finalTarget;

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

        public void Teleport(Vector3 portalPosition, Vector3 exitDirection)
        {
            Vector3 portalOne = _grid.GetCellPosition(level.portalsConfiguration.PortalOne);
            Vector3 portalTwo = _grid.GetCellPosition(level.portalsConfiguration.PortalTwo);

            portalPosition = _grid.GetCellPosition(portalPosition);


            if (portalOne == portalPosition)
            {
                Vector3 nextCellToPortal = _grid.GetNeighborCellPosition(portalTwo, exitDirection);
                transform.position = nextCellToPortal + exitDirection * 0.1f;
            }
            else if (portalTwo == portalPosition)
            {
                Vector3 nextCellToPortal = _grid.GetNeighborCellPosition(portalOne, exitDirection);
                transform.position = nextCellToPortal + exitDirection * 0.1f;
            }

            UpdateIntermediateTarget(transform.position, _finalTarget);
        }
    }
}