using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "Portals Config", menuName = "Cookieman/Configurations/PortalsConfig")]
    public class PortalsConfiguration : ScriptableObject
    {
        [field:SerializeField] public Vector3 PortalOne {get; private set; }
        [field:SerializeField] public Vector3Int PortalOneEntryDirection  {get; private set; }
    
        [field:SerializeField] public Vector3 PortalTwo  {get; private set; }
        [field:SerializeField] public Vector3Int PortalTwoEntryDirection  {get; private set; }
    }
}
