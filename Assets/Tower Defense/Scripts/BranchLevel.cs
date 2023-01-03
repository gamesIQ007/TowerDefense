using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    [RequireComponent(typeof(MapLevel))]

    /// <summary>
    /// �������������� �������
    /// </summary>
    public class BranchLevel : MonoBehaviour
    {
        /// <summary>
        /// �������� �������
        /// </summary>
        [SerializeField] private MapLevel m_RootLevel;

        /// <summary>
        /// ���������� ��������
        /// </summary>
        [SerializeField] private int m_NeedPoints = 3;

        /// <summary>
        /// �����, ������������ ���������� ��������
        /// </summary>
        [SerializeField] private Text m_NeedPointsText;

        /// <summary>
        /// ������� ���������
        /// </summary>
        public void TryActivate()
        {
            gameObject.SetActive(m_RootLevel.IsComplete);

            if (m_NeedPoints > MapCompletion.Instance.TotalScore)
            {
                m_NeedPointsText.text = m_NeedPoints.ToString();
            }
            else
            {
                m_NeedPointsText.transform.parent.gameObject.SetActive(false);
                GetComponent<MapLevel>().Initialise();
            }
        }
    }
}