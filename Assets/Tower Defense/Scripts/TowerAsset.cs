using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// Класс настроек башни
    /// </summary>
    [CreateAssetMenu]
    public class TowerAsset : ScriptableObject
    {
        /// <summary>
        /// Стоимость
        /// </summary>
        public int goldCost = 100;

        /// <summary>
        /// Спрайт интерфейса
        /// </summary>
        public Sprite GUISprite;

        /// <summary>
        /// Спрайт
        /// </summary>
        public Sprite sprite;

        /// <summary>
        /// Настройки турели
        /// </summary>
        public TurretProperties turret;
    }
}
