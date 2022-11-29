using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShooter
{
    /// <summary>
    /// �������
    /// </summary>
    public class CircleArea : MonoBehaviour
    {
        /// <summary>
        /// ������
        /// </summary>
        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;

        /// <summary>
        /// �������� ��������� ����� ������ �������
        /// </summary>
        /// <returns>��������� ����� ������ �������</returns>
        public Vector2 GetRandomInsideZone()
        {
            return (Vector2)transform.position + (Vector2)Random.insideUnitSphere * m_Radius;
        }

#if UNITY_EDITOR
        /// <summary>
        /// ���� �����
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