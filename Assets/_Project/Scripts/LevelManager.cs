using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //TODO to private
    public bool isFrightened = false;

    [SerializeField] private MonsterLevelState initialLevelState = MonsterLevelState.Scatter;
    [SerializeField] private float scatterTimerMax = 10f;
    [SerializeField] private float chaseTimerMax = 20f;

    public MonsterLevelState CurrentState { get; private set; }
    private float _scatterTimer;
    private float _chaseTimer;

    private void Awake()
    {
        CurrentState = initialLevelState;
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
        }

        CheckStateChange();
    }

    private void ResetScatterTimer() => _scatterTimer = scatterTimerMax;

    private void ResetChaseTimer() => _chaseTimer = chaseTimerMax;

    private void DecreaseScatterTimer() => _scatterTimer -= Time.deltaTime;

    private void DecreaseChaseTimer() => _chaseTimer -= Time.deltaTime;

    private void CheckStateChange()
    {
        if (CurrentState == MonsterLevelState.Scatter && _scatterTimer <= 0)
        {
            ResetScatterTimer();
            CurrentState = MonsterLevelState.Chase;
            Debug.Log("Chase state");
        }

        if (CurrentState == MonsterLevelState.Chase && _chaseTimer <= 0)
        {
            ResetChaseTimer();
            CurrentState = MonsterLevelState.Scatter;
            Debug.Log("Scatter state");
        }

        if (isFrightened && CurrentState != MonsterLevelState.Frightened)
        {
            CurrentState = MonsterLevelState.Frightened;
            Debug.Log("Frightened state");
        }
    }
}


public enum MonsterLevelState
{
    Scatter = 0,
    Chase = 1,
    Frightened = 2
}