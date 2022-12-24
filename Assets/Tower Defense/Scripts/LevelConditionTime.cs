using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// Условие завершения уровня по времени
    /// </summary>
    public class LevelConditionTime : MonoBehaviour, ILevelCondition
    {
        /// <summary>
        /// Время прохождения уровня
        /// </summary>
        [SerializeField] private float m_TimeLimit;

        /// <summary>
        /// Пройден ли уровень
        /// </summary>
        public bool IsCompleted => Time.time > m_TimeLimit;

        private void Start()
        {
            m_TimeLimit += Time.time;
        }
    }
}