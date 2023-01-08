using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// ”словие завершени€ уровн€ по окончании всех волн врагов
    /// </summary>
    public class LevelWaveCondition : MonoBehaviour, ILevelCondition
    {
        
        /// <summary>
        /// ѕройден ли уровень
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