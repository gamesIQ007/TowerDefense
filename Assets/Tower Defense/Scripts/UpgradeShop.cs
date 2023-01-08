using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// Магазин апгрейдов
    /// </summary>
    public class UpgradeShop : MonoBehaviour
    {
        /// <summary>
        /// Количество денег
        /// </summary>
        private int m_Money;

        /// <summary>
        /// Текст с количеством денег
        /// </summary>
        [SerializeField] private Text m_MoneyText;

        /// <summary>
        /// Массив слотов апгрейдов
        /// </summary>
        [SerializeField] private BuyUpgrade[] m_Upgrades;

        private void Start()
        {
            foreach (var slot in m_Upgrades)
            {
                slot.Initialize();
                slot.transform.Find("Button").GetComponent<Button>().onClick.AddListener(UpdateMoney);
            }

            UpdateMoney();
        }

        /// <summary>
        /// Обновить показатель денег
        /// </summary>
        public void UpdateMoney()
        {
            m_Money = MapCompletion.Instance.TotalScore;
            m_Money -= Upgrades.GetTotalCost();
            m_MoneyText.text = m_Money.ToString();

            foreach (var slot in m_Upgrades)
            {
                slot.CheckCost(m_Money);
            }
        }
    }
}