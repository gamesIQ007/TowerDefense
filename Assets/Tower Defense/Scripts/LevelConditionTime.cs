using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// ������� ���������� ������ �� �������
    /// </summary>
    public class LevelConditionTime : MonoBehaviour, ILevelCondition
    {
        /// <summary>
        /// ����� ����������� ������
        /// </summary>
        [SerializeField] private float m_TimeLimit;

        /// <summary>
        /// ������� �� �������
        /// </summary>
        public bool IsCompleted => Time.time > m_TimeLimit;

        private void Start()
        {
            m_TimeLimit += Time.time;
        }
    }
}