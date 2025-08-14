using UnityEngine;

public class LevelManager : MonoBehaviour
{
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
        switch (CurrentState)
        {
            case MonsterLevelState.Scatter:
                if (_scatterTimer <= 0)
                {
                    ResetScatterTimer();
                    CurrentState = MonsterLevelState.Chase;
                    Debug.Log("Chase state");
                }

                break;
            case MonsterLevelState.Chase:
                if (_chaseTimer <= 0)
                {
                    ResetChaseTimer();
                    CurrentState = MonsterLevelState.Scatter;
                    Debug.Log("Scatter state");
                }

                break;
        }
    }
}


public enum MonsterLevelState
{
    Scatter = 0,
    Chase = 1
}