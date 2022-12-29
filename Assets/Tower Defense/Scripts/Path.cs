using UnityEngine;
using SpaceShooter;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefense
{
    /// <summary>
    /// Путь из точек, между которыми будут перемещаться враги
    /// </summary>
    public class Path : MonoBehaviour
    {
        /// <summary>
        /// Стартовая точка
        /// </summary>
        [SerializeField] private CircleArea m_StartArea;
        public CircleArea StartArea => m_StartArea;

        /// <summary>
        /// Массив точек пути
        /// </summary>
        [SerializeField] private AIPointPatrol[] m_Points;

        /// <summary>
        /// Длина массива точек
        /// </summary>
        public int Length => m_Points.Length;

        /// <summary>
        /// Конкретная точка пути по номеру
        /// </summary>
        /// <param name="i">Номер точки пути</param>
        /// <returns>Точка пути</returns>
        public AIPointPatrol this[int i] => m_Points[i];

#if UNITY_EDITOR
        /// <summary>
        /// Цвет гизмо
        /// </summary>
        private static Color GizmoColor = new Color(0, 1, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Handles.color = GizmoColor;
            for (int i = 0; i < Length; i++)
            {
                Handles.DrawSolidDisc(m_Points[i].transform.position, transform.forward, m_Points[i].Radius);
            }
        }
#endif
    }
}