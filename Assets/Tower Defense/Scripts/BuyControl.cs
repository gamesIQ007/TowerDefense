using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Контроллер интерфейса постройки башни
    /// </summary>
    public class BuyControl : MonoBehaviour
    {
        /// <summary>
        /// Трансформ контроллера
        /// </summary>
        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += MoveToBuildSite;
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Изменить положение
        /// </summary>
        /// <param name="buildSite">Позиция</param>
        private void MoveToBuildSite(Transform buildSite)
        {
            if (buildSite)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.position);
                rectTransform.anchoredPosition = position;
                gameObject.SetActive(true);

                foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
                {
                    tbc.SetBuildSite(buildSite);
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}