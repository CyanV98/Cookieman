using System;
using Grid;
using Level;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour, IPortable
    {
        public event Action<Vector2> OnDirectionChanged;

        [SerializeField] private int speed = 3;

        public Vector2 Direction => _currentInputDir;

        private bool _shouldMove = false;
        private Vector2 _previousInputDir;
        private Vector2 _currentInputDir;

        private Vector2 _moveTarget;

        private GridManager _grid;
        [SerializeField] private LevelManager level;

        private void Start()
        {
            _grid = GridManager.Instance;

            _previousInputDir = Vector2.right;
            _currentInputDir = Vector2.right;
            _shouldMove = true;
        }

        private void Update()
        {
            if (!_shouldMove) return;

            if (!_grid.HasReachedCellCenterInDirection(_previousInputDir, transform.position))
                _moveTarget = GetTarget(_previousInputDir);
            else
                _moveTarget = GetTarget(_currentInputDir);
            Move();
        }

        private void Move()
        {
            Debug.DrawRay(transform.position, (Vector3)_moveTarget - transform.position, Color.cyan);
            transform.position = Vector2.MoveTowards(transform.position, _moveTarget, speed * Time.deltaTime);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started) return;

            if (context.phase == InputActionPhase.Canceled) return;

            Vector2 newDirection = context.ReadValue<Vector2>();
            if (ShouldIgnoreDir(newDirection)) return;

            // fix for oscillating on one axis
            HandleDirSwitch(newDirection);

            OnDirectionChanged?.Invoke(newDirection);
        }

        private void HandleDirSwitch(Vector2 newDirection)
        {
            // Direcion on perpendicular Axis
            _previousInputDir = _currentInputDir;
            _currentInputDir = newDirection;

            // Direction on same Axis
            if (ShouldSwitchDirection())
            {
                _previousInputDir = newDirection;
                _currentInputDir = newDirection;
            }
        }

        private bool ShouldSwitchDirection()
        {
            if (_previousInputDir == _currentInputDir) return false;
            if (Mathf.Abs(_previousInputDir.x) != Mathf.Abs(_currentInputDir.x) ||
                Mathf.Abs(_previousInputDir.y) != Mathf.Abs(_currentInputDir.y)) return false;

            return true;
        }

        private bool ShouldIgnoreDir(Vector2 dir)
        {
            // only accept 1 or -1
            if (Mathf.Abs(dir.x) == 1 && Mathf.Abs(dir.y) == 0) return false;
            if (Mathf.Abs(dir.x) == 0 && Mathf.Abs(dir.y) == 1) return false;
            return true;
        }

        private Vector3 GetTarget(Vector2 dir)
        {
            if (dir == Vector2.zero) return transform.position;

            if (_grid.IsNeighborCellWalkable(transform.position, dir))
                return _grid.GetNeighborCellPosition(transform.position, dir);

            return _grid.GetCellPosition(transform.position);
        }

        public void Teleport(Vector3 portalPosition, Vector3 exitDirection)
        {
            Vector3 portalOne = _grid.GetCellPosition(level.PortalsConfiguration.PortalOne);
            Vector3 portalTwo = _grid.GetCellPosition(level.PortalsConfiguration.PortalTwo);

            portalPosition = _grid.GetCellPosition(portalPosition);

            _currentInputDir = exitDirection;
            _previousInputDir = exitDirection;

            if (portalOne == portalPosition)
            {
                Vector3 nextCellToPortal = _grid.GetNeighborCellPosition(portalTwo, exitDirection);
                transform.position = nextCellToPortal + exitDirection * 0.1f;
                _moveTarget = _grid.GetNeighborCellPosition(nextCellToPortal, exitDirection);
            }
            else if (portalTwo == portalPosition)
            {
                Vector3 nextCellToPortal = _grid.GetNeighborCellPosition(portalOne, exitDirection);
                transform.position = nextCellToPortal + exitDirection * 0.1f;
                _moveTarget = _grid.GetNeighborCellPosition(nextCellToPortal, exitDirection);
            }

            OnDirectionChanged?.Invoke(_moveTarget);
        }
    }
}