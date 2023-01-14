using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// ���������� ����������
    /// </summary>
    public class UITextUpdate : MonoBehaviour
    {
        /// <summary>
        /// �������� ����������
        /// </summary>
        public enum UpdateSource
        {
            /// <summary>
            /// ������
            /// </summary>
            Gold,

            /// <summary>
            /// �����
            /// </summary>
            Life,

            /// <summary>
            /// ����
            /// </summary>
            Mana
        }

        /// <summary>
        /// �������� ����������
        /// </summary>
        [SerializeField] private UpdateSource m_UpdateSource;

        /// <summary>
        /// UI � �������
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
        /// �������� �����
        /// </summary>
        /// <param name="value">�����</param>
        private void UpdateText(int value)
        {
            m_Text.text = value.ToString();
        }
    }
}