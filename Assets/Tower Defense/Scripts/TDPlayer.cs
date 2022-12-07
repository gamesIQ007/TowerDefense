using System;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// ����� ������ � TowerDefense
    /// </summary>
    public class TDPlayer : Player
    {
        /// <summary>
        /// ������� ��� ��������
        /// </summary>
        public static new TDPlayer Instance
        {
            get { return Player.Instance as TDPlayer; }
        }

        /// <summary>
        /// ��������� ������� �� ��������� ������
        /// </summary>
        public static event Action<int> OnGoldUpdate;

        /// <summary>
        /// ��������� ������� �� ��������� �����
        /// </summary>
        public static event Action<int> OnLifeUpdate;

        /// <summary>
        /// ������
        /// </summary>
        [SerializeField] private int m_Gold;

        private void Start()
        {
            OnGoldUpdate(m_Gold);
            OnLifeUpdate(m_NumLives);
        }

        /// <summary>
        /// �������� ���������� ������
        /// </summary>
        /// <param name="gold">������</param>
        public void ChangeGold(int gold)
        {
            m_Gold += gold;
            OnGoldUpdate(m_Gold);
        }

        /// <summary>
        /// �������� ���������� ��������
        /// </summary>
        /// <param name="gold">��������</param>
        public void ReduceLife(int damage)
        {
            TakeDamage(damage);
            OnLifeUpdate(m_NumLives);
        }
    }
}