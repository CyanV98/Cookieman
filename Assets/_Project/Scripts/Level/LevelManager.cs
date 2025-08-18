using System;
using Game;
using UnityEngine;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        [field: SerializeField] public SuperCookieConfiguration SuperCookieConfiguration { get; private set; }
        [field: SerializeField] public PortalsConfiguration PortalsConfiguration { get; private set; }
        [SerializeField] private Portal portalPrefab;

        [SerializeField] private MonsterLevelState initialLevelState = MonsterLevelState.Scatter;
        [SerializeField] private float scatterTimerMax = 10f;
        [SerializeField] private float chaseTimerMax = 20f;
        [SerializeField] private float frightenedTimerMax = 10f;

        public MonsterLevelState CurrentState { get; private set; }

        private MonsterLevelState _lastState;

        private float _scatterTimer;
        private float _chaseTimer;
        public float FrightenedTimer { get; private set; }

        private void OnEnable()
        {
            GameEvents.OnSuperCookieEaten += SetToFrightened;
        }

        private void OnDisable()
        {
            GameEvents.OnSuperCookieEaten -= SetToFrightened;
        }

        private void Awake()
        {
            CurrentState = initialLevelState;
            Debug.Log($"Initial state: {CurrentState}");
            _lastState = CurrentState;
            ResetScatterTimer();
            ResetChaseTimer();
            ResetFrightenedTimer();
        }

        private void Start()
        {
            CreatePortals();
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
        private void DecreaseFrightenedTimer() => FrightenedTimer -= Time.deltaTime;

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
        }

        private void CreatePortals()
        {
            Portal portalOne = Instantiate(portalPrefab, PortalsConfiguration.PortalOne, Quaternion.identity);
            portalOne.ExitDirection = PortalsConfiguration.PortalTwoEntryDirection;
            portalOne.transform.SetParent(this.transform);

            Portal portalTwo = Instantiate(portalPrefab, PortalsConfiguration.PortalTwo, Quaternion.identity);
            portalTwo.ExitDirection = PortalsConfiguration.PortalOneEntryDirection;
            portalTwo.transform.SetParent(this.transform);
        }

        private void SetToFrightened()
        {
            CurrentState = MonsterLevelState.Frightened;
            Debug.Log("Frightened state");
        }
    }


    public enum MonsterLevelState
    {
        Scatter = 0,
        Chase = 1,
        Frightened = 2
    }
}