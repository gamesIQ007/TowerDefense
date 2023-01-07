using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    /// <summary>
    /// ScriptableObject апгрейда
    /// </summary>
    public class UpgradeAsset : ScriptableObject
    {
        /// <summary>
        /// Спрайт
        /// </summary>
        public Sprite sprite;

        /// <summary>
        /// Массив цен
        /// </summary>
        public int[] costByLevel = { 3 };

        /// <summary>
        /// Модификатор, какой бонус за уровень
        /// </summary>
        public float modifier = 5.0f;
    }
}