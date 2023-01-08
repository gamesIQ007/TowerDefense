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
        private Rigidbody2D m_Rigidbody;

        /// <summary>
        /// Спрайт
        /// </summary>
        private SpriteRenderer m_SpriteRenderer;

        private void Start()
        {
            m_Rigidbody = transform.root.GetComponent<Rigidbody2D>();
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()
        {
            transform.up = Vector2.up;

            m_SpriteRenderer.flipX = m_Rigidbody.velocity.x > 0.01f ? false : true;
        }
    }
}