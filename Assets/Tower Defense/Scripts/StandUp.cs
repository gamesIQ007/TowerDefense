using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Поворот объекта по направлению вверх в глобальных координатах вне зависимости от родительского объекта
    /// </summary>
    public class StandUp : MonoBehaviour
    {
        /// <summary>
        /// ригит родителя
        /// </summary>
        private Rigidbody2D rig;

        /// <summary>
        /// Спрайт
        /// </summary>
        private SpriteRenderer sr;

        private void Start()
        {
            rig = transform.root.GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()
        {
            transform.up = Vector2.up;

            sr.flipX = rig.velocity.x > 0.01f ? false : true;
        }
    }
}