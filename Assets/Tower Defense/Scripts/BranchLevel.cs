using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    [RequireComponent(typeof(MapLevel))]

    /// <summary>
    /// Дополнительный уровень
    /// </summary>
    public class BranchLevel : MonoBehaviour
    {
        /// <summary>
        /// Основной уровень
        /// </summary>
        [SerializeField] private MapLevel m_RootLevel;

        /// <summary>
        /// Требования открытия
        /// </summary>
        [SerializeField] private int m_NeedPoints = 3;

        /// <summary>
        /// Текст, отображающий требования открытия
        /// </summary>
        [SerializeField] private Text m_NeedPointsText;

        /// <summary>
        /// Попытка активации
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