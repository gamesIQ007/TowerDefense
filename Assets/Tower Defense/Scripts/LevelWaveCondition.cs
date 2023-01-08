using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// ������� ���������� ������ �� ��������� ���� ���� ������
    /// </summary>
    public class LevelWaveCondition : MonoBehaviour, ILevelCondition
    {
        
        /// <summary>
        /// ������� �� �������
        /// </summary>
        private bool m_IsCompleted;
        public bool IsCompleted => m_IsCompleted;

        private void Start()
        {
            FindObjectOfType<EnemyWaveManager>().OnAllWavesDead += () =>
            {
                m_IsCompleted = true;
            };
        }
    }
}