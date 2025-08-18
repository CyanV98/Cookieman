using System;
using Game;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private int startLives = 3;

        public int CurrentLives { get; private set; }

        private void OnEnable()
        {
            GameEvents.OnPlayerDead += HandlePlayerDead;
        }

        private void OnDisable()
        {
            GameEvents.OnPlayerDead -= HandlePlayerDead;
        }

        private void Awake()
        {
            CurrentLives = startLives;
        }

        private void HandlePlayerDead()
        {
            CurrentLives--;
        }
    }
}
