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
        private int totalScore;
        public int TotalScore => totalScore;

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
                totalScore += episodeScore.score;
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
                Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
            }
            else
            {
                Debug.Log(levelScore);
            }
        }

        /// <summary>
        /// Сохранение результатов эпизода
        /// </summary>
        /// <param name="episode">Эпизод</param>
        /// <param name="levelScore">Очки</param>
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