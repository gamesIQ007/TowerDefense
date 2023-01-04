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
        private int money;

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
            money = MapCompletion.Instance.TotalScore;
            money -= Upgrades.GetTotalCost();
            m_MoneyText.text = money.ToString();

            foreach (var slot in m_Upgrades)
            {
                slot.CheckCost(money);
            }
        }
    }
}