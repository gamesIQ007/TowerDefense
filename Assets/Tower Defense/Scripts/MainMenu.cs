using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// ������� ����
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        /// <summary>
        /// ������ "����������"
        /// </summary>
        [SerializeField] private Button m_ContinueButton;

        [SerializeField] private GameObject m_ConfirmationPanel;

        private void Start()
        {
            m_ContinueButton.interactable = FileHandler.HasFile(MapCompletion.FILENAME);
        }

        /// <summary>
        /// ������ ����� ����
        /// </summary>
        public void NewGame()
        {
            if (FileHandler.HasFile(MapCompletion.FILENAME))
            {
                ShowConfirmationPanel();
            }
            else
            {
                StartNewGame();
            }
        }

        /// <summary>
        /// ���������� ����
        /// </summary>
        public void Continue()
        {
            SceneManager.LoadScene(1);
        }

        /// <summary>
        /// �����
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }

        /// <summary>
        /// ���������� ���� ������������� ���������� ����� ����������
        /// </summary>
        public void ShowConfirmationPanel()
        {
            m_ConfirmationPanel.SetActive(true);
        }

        /// <summary>
        /// ������ ���� ������������� ���������� ����� ����������
        /// </summary>
        public void HideConfirmationPanel()
        {
            m_ConfirmationPanel.SetActive(false);
        }

        /// <summary>
        /// ������ ����� ����
        /// </summary>
        public void StartNewGame()
        {
            FileHandler.Reset(MapCompletion.FILENAME);
            FileHandler.Reset(Upgrades.FILENAME);
            SceneManager.LoadScene(1);
        }
    }
}