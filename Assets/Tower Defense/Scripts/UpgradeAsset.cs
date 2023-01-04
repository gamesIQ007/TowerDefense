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
    }
}