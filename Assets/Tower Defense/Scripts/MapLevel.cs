using UnityEngine;
using UnityEngine.UI;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// Уровень на карте
    /// </summary>
    public class MapLevel : MonoBehaviour
    {
        /// <summary>
        /// Эпизод
        /// </summary>
        [SerializeField] private Episode m_Episode;
        public Episode Episode => m_Episode;

        /// <summary>
        /// Панель результата
        /// </summary>
        [SerializeField] private RectTransform m_ResultPanel;

        /// <summary>
        /// Изображения результата прохождения уровня
        /// </summary>
        [SerializeField] private Image[] m_ResultImages;

        /// <summary>
        /// Пройден ли уровень
        /// </summary>
        public bool IsComplete => gameObject.activeSelf && m_ResultPanel.gameObject.activeSelf;

        /// <summary>
        /// Загрузить уровень
        /// </summary>
        public void LoadLevel()
        {
            LevelSequenceController.Instance.StartEpisode(m_Episode);
        }

        /// <summary>
        /// Инициализация уровня
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