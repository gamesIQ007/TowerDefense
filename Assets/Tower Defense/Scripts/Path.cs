using UnityEngine;
using SpaceShooter;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefense
{
    /// <summary>
    /// ���� �� �����, ����� �������� ����� ������������ �����
    /// </summary>
    public class Path : MonoBehaviour
    {
        /// <summary>
        /// ��������� �����
        /// </summary>
        [SerializeField] private CircleArea m_StartArea;
        public CircleArea StartArea => m_StartArea;

        /// <summary>
        /// ������ ����� ����
        /// </summary>
        [SerializeField] private AIPointPatrol[] m_Points;

        /// <summary>
        /// ����� ������� �����
        /// </summary>
        public int Length => m_Points.Length;

        /// <summary>
        /// ���������� ����� ���� �� ������
        /// </summary>
        /// <param name="i">����� ����� ����</param>
        /// <returns>����� ����</returns>
        public AIPointPatrol this[int i] => m_Points[i];

#if UNITY_EDITOR
        /// <summary>
        /// ���� �����
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