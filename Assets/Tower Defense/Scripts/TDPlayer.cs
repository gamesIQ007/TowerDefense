using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// Класс игрока в TowerDefense
    /// </summary>
    public class TDPlayer : Player
    {
        /// <summary>
        /// Золото
        /// </summary>
        [SerializeField] private int m_Gold;

        /// <summary>
        /// Изменить количество золота
        /// </summary>
        /// <param name="gold">Золото</param>
        public void ChangeGold(int gold)
        {
            m_Gold += gold;
        }
    }
}