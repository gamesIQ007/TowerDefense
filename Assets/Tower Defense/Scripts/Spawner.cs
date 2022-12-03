using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// ����������� �������
    /// </summary>
    public abstract class Spawner : MonoBehaviour
    {
        /// <summary>
        /// ����� ������
        /// </summary>
        public enum SpawnMode
        {
            /// <summary>
            /// ����� ��� ������
            /// </summary>
            Start,

            /// <summary>
            /// ����� � �����
            /// </summary>
            Loop
        }

        /// <summary>
        /// ���������� ����������� ������
        /// </summary>
        protected abstract GameObject GenerateSpawnedEntity();

        /// <summary>
        /// ������� ������
        /// </summary>
        [SerializeField] private CircleArea m_Area;

        /// <summary>
        /// ����� ������
        /// </summary>
        [SerializeField] private SpawnMode m_SpawnMode;

        /// <summary>
        /// ���������� �������
        /// </summary>
        [SerializeField] private int m_NumSpawns;

        /// <summary>
        /// ������������� ������
        /// </summary>
        [SerializeField] private float m_RespawnTime;

        /// <summary>
        /// ������
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
        /// ����� ��������
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