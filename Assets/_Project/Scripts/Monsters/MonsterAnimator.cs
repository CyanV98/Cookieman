using UnityEngine;

namespace Monsters
{
    [RequireComponent(typeof(Animator))]
    public class MonsterAnimator : MonoBehaviour
    {
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");
        private static readonly int Default = Animator.StringToHash("IsDefault");
        private static readonly int Eaten = Animator.StringToHash("IsEaten");
        private static readonly int Frightened = Animator.StringToHash("IsFrightened");
        private static readonly int FrightenedTimeout = Animator.StringToHash("FrightenedTimeout");
        private static readonly int FrightenedEnter = Animator.StringToHash("FrightenedEnter");


        private Animator _animator;
        private MonsterController _monsterController;
    
        private void OnEnable()
        {
            _monsterController.OnDirectionChanged += HandleDirectionState;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _monsterController = GetComponent<MonsterController>();
        }

        private void HandleDirectionState(Vector2 dir)
        {
            _animator.SetInteger(MoveX, (int)dir.x);
            _animator.SetInteger(MoveY, (int)dir.y);
        }

        public void SetDefault(bool isDefault)
        {
            _animator.SetBool(Default, isDefault);
        }

        public void SetEaten(bool isEaten)
        {
            _animator.SetBool(Eaten, isEaten);
        }

        public void SetFrightened(bool isFrightened)
        {
            _animator.SetBool(Frightened, isFrightened);
        }

        public void EnterFrightened()
        {
            SetFrightened(true);
            _animator.SetTrigger(FrightenedEnter);
        }

        public void EnterFrightenedTimeout()
        {
            _animator.SetTrigger(FrightenedTimeout);
        }

        private void OnDisable()
        {
            _monsterController.OnDirectionChanged -= HandleDirectionState;
        }
    }
}
