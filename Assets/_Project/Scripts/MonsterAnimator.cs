using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MonsterAnimator : MonoBehaviour
{
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");
    private static readonly int Default = Animator.StringToHash("IsDefault");
    private static readonly int Eaten = Animator.StringToHash("IsEaten");
    private static readonly int Frightened = Animator.StringToHash("IsFrightened");
    private static readonly int FrightenedTimeout = Animator.StringToHash("FrightenedTimeout");
    

    private Animator _animator;
    
    private void OnEnable()
    {
       
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void HandleDirectionState(Vector2 dir)
    {
        _animator.SetInteger(MoveX, (int)dir.x);
        _animator.SetInteger(MoveY, (int)dir.y);
    }

    private void SetDefault(bool isDefault)
    {
        _animator.SetBool(Default, isDefault);
    }

    private void SetEaten(bool isEaten)
    {
        _animator.SetBool(Eaten, isEaten);
    }

    private void SetFrightened(bool isFrightened)
    {
        _animator.SetBool(Frightened, isFrightened);
    }

    private void SetFrightenedTimeout()
    {
        _animator.SetTrigger(FrightenedTimeout);
    }

    private void OnDisable()
    {

    }
}
