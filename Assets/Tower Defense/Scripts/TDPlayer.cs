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
        /// ������
        /// </summary>
        [SerializeField] private int m_Gold;

        /// <summary>
        /// �������� ���������� ������
        /// </summary>
        /// <param name="gold">������</param>
        public void ChangeGold(int gold)
        {
            m_Gold += gold;
        }
    }
}