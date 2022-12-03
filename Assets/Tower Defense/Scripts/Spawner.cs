using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// Абстрактный спавнер
    /// </summary>
    public abstract class Spawner : MonoBehaviour
    {
        /// <summary>
        /// Режим спавна
        /// </summary>
        public enum SpawnMode
        {
            /// <summary>
            /// Спавн при старте
            /// </summary>
            Start,

            /// <summary>
            /// Спавн в цикле
            /// </summary>
            Loop
        }

        /// <summary>
        /// Генерируем спавнящийся префаб
        /// </summary>
        protected abstract GameObject GenerateSpawnedEntity();

        /// <summary>
        /// Область спавна
        /// </summary>
        [SerializeField] private CircleArea m_Area;

        /// <summary>
        /// Режим спавна
        /// </summary>
        [SerializeField] private SpawnMode m_SpawnMode;

        /// <summary>
        /// Количество спавнов
        /// </summary>
        [SerializeField] private int m_NumSpawns;

        /// <summary>
        /// Периодичность спавна
        /// </summary>
        [SerializeField] private float m_RespawnTime;

        /// <summary>
        /// Таймер
        /// </summary>
        private float m_Timer;

        #region Unity Events

        private void Start()
        {
            if (m_SpawnMode == SpawnMode.Start)
            {
                SpawnEntities();
            }

            m_Timer = m_RespawnTime;
        }

        private void Update()
        {
            if (m_Timer > 0)
            {
                m_Timer -= Time.deltaTime;
            }

            if (m_SpawnMode == SpawnMode.Loop && m_Timer <= 0)
            {
                SpawnEntities();

                m_Timer = m_RespawnTime;
            }
        }

        #endregion

        /// <summary>
        /// Спавн сущности
        /// </summary>
        private void SpawnEntities()
        {
            for (int i = 0; i < m_NumSpawns; i++)
            {
                GameObject e = GenerateSpawnedEntity();
                e.transform.position = m_Area.GetRandomInsideZone();
            }
        }
    }
}