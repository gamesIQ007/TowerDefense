using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    /// <summary>
    /// Контроллер окна результатов
    /// </summary>
    public class LevelResultController : SingletonBase<LevelResultController>
    {
        /// <summary>
        /// Количество убийств
        /// </summary>
        [SerializeField] private Text m_NumKills;

        /// <summary>
        /// Очки
        /// </summary>
        [SerializeField] private Text m_Score;

        /// <summary>
        /// Время
        /// </summary>
        [SerializeField] private Text m_Time;

        /// <summary>
        /// Бонусные очки
        /// </summary>
        [SerializeField] private Text m_Bonus;

        /// <summary>
        /// Текст результата
        /// </summary>
        [SerializeField] private Text m_Result;

        /// <summary>
        /// Текст кнопки "Далее"
        /// </summary>
        [SerializeField] private Text m_ButtonNextText;

        /// <summary>
        /// Пройден ли уровень
        /// </summary>
        private bool m_Success;

        /// <summary>
        /// Панель успешного завершения уровня
        /// </summary>
        [SerializeField] private GameObject m_PanelSuccess;

        /// <summary>
        /// Панель провала уровня
        /// </summary>
        [SerializeField] private GameObject m_PanelFailure;

        private void Start()
        {
            //gameObject.SetActive(false);
        }

        /// <summary>
        /// Отобразить статистику
        /// </summary>
        /// <param name="levelResult">Статистика уровня</param>
        /// <param name="success">Успех прохождения</param>
        //public void ShowResult(PlayerStatistics levelResult, bool success)
        public void ShowResult(bool success)
        {
            /*gameObject.SetActive(true);

            m_NumKills.text = "Kills: " + levelResult.numKills.ToString();
            m_Score.text = "Score: " + levelResult.score.ToString();
            m_Time.text = "Time: " + levelResult.time.ToString();
            m_Bonus.text = "Bonus: " + levelResult.bonus.ToString();*/

            m_Success = success;

            /*m_Result.text = success ? "Win" : "Lose";

            m_ButtonNextText.text = success ? "Next" : "Restart";

            Time.timeScale = 0;*/
            
            m_PanelSuccess.SetActive(success);
            m_PanelFailure.SetActive(!success);
        }

        /// <summary>
        /// действие при нажатии на кнопку "Далее"
        /// </summary>
        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1;

            if (m_Success)
            {
                LevelSequenceController.Instance.AdvanceLevel();
            }
            else
            {
                LevelSequenceController.Instance.RestartLevel();
            }
        }
    }
}