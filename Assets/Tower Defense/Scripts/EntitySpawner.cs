using UnityEngine;

namespace TowerDefense
{

    /// <summary>
    /// Спавнер
    /// </summary>
    public class EntitySpawner : Spawner
    {
        /// <summary>
        /// Список объектов для спавна
        /// </summary>
        [SerializeField] private GameObject[] m_EntityPrefabs;

        /// <summary>
        /// Генерируем спавнящийся префаб
        /// </summary>
        protected override GameObject GenerateSpawnedEntity()
        {
            return Instantiate(m_EntityPrefabs[Random.Range(0, m_EntityPrefabs.Length)]);
        }
    }
}