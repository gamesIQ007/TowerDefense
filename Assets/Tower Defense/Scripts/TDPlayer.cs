using System;
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
        /// Инстанс для ТДигрока
        /// </summary>
        public static new TDPlayer Instance
        {
            get { return Player.Instance as TDPlayer; }
        }

        /// <summary>
        /// Системное событие на изменение золота
        /// </summary>
        public static event Action<int> OnGoldUpdate;

        /// <summary>
        /// Системное событие на изменение жизни
        /// </summary>
        public static event Action<int> OnLifeUpdate;

        /// <summary>
        /// Золото
        /// </summary>
        [SerializeField] private int m_Gold;

        private void Start()
        {
            OnGoldUpdate(m_Gold);
            OnLifeUpdate(m_NumLives);
        }

        /// <summary>
        /// Изменить количество золота
        /// </summary>
        /// <param name="gold">Золото</param>
        public void ChangeGold(int gold)
        {
            m_Gold += gold;
            OnGoldUpdate(m_Gold);
        }

        /// <summary>
        /// Изменить количество здоровья
        /// </summary>
        /// <param name="gold">Здоровье</param>
        public void ReduceLife(int damage)
        {
            TakeDamage(damage);
            OnLifeUpdate(m_NumLives);
        }
    }
}