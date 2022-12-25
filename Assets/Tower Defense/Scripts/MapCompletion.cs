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
        /// Данные о результатах завершения эпизодов
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

        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore[]>.TryLoad(FILENAME, ref m_CompletionData);
        }

        /// <summary>
        /// Сохранение результатов эпизода
        /// </summary>
        /// <param name="levelScore">Очки</param>
        public static void SaveEpisodeResult(int levelScore)
        {
            Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
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