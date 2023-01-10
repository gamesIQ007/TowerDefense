using System.Collections.Generic;
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

        /// <summary>
        /// ������ �������� ������� �����
        /// </summary>
        [SerializeField] private TowerBuyControl m_TowerBuyPrefab;

        /// <summary>
        /// ������ �������� ��������� ������� �����
        /// </summary>
        private List<TowerBuyControl> m_ActiveControls;

        /// <summary>
        /// �������� ��� ������ ������� ������������ ������
        /// </summary>
        [SerializeField] private int m_OffsetCenter;

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
        private void MoveToBuildSite(BuildSite buildSite)
        {
            if (buildSite)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.transform.root.position);
                m_RectTransform.anchoredPosition = position;

                m_ActiveControls = new List<TowerBuyControl>();

                foreach (var asset in buildSite.buildableTowers)
                {
                    if (asset.IsAvailable())
                    {
                        var newControl = Instantiate(m_TowerBuyPrefab, transform);
                        m_ActiveControls.Add(newControl);
                        newControl.SetTowerAsset(asset);
                    }
                }
                if (m_ActiveControls.Count > 0)
                {
                    gameObject.SetActive(true);

                    var angle = 360 / m_ActiveControls.Count;
                    for (int i = 0; i < m_ActiveControls.Count; i++)
                    {
                        var offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * (Vector3.left * m_OffsetCenter);
                        m_ActiveControls[i].transform.position += offset;
                    }

                    foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
                    {
                        tbc.SetBuildSite(buildSite.transform.root);
                    }
                }
            }
            else
            {
                foreach (var control in m_ActiveControls)
                {
                    Destroy(control.gameObject);
                }
                m_ActiveControls.Clear();
                gameObject.SetActive(false);
            }
        }
    }
}