using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// ������� ���������
    /// </summary>
    public class BuyUpgrade : MonoBehaviour
    {
        /// <summary>
        /// ����� ��������
        /// </summary>
        [SerializeField] private UpgradeAsset m_Asset;

        /// <summary>
        /// ����������� ��������
        /// </summary>
        [SerializeField] private Image m_UpgradeIcon;

        /// <summary>
        /// ������� ��������
        /// </summary>
        [SerializeField] private Text m_Level;

        /// <summary>
        /// ����� ���� ��������
        /// </summary>
        [SerializeField] private Text m_CostText;

        /// <summary>
        /// ���� ��������
        /// </summary>
        private int m_Cost = 0;

        /// <summary>
        /// ������ �������
        /// </summary>
        [SerializeField] private Button m_Button;

        /// <summary>
        /// ������������� ��������
        /// </summary>
        public void Initialize()
        {
            m_UpgradeIcon.sprite = m_Asset.sprite;
            int savedLevel = Upgrades.GetUpgradeLevel(m_Asset);

            if (savedLevel >= m_Asset.costByLevel.Length)
            {
                m_Level.text = $"�������: {savedLevel} (Max)";
                m_Button.interactable = false;
                m_Button.transform.Find("Image").gameObject.SetActive(false);
                m_Button.transform.Find("Cost").gameObject.SetActive(false);
                m_Button.transform.Find("Text").GetComponent<Text>().text = "��������";
                m_Cost = int.MaxValue;
            }
            else
            {
                m_Level.text = $"�������: {savedLevel + 1}";
                m_Cost = m_Asset.costByLevel[savedLevel];
                m_CostText.text = m_Cost.ToString();
            }
        }

        /// <summary>
        /// ������� ��������
        /// </summary>
        /// <param name="asset">�����</param>
        public void Buy()
        {
            Upgrades.BuyUpgrade(m_Asset);
            Initialize();
        }

        /// <summary>
        /// ��������, ���������� �� ����� ��� �������
        /// </summary>
        /// <param name="money">���������� �����</param>
        public void CheckCost(int money)
        {
            m_Button.interactable = money >= m_Cost;
        }
    }
}