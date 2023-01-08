using UnityEngine;
using TowerDefense;

namespace SpaceShooter
{
    /// <summary>
    /// Режим стрельбы турели. Главная, вторичная
    /// </summary>
    public enum TurretMode
    {
        Primary,
        Secondary,
        Auto
    }

    /// <summary>
    /// ScriptableObject со свойствами турелей
    /// </summary>
    [CreateAssetMenu]
    public sealed class TurretProperties : ScriptableObject
    {
        /// <summary>
        /// Режим стрельбы турели
        /// </summary>
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        /// <summary>
        /// Префаб снаряда
        /// </summary>
        [SerializeField] private Projectile m_ProjectilePrefab;
        public Projectile ProjectilePrefab => m_ProjectilePrefab;

        /// <summary>
        /// Скорострельность
        /// </summary>
        [SerializeField] private float m_RateOfFire;
        public float RateOfFire => m_RateOfFire;

        /// <summary>
        /// Расход энергии на выстрел
        /// </summary>
        [SerializeField] private int m_EnergyUsage;
        public int EnergyUsage => m_EnergyUsage;

        /// <summary>
        /// Расход патронов на выстрел
        /// </summary>
        [SerializeField] private int m_AmmoUsage;
        public int AmmoUsage => m_AmmoUsage;

        /// <summary>
        /// Звук выстрела
        /// </summary>
        [SerializeField] private AudioClip m_LaunchSFX;
        public AudioClip LaunchSFX => m_LaunchSFX;
    }
}