using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Статистика игрока
    /// </summary>
    public class PlayerStatistics
    {
        /// <summary>
        /// Количество убийств
        /// </summary>
        public int numKills;

        /// <summary>
        /// Очки
        /// </summary>
        public int score;

        /// <summary>
        /// Время
        /// </summary>
        public int time;

        /// <summary>
        /// Бонус
        /// </summary>
        public int bonus;

        public void Reset()
        {
            numKills = 0;
            score = 0;
            time = 0;
            bonus = 0;
        }
    }
}