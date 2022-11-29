using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Общая статистика игр
    /// </summary>
    public class TotalStatistics
    {
        /// <summary>
        /// Количество убийств
        /// </summary>
        public int totalNumKills;

        /// <summary>
        /// Очки
        /// </summary>
        public int totalScore;

        /// <summary>
        /// Время
        /// </summary>
        public int totalTime;

        public void Reset()
        {
            totalNumKills = 0;
            totalScore = 0;
            totalTime = 0;
        }
    }
}