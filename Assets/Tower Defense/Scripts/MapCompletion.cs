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
        public const string FILENAME = "completion.dat";

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
        /// ���������� ����� �����
        /// </summary>
        private int totalScore;
        public int TotalScore => totalScore;

        /// <summary>
        /// ������ � ����������� ���������� ��������
        /// </summary>
        [SerializeField] private EpisodeScore[] m_CompletionData;

        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore[]>.TryLoad(FILENAME, ref m_CompletionData);

            foreach (var episodeScore in m_CompletionData)
            {
                totalScore += episodeScore.score;
            }
        }

        /// <summary>
        /// �������� ���� �������
        /// </summary>
        /// <param name="episode">������</param>
        /// <returns>����</returns>
        public int GetEpisodeScore(Episode episode)
        {
            foreach (var data in m_CompletionData)
            {
                if (episode == data.episode)
                {
                    return data.score;
                }
            }
            return 0;
        }

        /// <summary>
        /// ���������� ����������� �������
        /// </summary>
        /// <param name="levelScore">����</param>
        public static void SaveEpisodeResult(int levelScore)
        {
            if (Instance)
            {
                Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
            }
            else
            {
                Debug.Log(levelScore);
            }
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
                        Saver<EpisodeScore[]>.Save(FILENAME, m_CompletionData);
                    }
                }
            }
        }
    }
}