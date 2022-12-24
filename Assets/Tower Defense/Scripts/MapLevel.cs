using UnityEngine;
using UnityEngine.UI;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// ������� �� �����
    /// </summary>
    public class MapLevel : MonoBehaviour
    {
        /// <summary>
        /// ������
        /// </summary>
        private Episode m_Episode;

        /// <summary>
        /// ������������ �����
        /// </summary>
        [SerializeField] private Text text;

        /// <summary>
        /// ��������� �������
        /// </summary>
        public void LoadLevel()
        {
            LevelSequenceController.Instance.StartEpisode(m_Episode);
        }

        /// <summary>
        /// ���������� ������ �������
        /// </summary>
        /// <param name="episode">������</param>
        /// <param name="score">����</param>
        public void SetLevelData(Episode episode, int score)
        {
            m_Episode = episode;
            text.text = $"{score}/3";
        }
    }
}