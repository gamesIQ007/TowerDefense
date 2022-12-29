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
        private bool isCompleted;
        public bool IsCompleted => isCompleted;

        private void Start()
        {
            FindObjectOfType<EnemyWaveManager>().OnAllWavesDead += () =>
            {
                isCompleted = true;
            };
        }
    }
}