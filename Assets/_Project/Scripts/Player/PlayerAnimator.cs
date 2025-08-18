using Game;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator), typeof(PlayerMovement))]
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");
        private static readonly int Dead = Animator.StringToHash("PlayerDead");
        private Animator _animator;
        private PlayerMovement _playerMovement;

        private void OnEnable()
        {
            _playerMovement.OnDirectionChanged += HandleDirectionState;
            GameEvents.OnPlayerDead += PlayerDead;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void HandleDirectionState(Vector2 dir)
        {
            _animator.SetInteger(MoveX, (int)dir.x);
            _animator.SetInteger(MoveY, (int)dir.y);
        }

        private void PlayerDead()
        {
            _animator.SetTrigger(Dead);
        }

        private void OnDisable()
        {
            _playerMovement.OnDirectionChanged -= HandleDirectionState;
            GameEvents.OnPlayerDead -= PlayerDead;
        }
    }
}