using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Спавнер врагов
    /// </summary>
    public class EnemySpawner : Spawner
    {
        /// <summary>
        /// Префаб врага
        /// </summary>
        [SerializeField] private Enemy m_EnemyPrefab;

        /// <summary>
        /// ScriptableObject с настройками врагов
        /// </summary>
        [SerializeField] private EnemyAsset[] m_EnemyAssets;

        /// <summary>
        /// Путь следования врага
        /// </summary>
        [SerializeField] private Path m_Path;

        /// <summary>
        /// Генерируем спавнящийся префаб
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