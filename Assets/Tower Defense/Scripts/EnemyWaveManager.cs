using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// �������� ���� ������
    /// </summary>
    public class EnemyWaveManager : MonoBehaviour
    {
        /// <summary>
        /// ���� ��������
        /// </summary>
        [SerializeField] private Path[] m_Paths;

        /// <summary>
        /// ������ �����
        /// </summary>
        [SerializeField] private Enemy m_EnemyPrefab;

        /// <summary>
        /// ������� ����� ������
        /// </summary>
        [SerializeField] private EnemyWave m_CurrentWave;

        private void Start()
        {
            m_CurrentWave.Prepare(SpawnEnemies);
        }

        /// <summary>
        /// ������� ������
        /// </summary>
        private void SpawnEnemies()
        {
            foreach ((EnemyAsset asset, int count, int pathIndex) in m_CurrentWave.EnumerateSquads())
            {
                if (pathIndex < m_Paths.Length)
                {
                    for (int i = 0; i < count; i++)
                    {
                        Enemy enemy = Instantiate(m_EnemyPrefab, m_Paths[pathIndex].StartArea.GetRandomInsideZone(), Quaternion.identity);
                        enemy.GetComponent<TDPatrolController>().SetPath(m_Paths[pathIndex]);
                        enemy.Use(asset);
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid pathIndex in {name}");
                }
            }

            m_CurrentWave = m_CurrentWave.PrepareNext(SpawnEnemies);
        }
    }
}