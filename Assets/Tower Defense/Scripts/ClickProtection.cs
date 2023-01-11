using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// Предотвращение кликов по экрану
    /// </summary>
    public class ClickProtection : SingletonBase<ClickProtection>, IPointerClickHandler
    {
        /// <summary>
        /// Изображение-блокировщик
        /// </summary>
        private Image m_Blocker;

        /// <summary>
        /// Действие при клике
        /// </summary>
        private Action<Vector2> m_OnClickAction;

        private void Start()
        {
            m_Blocker = GetComponent<Image>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            m_Blocker.enabled = false;
            m_OnClickAction(eventData.pressPosition);
            m_OnClickAction = null;
        }

        /// <summary>
        /// Активация
        /// </summary>
        /// <param name="mouseAction">Положение курсора</param>
        public void Activate(Action<Vector2> mouseAction)
        {
            m_Blocker.enabled = true;
            m_OnClickAction = mouseAction;
        }
    }
}