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