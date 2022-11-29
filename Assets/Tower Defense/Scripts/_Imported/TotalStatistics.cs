using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ����� ���������� ���
    /// </summary>
    public class TotalStatistics
    {
        /// <summary>
        /// ���������� �������
        /// </summary>
        public int totalNumKills;

        /// <summary>
        /// ����
        /// </summary>
        public int totalScore;

        /// <summary>
        /// �����
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