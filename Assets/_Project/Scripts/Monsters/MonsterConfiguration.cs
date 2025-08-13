using UnityEngine;

namespace Monsters
{
    [CreateAssetMenu(fileName = "new MonsterConfiguration", menuName = "Cookieman/Configurations/Monster")]
    public class MonsterConfiguration : ScriptableObject
    {
        [SerializeField] private Color color;

        public Color Color => color;
    }
}
