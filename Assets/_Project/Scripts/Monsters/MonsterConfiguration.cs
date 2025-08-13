using UnityEngine;

namespace Monsters
{
    [CreateAssetMenu(fileName = "new MonsterConfiguration", menuName = "Cookieman/Configurations/Monster")]
    public class MonsterConfiguration : ScriptableObject
    {
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public Vector3 ScatterPosition { get; private set; }
        [field: SerializeField] public Vector3 ChaseDefaultPosition { get; private set; }

    }
}
