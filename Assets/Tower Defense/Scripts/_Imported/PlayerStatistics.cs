using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ���������� ������
    /// </summary>
    public class PlayerStatistics
    {
        /// <summary>
        /// ���������� �������
        /// </summary>
        public int numKills;

        /// <summary>
        /// ����
        /// </summary>
        public int score;

        /// <summary>
        /// �����
        /// </summary>
        public int time;

        /// <summary>
        /// �����
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