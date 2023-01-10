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

        /// <summary>
        /// Требуемый апгрейд
        /// </summary>
        [SerializeField] private UpgradeAsset requiredUpgrade;

        /// <summary>
        /// Требуемый уровень апгрейда
        /// </summary>
        [SerializeField] private int requiredUpgradeLevel;

        /// <summary>
        /// Доступность башни
        /// </summary>
        /// <returns>Доступна ли башня</returns>
        public bool IsAvailable()
        {
            if (requiredUpgrade)
            {
                return requiredUpgradeLevel <= Upgrades.GetUpgradeLevel(requiredUpgrade);
            }
            else return true;
        }

        /// <summary>
        /// В какие башни можно апгрейдить эту башню
        /// </summary>
        public TowerAsset[] m_UpgradesTo;
    }
}
