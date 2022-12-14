using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    /// <summary>
    /// ����� ��������� �����
    /// </summary>
    public class BuildSite : MonoBehaviour, IPointerDownHandler
    {
        /// <summary>
        /// ������� �� �����
        /// </summary>
        public static event Action<Transform> OnClickEvent;

        /// <summary>
        /// ��������� ����� �����
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent(transform.root);
        }

        /// <summary>
        /// �������� ������� ����� ��� ���������
        /// </summary>
        public static void HideControls()
        {
            OnClickEvent(null);
        }
    }
}