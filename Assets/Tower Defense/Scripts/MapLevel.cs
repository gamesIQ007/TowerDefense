using UnityEngine;
using UnityEngine.UI;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// ”ровень на карте
    /// </summary>
    public class MapLevel : MonoBehaviour
    {
        /// <summary>
        /// Ёпизод
        /// </summary>
        private Episode m_Episode;

        /// <summary>
        /// ѕанель результата
        /// </summary>
        [SerializeField] private RectTransform m_ResultPanel;

        /// <summary>
        /// »зображени€ результата прохождени€ уровн€
        /// </summary>
        [SerializeField] private Image[] m_ResultImages;

        /// <summary>
        /// «агрузить уровень
        /// </summary>
        public void LoadLevel()
        {
            LevelSequenceController.Instance.StartEpisode(m_Episode);
        }

        /// <summary>
        /// ”становить данные эпизода
        /// </summary>
        /// <param name="episode">Ёпизод</param>
        /// <param name="score">ќчки</param>
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