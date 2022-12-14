using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    /// <summary>
    /// ƒочерний класс от места строительства без передачи координат
    /// </summary>
    public class NullBuildSite : BuildSite
    {
        /// <summary>
        /// ќбработка клика мышки
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnPointerDown(PointerEventData eventData)
        {
            HideControls();
        }
    }
}