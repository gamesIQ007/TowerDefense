using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    /// <summary>
    /// Место постройки башни
    /// </summary>
    public class BuildSite : MonoBehaviour, IPointerDownHandler
    {
        /// <summary>
        /// Событие по клику
        /// </summary>
        public static event Action<Transform> OnClickEvent;

        /// <summary>
        /// Обработка клика мышки
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent(transform.root);
        }

        /// <summary>
        /// Передача события клика без координат
        /// </summary>
        public static void HideControls()
        {
            OnClickEvent(null);
        }
    }
}