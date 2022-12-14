using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// ���������� ������� �����
    /// </summary>
    public class TowerBuyControl : MonoBehaviour
    {
        /// <summary>
        /// ��������� �����
        /// </summary>
        [SerializeField] private TowerAsset m_TowerAsset;

        /// <summary>
        /// �����
        /// </summary>
        [SerializeField] private Text m_Text;

        /// <summary>
        /// ������
        /// </summary>
        [SerializeField] private Button m_Button;

        /// <summary>
        /// ��������� �����
        /// </summary>
        [SerializeField] private Transform m_BuildSite;

        /// <summary>
        /// ������ ������� ����� ���������
        /// </summary>
        /// <param name="position">�������</param>
        public void SetBuildSite(Transform position)
        {
            m_BuildSite = position;
        }

        private void Start()
        {
            TDPlayer.GoldUpdateSubscription(GoldStatusCheck);
            m_Text.text = m_TowerAsset.goldCost.ToString();
            m_Button.GetComponent<Image>().sprite = m_TowerAsset.GUISprite;
        }

        /// <summary>
        /// ��������, ������� �� ������
        /// </summary>
        /// <param name="gold">������</param>
        private void GoldStatusCheck(int gold)
        {
            if (gold >= m_TowerAsset.goldCost != m_Button.interactable)
            {
                m_Button.interactable = !m_Button.interactable;
                m_Text.color = m_Button.interactable ? Color.white : Color.red;
            }
        }

        /// <summary>
        /// ������� �����
        /// </summary>
        public void Buy()
        {
            TDPlayer.Instance.TryBuild(m_TowerAsset, m_BuildSite);
            BuildSite.HideControls();
        }
    }
}