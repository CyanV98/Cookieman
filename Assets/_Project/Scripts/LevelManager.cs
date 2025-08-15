using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //TODO to private
    public bool isFrightened = false;

    [SerializeField] private MonsterLevelState initialLevelState = MonsterLevelState.Scatter;
    [SerializeField] private float scatterTimerMax = 10f;
    [SerializeField] private float chaseTimerMax = 20f;
    [SerializeField] private float frightenedTimerMax = 10f;

    public MonsterLevelState CurrentState { get; private set; }

    private MonsterLevelState _lastState;
    
    private float _scatterTimer;
    private float _chaseTimer;
    public float FrightenedTimer { get; private set; }

    private void Awake()
    {
        CurrentState = initialLevelState;
        _lastState = CurrentState;
        ResetScatterTimer();
        ResetScatterTimer();
    }

    private void Update()
    {
        switch (CurrentState)
        {
            case MonsterLevelState.Scatter:
                DecreaseScatterTimer();
                break;
            case MonsterLevelState.Chase:
                DecreaseChaseTimer();
                break;
            case MonsterLevelState.Frightened:
                DecreaseFrightenedTimer();
                break;
        }

        CheckStateChange();
    }

    private void ResetScatterTimer() => _scatterTimer = scatterTimerMax;
    private void ResetChaseTimer() => _chaseTimer = chaseTimerMax;
    private void ResetFrightenedTimer() => FrightenedTimer = frightenedTimerMax;

    private void DecreaseScatterTimer() => _scatterTimer -= Time.deltaTime;
    private void DecreaseChaseTimer() => _chaseTimer -= Time.deltaTime;
    private void DecreaseFrightenedTimer() => FrightenedTimer = FrightenedTimer - Time.deltaTime;

    private void CheckStateChange()
    {
        if (CurrentState == MonsterLevelState.Scatter && _scatterTimer <= 0)
        {
            ResetScatterTimer();
            _lastState = CurrentState;
            CurrentState = MonsterLevelState.Chase;
            Debug.Log("Chase state");
            return;
        }

        if (CurrentState == MonsterLevelState.Chase && _chaseTimer <= 0)
        {
            ResetChaseTimer();
            _lastState = CurrentState;
            CurrentState = MonsterLevelState.Scatter;
            Debug.Log("Scatter state");
            return;
        }

        if (CurrentState == MonsterLevelState.Frightened && FrightenedTimer <= 0)
        {
            ResetFrightenedTimer();
            CurrentState = _lastState;
            return;
        }

        if (isFrightened && CurrentState != MonsterLevelState.Frightened)
        {
            CurrentState = MonsterLevelState.Frightened;
            Debug.Log("Frightened state");
            return;
        }
    }
}


public enum MonsterLevelState
{
    Scatter = 0,
    Chase = 1,
    Frightened = 2
}