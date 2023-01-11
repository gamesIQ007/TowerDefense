using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// �������������� ������ �� ������
    /// </summary>
    public class ClickProtection : SingletonBase<ClickProtection>, IPointerClickHandler
    {
        /// <summary>
        /// �����������-�����������
        /// </summary>
        private Image m_Blocker;

        /// <summary>
        /// �������� ��� �����
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
        /// ���������
        /// </summary>
        /// <param name="mouseAction">��������� �������</param>
        public void Activate(Action<Vector2> mouseAction)
        {
            m_Blocker.enabled = true;
            m_OnClickAction = mouseAction;
        }
    }
}