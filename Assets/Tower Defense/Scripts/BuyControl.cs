using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ���������� ���������� ��������� �����
    /// </summary>
    public class BuyControl : MonoBehaviour
    {
        /// <summary>
        /// ��������� �����������
        /// </summary>
        private RectTransform m_RectTransform;

        private void Awake()
        {
            m_RectTransform = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += MoveToBuildSite;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            BuildSite.OnClickEvent -= MoveToBuildSite;
        }

        /// <summary>
        /// �������� ���������
        /// </summary>
        /// <param name="buildSite">�������</param>
        private void MoveToBuildSite(Transform buildSite)
        {
            if (buildSite)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.position);
                m_RectTransform.anchoredPosition = position;
                gameObject.SetActive(true);

                foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
                {
                    tbc.SetBuildSite(buildSite);
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}