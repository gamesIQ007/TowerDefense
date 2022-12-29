using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ����� ������
    /// </summary>
    public class EnemyWave : MonoBehaviour
    {
        /// <summary>
        /// ����� ������
        /// </summary>
        [Serializable]
        private class Squad
        {
            /// <summary>
            /// �����
            /// </summary>
            public EnemyAsset asset;
            /// <summary>
            /// ����������
            /// </summary>
            public int count;
        }

        /// <summary>
        /// ����� ������ ����
        /// </summary>
        [Serializable]
        private class PathGroup
        {
            /// <summary>
            /// ������ �������
            /// </summary>
            public Squad[] squads;
        }

        /// <summary>
        /// ������ ����� �����
        /// </summary>
        [SerializeField] private PathGroup[] m_PathGroups;

        /// <summary>
        /// ����� ����������
        /// </summary>
        [SerializeField] private float m_PrepareTime = 10.0f;
        public float GetRemainingTime() { return m_PrepareTime - Time.time; }

        /// <summary>
        /// ��������� �����
        /// </summary>
        [SerializeField] private EnemyWave m_NextWave;

        /// <summary>
        /// ������� � ���������� �����
        /// </summary>
        private event Action OnWaveReady;

        /// <summary>
        /// ������� � ������ ���������� �����
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
        /// ���������� �����
        /// </summary>
        /// <param name="spawnEnemies">��������</param>
        public void Prepare(Action spawnEnemies)
        {
            OnWavePrepare?.Invoke(m_PrepareTime);
            m_PrepareTime += Time.time;
            enabled = true;
            OnWaveReady += spawnEnemies;
        }

        /// <summary>
        /// ���������� ��������� �����
        /// </summary>
        /// <param name="spawnEnemies">��������</param>
        /// <returns>��������� �����</returns>
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
        /// ���������, �������� ������ ������
        /// </summary>
        /// <returns>�����</returns>
        public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumerateSquads()
        {
            for (int i = 0; i < m_PathGroups.Length; i++)
            {
                foreach (var squad in m_PathGroups[i].squads)
                {
                    // yield return - ����� ������� ��������� ��������, �������� ������ � IEnumerable
                    yield return (squad.asset, squad.count, i);
                }
            }
        }
    }
}