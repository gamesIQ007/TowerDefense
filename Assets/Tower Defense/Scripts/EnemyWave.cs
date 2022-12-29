using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Волна врагов
    /// </summary>
    public class EnemyWave : MonoBehaviour
    {
        /// <summary>
        /// Класс сквада
        /// </summary>
        [Serializable]
        private class Squad
        {
            /// <summary>
            /// Ассет
            /// </summary>
            public EnemyAsset asset;
            /// <summary>
            /// Количество
            /// </summary>
            public int count;
        }

        /// <summary>
        /// Класс группы пути
        /// </summary>
        [Serializable]
        private class PathGroup
        {
            /// <summary>
            /// Массив сквадов
            /// </summary>
            public Squad[] squads;
        }

        /// <summary>
        /// Массив групп путей
        /// </summary>
        [SerializeField] private PathGroup[] m_PathGroups;

        /// <summary>
        /// Время подготовки
        /// </summary>
        [SerializeField] private float m_PrepareTime = 10.0f;
        public float GetRemainingTime() { return m_PrepareTime - Time.time; }

        /// <summary>
        /// Следующая волна
        /// </summary>
        [SerializeField] private EnemyWave m_NextWave;

        /// <summary>
        /// Событие о готовности волны
        /// </summary>
        private event Action OnWaveReady;

        /// <summary>
        /// Событие о начале подготовки волны
        /// </summary>
        public static Action<float> OnWavePrepare;

        private void Awake()
        {
            enabled = false;
        }

        private void Update()
        {
            if (Time.time > m_PrepareTime)
            {
                enabled = false;
                OnWaveReady?.Invoke();
            }
        }

        /// <summary>
        /// Подготовка волны
        /// </summary>
        /// <param name="spawnEnemies">Действие</param>
        public void Prepare(Action spawnEnemies)
        {
            OnWavePrepare?.Invoke(m_PrepareTime);
            m_PrepareTime += Time.time;
            enabled = true;
            OnWaveReady += spawnEnemies;
        }

        /// <summary>
        /// Подготовка следующей волны
        /// </summary>
        /// <param name="spawnEnemies">Действие</param>
        /// <returns>Следующая волна</returns>
        public EnemyWave PrepareNext(Action spawnEnemies)
        {
            OnWaveReady -= spawnEnemies;
            if (m_NextWave)
            {
                m_NextWave.Prepare(spawnEnemies);
            }
            return m_NextWave;
        }

        /// <summary>
        /// Нумератор, выдающий сквады врагов
        /// </summary>
        /// <returns>Сквад</returns>
        public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumerateSquads()
        {
            for (int i = 0; i < m_PathGroups.Length; i++)
            {
                foreach (var squad in m_PathGroups[i].squads)
                {
                    // yield return - может сделать несколько ретурнов, работает только в IEnumerable
                    yield return (squad.asset, squad.count, i);
                }
            }
        }
    }
}