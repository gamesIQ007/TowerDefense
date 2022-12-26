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
            Life
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
                    TDPlayer.GoldUpdateSubscription(UpdateText);
                    break;
                case UpdateSource.Life:
                    TDPlayer.LifeUpdateSubscription(UpdateText);
                    break;
            }
        }

        private void OnDestroy()
        {
            switch (m_UpdateSource)
            {
                case UpdateSource.Gold:
                    TDPlayer.GoldUpdateUnsubscribe(UpdateText);
                    break;
                case UpdateSource.Life:
                    TDPlayer.LifeUpdateUnsubscribe(UpdateText);
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