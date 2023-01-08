using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// Покупка апгрейдов
    /// </summary>
    public class BuyUpgrade : MonoBehaviour
    {
        /// <summary>
        /// Ассет апгрейда
        /// </summary>
        [SerializeField] private UpgradeAsset m_Asset;

        /// <summary>
        /// Изображение апгрейда
        /// </summary>
        [SerializeField] private Image m_UpgradeIcon;

        /// <summary>
        /// Уровень апгрейда
        /// </summary>
        [SerializeField] private Text m_Level;

        /// <summary>
        /// Текст цены апгрейда
        /// </summary>
        [SerializeField] private Text m_CostText;

        /// <summary>
        /// Цена апгрейда
        /// </summary>
        private int m_Cost = 0;

        /// <summary>
        /// Кнопка покупки
        /// </summary>
        [SerializeField] private Button m_Button;

        /// <summary>
        /// Инициализация апгрейда
        /// </summary>
        public void Initialize()
        {
            m_UpgradeIcon.sprite = m_Asset.sprite;
            int savedLevel = Upgrades.GetUpgradeLevel(m_Asset);

            if (savedLevel >= m_Asset.costByLevel.Length)
            {
                m_Level.text = $"Уровень: {savedLevel} (Max)";
                m_Button.interactable = false;
                m_Button.transform.Find("Image").gameObject.SetActive(false);
                m_Button.transform.Find("Cost").gameObject.SetActive(false);
                m_Button.transform.Find("Text").GetComponent<Text>().text = "Улучшено";
                m_Cost = int.MaxValue;
            }
            else
            {
                m_Level.text = $"Уровень: {savedLevel + 1}";
                m_Cost = m_Asset.costByLevel[savedLevel];
                m_CostText.text = m_Cost.ToString();
            }
        }

        /// <summary>
        /// Покупка апгрейда
        /// </summary>
        /// <param name="asset">Ассет</param>
        public void Buy()
        {
            Upgrades.BuyUpgrade(m_Asset);
            Initialize();
        }

        /// <summary>
        /// Проверка, достаточно ли денег для покупки
        /// </summary>
        /// <param name="money">Количество денег</param>
        public void CheckCost(int money)
        {
            m_Button.interactable = money >= m_Cost;
        }
    }
}