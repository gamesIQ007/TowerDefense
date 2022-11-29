using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShooter
{
    /// <summary>
    /// Область
    /// </summary>
    public class CircleArea : MonoBehaviour
    {
        /// <summary>
        /// Радиус
        /// </summary>
        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;

        /// <summary>
        /// Получить случайную точку внутри области
        /// </summary>
        /// <returns>Случайная точка внутри области</returns>
        public Vector2 GetRandomInsideZone()
        {
            return (Vector2)transform.position + (Vector2)Random.insideUnitSphere * m_Radius;
        }

#if UNITY_EDITOR
        /// <summary>
        /// Цвет гизмо
        /// </summary>
        private static Color GizmoColor = new Color(0, 1, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Handles.color = GizmoColor;
            Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius);
        }
#endif

    }
}