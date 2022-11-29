using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ����� �������� ������. �������, ���������
    /// </summary>
    public enum TurretMode
    {
        Primary,
        Secondary
    }

    /// <summary>
    /// ScriptableObject �� ���������� �������
    /// </summary>
    [CreateAssetMenu]
    public sealed class TurretProperties : ScriptableObject
    {
        /// <summary>
        /// ����� �������� ������
        /// </summary>
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        /// <summary>
        /// ������ �������
        /// </summary>
        [SerializeField] private Projectile m_ProjectilePrefab;
        public Projectile ProjectilePrefab => m_ProjectilePrefab;

        /// <summary>
        /// ����������������
        /// </summary>
        [SerializeField] private float m_RateOfFire;
        public float RateOfFire => m_RateOfFire;

        /// <summary>
        /// ������ ������� �� �������
        /// </summary>
        [SerializeField] private int m_EnergyUsage;
        public int EnergyUsage => m_EnergyUsage;

        /// <summary>
        /// ������ �������� �� �������
        /// </summary>
        [SerializeField] private int m_AmmoUsage;
        public int AmmoUsage => m_AmmoUsage;

        /// <summary>
        /// ���� ��������
        /// </summary>
        [SerializeField] private AudioClip m_LaunchSFX;
        public AudioClip LaunchSFX => m_LaunchSFX;
    }
}