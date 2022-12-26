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
        /// ������ ����������
        /// </summary>
        [SerializeField] private RectTransform m_ResultPanel;

        /// <summary>
        /// ����������� ���������� ����������� ������
        /// </summary>
        [SerializeField] private Image[] m_ResultImages;

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
            m_ResultPanel.gameObject.SetActive(score > 0);
            for (int i = 0; i < score; i++)
            {
                m_ResultImages[i].color = Color.white;
            }            
        }
    }
}