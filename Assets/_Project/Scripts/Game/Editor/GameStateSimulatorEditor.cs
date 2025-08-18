using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    [CustomEditor(typeof(GameStateSimulator))]
    public class GameStateSimulatorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        
            GameStateSimulator gameStateSimulator = (GameStateSimulator)target;

            if (GUILayout.Button("Simulate Game Start"))
            {
                Debug.Log("Start");
                GameEvents.StartGame();
            }
        
            if (GUILayout.Button("Simulate Game Won"))
            {
                Debug.Log("Game Won");
                GameEvents.GameWon();
            }
        
            if (GUILayout.Button("Simulate Game Over"))
            {
                Debug.Log("Game Over");
                GameEvents.GameOver();
            }
        
            if (GUILayout.Button("Simulate Game Restart"))
            {
                Debug.Log("Restart");
                GameEvents.Restart();
            }
        }
    }
}