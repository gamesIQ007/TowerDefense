using System;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// Результаты прохождения уровней
    /// </summary>
    public class MapCompletion : SingletonBase<MapCompletion>
    {
        /// <summary>
        /// Файл сохранения
        /// </summary>
        public const string FILENAME = "completion.dat";

        /// <summary>
        /// Внутренний класс данных о результатах завершения эпизодов
        /// </summary>
        [Serializable]
        private class EpisodeScore
        {
            /// <summary>
            /// Эпизод
            /// </summary>
            public Episode episode;
            /// <summary>
            /// Очки
            /// </summary>
            public int score;
        }

        /// <summary>
        /// Количество очков всего
        /// </summary>
        private int m_TotalScore;
        public int TotalScore => m_TotalScore;

        /// <summary>
        /// Данные о результатах завершения эпизодов
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
        /// Получить очки эпизода
        /// </summary>
        /// <param name="episode">Эпизод</param>
        /// <returns>Очки</returns>
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
        /// Сохранение результатов эпизода
        /// </summary>
        /// <param name="levelScore">Очки</param>
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