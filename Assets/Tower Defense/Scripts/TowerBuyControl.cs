using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// Контроллер покупки башни
    /// </summary>
    public class TowerBuyControl : MonoBehaviour
    {
        /// <summary>
        /// Настройки башни
        /// </summary>
        [SerializeField] private TowerAsset m_TowerAsset;

        /// <summary>
        /// Текст
        /// </summary>
        [SerializeField] private Text m_Text;

        /// <summary>
        /// Кнопка
        /// </summary>
        [SerializeField] private Button m_Button;

        /// <summary>
        /// Положение башни
        /// </summary>
        [SerializeField] private Transform m_BuildSite;

        /// <summary>
        /// Задать позицию места постройки
        /// </summary>
        /// <param name="position">Позиция</param>
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
        /// Проверка, хватает ли золота
        /// </summary>
        /// <param name="gold">Золото</param>
        private void GoldStatusCheck(int gold)
        {
            if (gold >= m_TowerAsset.goldCost != m_Button.interactable)
            {
                m_Button.interactable = !m_Button.interactable;
                m_Text.color = m_Button.interactable ? Color.white : Color.red;
            }
        }

        /// <summary>
        /// Покупка башни
        /// </summary>
        public void Buy()
        {
            TDPlayer.Instance.TryBuild(m_TowerAsset, m_BuildSite);
            BuildSite.HideControls();
        }
    }
}