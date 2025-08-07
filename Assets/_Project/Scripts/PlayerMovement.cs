using System;
using Grid;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed = 3;

    private bool _shouldMove = false;
    private Vector2 _previousInputDir;
    private Vector2 _currentInputDir;

    private Vector2 _moveTarget;

    private GridManager _grid;

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
}