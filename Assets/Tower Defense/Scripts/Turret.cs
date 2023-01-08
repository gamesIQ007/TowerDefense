using UnityEngine;
using TowerDefense;

namespace SpaceShooter
{

    /// <summary>
    /// Скрипт турели
    /// </summary>
    public class Turret : MonoBehaviour
    {
        /// <summary>
        /// Режим стрельбы турели
        /// </summary>
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        /// <summary>
        /// Свойства турели. ScriptableObject
        /// </summary>
        [SerializeField] private TurretProperties m_TurretProperties;

        /// <summary>
        /// Таймер повторного выстрела
        /// </summary>
        private float m_RefireTimer;

        /// <summary>
        /// Возможность стрельбы
        /// </summary>
        public bool CanFire => m_RefireTimer <= 0;

        /// <summary>
        /// Ссылка на корабль
        /// </summary>
        private SpaceShip m_Ship;

        #region Unity Events

        private void Start()
        {
            m_Ship = transform.root.GetComponent<SpaceShip>();
        }

        private void Update()
        {
            if (m_RefireTimer > 0)
            {
                m_RefireTimer -= Time.deltaTime;
            }
            else
            {
                if (m_Mode == TurretMode.Auto)
                {
                    Fire();
                }
            }
        }

        #endregion

        #region Public API

        /// <summary>
        /// Стрельба
        /// </summary>
        public void Fire()
        {
            if (m_TurretProperties == null) return;
            if (CanFire == false) return;

            if (m_Ship != null)
            {
                if (m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false) return;
                if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false) return;
            }

            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab);
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;
            projectile.SetParentShooter(m_Ship);

            m_RefireTimer = m_TurretProperties.RateOfFire;

            //m_Ship.audio.clip = m_TurretProperties.LaunchSFX;
            //m_Ship.audio.Play();
        }

        /// <summary>
        /// Замена режима стрельбы
        /// </summary>
        /// <param name="props"></param>
        public void AssignLoadout(TurretProperties props)
        {
            //if (m_Mode != props.Mode) return;

            m_RefireTimer = 0;

            m_TurretProperties = props;
        }

        #endregion
    }
}