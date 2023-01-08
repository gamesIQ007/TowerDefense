using System;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Менеджер волн врагов
    /// </summary>
    public class EnemyWaveManager : MonoBehaviour
    {
        /// <summary>
        /// Пути движения
        /// </summary>
        [SerializeField] private Path[] m_Paths;

        /// <summary>
        /// Префаб врага
        /// </summary>
        [SerializeField] private Enemy m_EnemyPrefab;

        /// <summary>
        /// Текущая волна врагов
        /// </summary>
        [SerializeField] private EnemyWave m_CurrentWave;

        /// <summary>
        /// Количество активных врагов
        /// </summary>
        private int m_ActiveEnemyCount = 0;

        /// <summary>
        /// Действие при окончании всех волн
        /// </summary>
        public event Action OnAllWavesDead;

        private void Start()
        {
            m_CurrentWave.Prepare(SpawnEnemies);
        }

        /// <summary>
        /// Спавним врагов
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
                        enemy.OnEnd += RecordEnemyDead;
                        m_ActiveEnemyCount++;
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid pathIndex in {name}");
                }
            }

            m_CurrentWave = m_CurrentWave.PrepareNext(SpawnEnemies);
        }

        /// <summary>
        /// Вызов следующей волны
        /// </summary>
        public void ForceNextWave()
        {
            if (m_CurrentWave)
            {
                TDPlayer.Instance.ChangeGold((int)m_CurrentWave.GetRemainingTime());
                SpawnEnemies();
            }
            else
            {
                if (m_ActiveEnemyCount == 0)
                {
                    OnAllWavesDead?.Invoke();
                }
            }
        }

        /// <summary>
        /// Считаем умерших врагов
        /// </summary>
        private void RecordEnemyDead()
        {
            if (--m_ActiveEnemyCount == 0)
            {
                ForceNextWave();
            }
        }
    }
}