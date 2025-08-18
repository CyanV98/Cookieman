using UnityEngine;

namespace Monsters.Debug
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PathArrow : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Setup(Color color, Vector2 direction)
        {
            spriteRenderer.color = color;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}