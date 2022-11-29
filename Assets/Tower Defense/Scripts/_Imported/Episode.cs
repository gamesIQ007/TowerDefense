using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Эпизод игры
    /// </summary>
    [CreateAssetMenu]
    public class Episode : ScriptableObject
    {
        /// <summary>
        /// Имя эпизода
        /// </summary>
        [SerializeField] private string m_EpisodeName;
        public string EpisodeName => m_EpisodeName;

        /// <summary>
        /// Массив имён уровней
        /// </summary>
        [SerializeField] private string[] m_Levels;
        public string[] Levels => m_Levels;

        /// <summary>
        /// Изображение-превью эпизода
        /// </summary>
        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;
    }
}