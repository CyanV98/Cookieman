using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "Super Cookie Configuration", menuName = "Cookieman/Configurations/SuperCookie")]
    public class SuperCookieConfiguration : ScriptableObject
    {
        [field: SerializeField] public Vector3[] SuperCookiePositions { get; private set; }
        [field: SerializeField] public Cookie SuperCookiePrefab { get; private set; }
        
    }
}