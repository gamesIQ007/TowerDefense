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
        /// ���� ����������
        /// </summary>
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
        private int m_TotalScore;
        public int TotalScore => m_TotalScore;

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
                m_TotalScore += episodeScore.score;
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
                foreach (var item in Instance.m_CompletionData)
                {
                    if (item.episode == LevelSequenceController.Instance.CurrentEpisode)
                    {
                        if (levelScore > item.score)
                        {
                            Instance.m_TotalScore += levelScore - item.score;
                            item.score = levelScore;
                            Saver<EpisodeScore[]>.Save(FILENAME, Instance.m_CompletionData);
                        }
                    }
                }
            }
            else
            {
                Debug.Log(levelScore);
            }
        }
    }
}