using UnityEngine;

namespace SpaceShooter
{

    /// <summary>
    /// Спавнер
    /// </summary>
    public class EntitySpawner : MonoBehaviour
    {
        /// <summary>
        /// Режим спавна
        /// </summary>
        public enum SpawnMode
        {
            Start,
            Loop
        }

        /// <summary>
        /// Спавнящиеся префабы
        /// </summary>
        [SerializeField] private Entity[] m_EntityPrefabs;

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
        /// Периодичность респавна
        /// </summary>
        [SerializeField] private float m_RespawnTime;

        /// <summary>
        /// Таймер
        /// </summary>
        private float m_Timer;

        [SerializeField] private AIPointPatrol m_MoveTarget;

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
        /// Спавн сущностей
        /// </summary>
        private void SpawnEntities()
        {
            for (int i = 0; i < m_NumSpawns; i++)
            {
                int index = Random.Range(0, m_EntityPrefabs.Length);

                GameObject e = Instantiate(m_EntityPrefabs[index].gameObject);
                e.transform.position = m_Area.GetRandomInsideZone();

                if (e.TryGetComponent<AIController>(out var ai))
                {
                    ai.SetPatrolBehaviour(m_MoveTarget);
                }
            }
        }
    }
}