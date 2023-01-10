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
        /// Массив доступных для постройки башен
        /// </summary>
        public TowerAsset[] buildableTowers;
        public void SetBuildableTowers(TowerAsset[] towers) 
        { 
            if (towers == null || towers.Length == 0)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                buildableTowers = towers;
            }
        }

        /// <summary>
        /// Событие по клику
        /// </summary>
        public static event Action<BuildSite> OnClickEvent;

        /// <summary>
        /// Обработка клика мышки
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent(this);
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