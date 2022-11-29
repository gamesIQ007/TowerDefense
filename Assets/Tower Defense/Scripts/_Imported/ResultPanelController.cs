using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    /// <summary>
    /// ���������� ���� �����������
    /// </summary>
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        /// <summary>
        /// ���������� �������
        /// </summary>
        [SerializeField] private Text m_NumKills;

        /// <summary>
        /// ����
        /// </summary>
        [SerializeField] private Text m_Score;

        /// <summary>
        /// �����
        /// </summary>
        [SerializeField] private Text m_Time;

        /// <summary>
        /// �������� ����
        /// </summary>
        [SerializeField] private Text m_Bonus;

        /// <summary>
        /// ����� ����������
        /// </summary>
        [SerializeField] private Text m_Result;

        /// <summary>
        /// ����� ������ "�����"
        /// </summary>
        [SerializeField] private Text m_ButtonNextText;

        /// <summary>
        /// ������� �� �������
        /// </summary>
        private bool m_Success;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// ���������� ����������
        /// </summary>
        /// <param name="levelResult">���������� ������</param>
        /// <param name="success">����� �����������</param>
        public void ShowResult(PlayerStatistics levelResult, bool success)
        {
            gameObject.SetActive(true);

            m_NumKills.text = "Kills: " + levelResult.numKills.ToString();
            m_Score.text = "Score: " + levelResult.score.ToString();
            m_Time.text = "Time: " + levelResult.time.ToString();
            m_Bonus.text = "Bonus: " + levelResult.bonus.ToString();

            m_Success = success;

            m_Result.text = success ? "Win" : "Lose";

            m_ButtonNextText.text = success ? "Next" : "Restart";

            Time.timeScale = 0;
        }

        /// <summary>
        /// �������� ��� ������� �� ������ "�����"
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