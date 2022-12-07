using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ������� ������
    /// </summary>
    public class EnemySpawner : Spawner
    {
        /// <summary>
        /// ������ �����
        /// </summary>
        [SerializeField] private Enemy m_EnemyPrefab;

        /// <summary>
        /// ScriptableObject � ����������� ������
        /// </summary>
        [SerializeField] private EnemyAsset[] m_EnemyAssets;

        /// <summary>
        /// ���� ���������� �����
        /// </summary>
        [SerializeField] private Path m_Path;

        /// <summary>
        /// ���������� ����������� ������
        /// </summary>
        protected override GameObject GenerateSpawnedEntity()
        {
            Enemy enemy = Instantiate(m_EnemyPrefab);

            enemy.GetComponent<TDPatrolController>().SetPath(m_Path);

            enemy.Use(m_EnemyAssets[Random.Range(0, m_EnemyAssets.Length)]);

            return enemy.gameObject;
        }
    }
}