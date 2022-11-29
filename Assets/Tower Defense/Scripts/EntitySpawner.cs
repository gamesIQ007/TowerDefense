using UnityEngine;

namespace SpaceShooter
{

    /// <summary>
    /// �������
    /// </summary>
    public class EntitySpawner : MonoBehaviour
    {
        /// <summary>
        /// ����� ������
        /// </summary>
        public enum SpawnMode
        {
            Start,
            Loop
        }

        /// <summary>
        /// ����������� �������
        /// </summary>
        [SerializeField] private Entity[] m_EntityPrefabs;

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
        /// ������������� ��������
        /// </summary>
        [SerializeField] private float m_RespawnTime;

        /// <summary>
        /// ������
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
        /// ����� ���������
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