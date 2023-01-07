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
        [SerializeField] private Episode m_Episode;
        public Episode Episode => m_Episode;

        /// <summary>
        /// ������ ����������
        /// </summary>
        [SerializeField] private RectTransform m_ResultPanel;

        /// <summary>
        /// ����������� ���������� ����������� ������
        /// </summary>
        [SerializeField] private Image[] m_ResultImages;

        /// <summary>
        /// ������� �� �������
        /// </summary>
        public bool IsComplete => gameObject.activeSelf && m_ResultPanel.gameObject.activeSelf;

        /// <summary>
        /// ��������� �������
        /// </summary>
        public void LoadLevel()
        {
            LevelSequenceController.Instance.StartEpisode(m_Episode);
        }

        /// <summary>
        /// ������������� ������
        /// </summary>
        public void Initialise()
        {
            var score = MapCompletion.Instance.GetEpisodeScore(m_Episode);
            m_ResultPanel.gameObject.SetActive(score > 0);
            for (int i = 0; i < score; i++)
            {
                m_ResultImages[i].color = Color.white;
            }
        }
    }
}