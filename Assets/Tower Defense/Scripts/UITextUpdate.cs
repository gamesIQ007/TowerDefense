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
            Life,

            /// <summary>
            /// Мана
            /// </summary>
            Mana
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
        }

        private void Start()
        {
            switch (m_UpdateSource)
            {
                case UpdateSource.Gold:
                    TDPlayer.Instance.GoldUpdateSubscription(UpdateText);
                    break;
                case UpdateSource.Life:
                    TDPlayer.Instance.LifeUpdateSubscription(UpdateText);
                    break;
                case UpdateSource.Mana:
                    TDPlayer.Instance.ManaUpdateSubscription(UpdateText);
                    break;
            }
        }

        private void OnDestroy()
        {
            switch (m_UpdateSource)
            {
                case UpdateSource.Gold:
                    TDPlayer.Instance.GoldUpdateUnsubscribe(UpdateText);
                    break;
                case UpdateSource.Life:
                    TDPlayer.Instance.LifeUpdateUnsubscribe(UpdateText);
                    break;
                case UpdateSource.Mana:
                    TDPlayer.Instance.ManaUpdateUnsubscribe(UpdateText);
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