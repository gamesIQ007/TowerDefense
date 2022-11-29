using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    /// <summary>
    /// ���������� ������������������ �������
    /// </summary>
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        /// <summary>
        /// ��� ����� � ������� ����
        /// </summary>
        public static string MainMenuSceneNickname = "main_menu";

        /// <summary>
        /// ����� �����������, �� ������� ����� �������� ����
        /// </summary>
        [SerializeField] private int m_MaxTimeForBonus;

        /// <summary>
        /// ��������� �����
        /// </summary>
        [SerializeField] private float m_BonusMultiplier;

        /// <summary>
        /// ������� ������
        /// </summary>
        public Episode CurrentEpisode { get; private set; }

        /// <summary>
        /// ������� �������
        /// </summary>
        public int CurrentLevel { get; private set; }

        /// <summary>
        /// ��������� ���������� ������
        /// </summary>
        public bool LastLevelResult { get; private set; }

        /// <summary>
        /// ����������
        /// </summary>
        public PlayerStatistics LevelStatistics { get; private set; }

        /// <summary>
        /// ����� ����������
        /// </summary>
        public TotalStatistics TotalStatistics { get; private set; }

        /// <summary>
        /// ������� ������
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
        /// ������ �������
        /// </summary>
        /// <param name="episode">������</param>
        public void StartEpisode(Episode episode)
        {
            CurrentEpisode = episode;
            CurrentLevel = 0;

            LevelStatistics = new PlayerStatistics();
            LevelStatistics.Reset();

            SceneManager.LoadScene(episode.Levels[CurrentLevel]);
        }

        /// <summary>
        /// ���������� ������
        /// </summary>
        public void RestartLevel()
        {
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        /// <summary>
        /// ���������� �������� ������
        /// </summary>
        /// <param name="success"></param>
        public void FinishCurrentLevel(bool success)
        {
            LastLevelResult = success;

            CalculateLevelStatistic();

            ResultPanelController.Instance.ShowResult(LevelStatistics, success);
        }

        /// <summary>
        /// ������� � ���������� ������
        /// </summary>
        public void AdvanceLevel()
        {
            LevelStatistics.Reset();

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
        /// ������������ ����������
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