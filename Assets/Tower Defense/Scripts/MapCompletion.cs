using System;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// ���������� ����������� �������
    /// </summary>
    public class MapCompletion : SingletonBase<MapCompletion>
    {
        /// <summary>
        /// ���������� ����� ������ � ����������� ���������� ��������
        /// </summary>
        [Serializable]
        private class EpisodeScore
        {
            /// <summary>
            /// ������
            /// </summary>
            public Episode episode;
            /// <summary>
            /// ����
            /// </summary>
            public int score;
        }

        /// <summary>
        /// ������ � ����������� ���������� ��������
        /// </summary>
        [SerializeField] private EpisodeScore[] m_CompletionData;
        public bool TryIndex(int id, out Episode episode, out int score)
        {
            if (id >= 0 && id < m_CompletionData.Length)
            {
                episode = m_CompletionData[id].episode;
                score = m_CompletionData[id].score;
                return true;
            }
            else
            {
                episode = null;
                score = 0;
                return false;
            }
        }

        /// <summary>
        /// ���������� ����������� �������
        /// </summary>
        /// <param name="levelScore">����</param>
        public static void SaveEpisodeResult(int levelScore)
        {
            Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
        }

        /// <summary>
        /// ���������� ����������� �������
        /// </summary>
        /// <param name="episode">������</param>
        /// <param name="levelScore">����</param>
        private void SaveResult(Episode currentEpisode, int levelScore)
        {
            foreach (var item in m_CompletionData)
            {
                if (item.episode == currentEpisode)
                {
                    if (levelScore > item.score)
                    {
                        item.score = levelScore;
                    }
                }
            }
        }
    }
}