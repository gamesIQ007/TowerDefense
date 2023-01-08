using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// ������� ���������
    /// </summary>
    public class UpgradeShop : MonoBehaviour
    {
        /// <summary>
        /// ���������� �����
        /// </summary>
        private int m_Money;

        /// <summary>
        /// ����� � ����������� �����
        /// </summary>
        [SerializeField] private Text m_MoneyText;

        /// <summary>
        /// ������ ������ ���������
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
        /// �������� ���������� �����
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