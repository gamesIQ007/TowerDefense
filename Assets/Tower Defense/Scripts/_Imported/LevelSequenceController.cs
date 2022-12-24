using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    /// <summary>
    /// Контроллер последовательности уровней. Должен быть в главном меню с пометкой "Не уничтожать при загрузке"
    /// </summary>
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        /// <summary>
        /// Имя сцены с главным меню
        /// </summary>
        public static string MainMenuSceneNickname = "LevelMap";

        /// <summary>
        /// Время прохождения, за которое дадут бонусные очки
        /// </summary>
        [SerializeField] private int m_MaxTimeForBonus;

        /// <summary>
        /// Множитель очков
        /// </summary>
        [SerializeField] private float m_BonusMultiplier;

        /// <summary>
        /// Текущий эпизод
        /// </summary>
        public Episode CurrentEpisode { get; private set; }

        /// <summary>
        /// Текущий уровень
        /// </summary>
        public int CurrentLevel { get; private set; }

        /// <summary>
        /// Результат последнего уровня
        /// </summary>
        public bool LastLevelResult { get; private set; }

        /// <summary>
        /// Статистика
        /// </summary>
        public PlayerStatistics LevelStatistics { get; private set; }

        /// <summary>
        /// Общая статистика
        /// </summary>
        public TotalStatistics TotalStatistics { get; private set; }

        /// <summary>
        /// Корабль игрока
        /// </summary>
        public static SpaceShip PlayerShip { get; set; }

        private void Start()
        {
            if (TotalStatistics == null)
            {
                TotalStatistics = new TotalStatistics();
                TotalStatistics.Reset();
            }
        }

        /// <summary>
        /// Запуск эпизода
        /// </summary>
        /// <param name="episode">Эпизод</param>
        public void StartEpisode(Episode episode)
        {
            CurrentEpisode = episode;
            CurrentLevel = 0;

            LevelStatistics = new PlayerStatistics();
            LevelStatistics.Reset();

            SceneManager.LoadScene(episode.Levels[CurrentLevel]);
        }

        /// <summary>
        /// Перезапуск уровня
        /// </summary>
        public void RestartLevel()
        {
            //SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            SceneManager.LoadScene(0);
        }

        /// <summary>
        /// Завершение текущего уровня
        /// </summary>
        /// <param name="success"></param>
        public void FinishCurrentLevel(bool success)
        {
            LastLevelResult = success;
            
            //CalculateLevelStatistic();

            //ResultPanelController.Instance.ShowResult(LevelStatistics, success);
            LevelResultController.Instance.ShowResult(success);
        }

        /// <summary>
        /// Переход к следующему уровню
        /// </summary>
        public void AdvanceLevel()
        {
            //LevelStatistics.Reset();

            CurrentLevel++;

            if (CurrentEpisode.Levels.Length <= CurrentLevel)
            {
                SceneManager.LoadScene(MainMenuSceneNickname);
            }
            else
            {
                SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            }
        }

        /// <summary>
        /// Подсчитываем статистику
        /// </summary>
        private void CalculateLevelStatistic()
        {
            LevelStatistics.numKills = Player.Instance.NumKills;
            LevelStatistics.score = Player.Instance.Score;
            LevelStatistics.time = (int)LevelController.Instance.LevelTime;
            if (m_MaxTimeForBonus > LevelStatistics.time)
            {
                LevelStatistics.bonus = (int)(LevelStatistics.score * m_BonusMultiplier);
            }

            TotalStatistics.totalNumKills += LevelStatistics.numKills;
            TotalStatistics.totalScore += LevelStatistics.score + LevelStatistics.bonus;
            TotalStatistics.totalTime += LevelStatistics.time;
        }
    }
}