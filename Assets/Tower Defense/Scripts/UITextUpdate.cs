using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// Обновление интерфейса
    /// </summary>
    public class UITextUpdate : MonoBehaviour
    {
        /// <summary>
        /// Источник обновления
        /// </summary>
        public enum UpdateSource
        {
            /// <summary>
            /// Золото
            /// </summary>
            Gold,

            /// <summary>
            /// Жизнь
            /// </summary>
            Life
        }
        /// <summary>
        /// Источник обновления
        /// </summary>
        [SerializeField] private UpdateSource m_UpdateSource;

        /// <summary>
        /// UI с текстом
        /// </summary>
        private Text m_Text;

        private void Awake()
        {
            m_Text = GetComponent<Text>();

            switch (m_UpdateSource)
            {
                case UpdateSource.Gold:
                    TDPlayer.OnGoldUpdate += UpdateText;
                    break;
                case UpdateSource.Life:
                    TDPlayer.OnLifeUpdate += UpdateText;
                    break;
            }
            
        }

        /// <summary>
        /// Обновить текст
        /// </summary>
        /// <param name="value">Текст</param>
        private void UpdateText(int value)
        {
            m_Text.text = value.ToString();
        }
    }
}