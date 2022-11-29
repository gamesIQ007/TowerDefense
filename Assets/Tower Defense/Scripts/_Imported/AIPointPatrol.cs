using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ������� ��������������
    /// </summary>
    public class AIPointPatrol : MonoBehaviour
    {
        /// <summary>
        /// ������ �������
        /// </summary>
        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;

        private static readonly Color GizmoColor = new Color(1, 0, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = GizmoColor;
            Gizmos.DrawSphere(transform.position, m_Radius);
        }
    }
}