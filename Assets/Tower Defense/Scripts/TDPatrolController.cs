using UnityEngine;
using UnityEngine.Events;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// ���������� �������������� �� ��������
    /// </summary>
    public class TDPatrolController : AIController
    {
        /// <summary>
        /// ����
        /// </summary>
        private Path m_Path;

        /// <summary>
        /// ������ ����� ����
        /// </summary>
        private int m_PathIndex;

        /// <summary>
        /// ������� ��� ���������� ����� ��������
        /// </summary>
        [SerializeField] private UnityEvent OnEndPath;

        /// <summary>
        /// ������ ����
        /// </summary>
        /// <param name="path">���������� ����</param>
        public void SetPath(Path newPath)
        {
            m_Path = newPath;
            m_PathIndex = 0;
            SetPatrolBehaviour(m_Path[m_PathIndex]);
        }

        /// <summary>
        /// ����� ��������� ����� ��������������
        /// </summary>
        protected override void GetNewPointPatrol()
        {
            m_PathIndex++;

            if (m_Path.Length > m_PathIndex)
            {
                SetPatrolBehaviour(m_Path[m_PathIndex]);
            }
            else
            {
                OnEndPath.Invoke();
                Destroy(gameObject);
            }
        }
    }
}