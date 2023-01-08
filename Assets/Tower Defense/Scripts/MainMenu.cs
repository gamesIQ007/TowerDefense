using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// Главное меню
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        /// <summary>
        /// Кнопка "Продолжить"
        /// </summary>
        [SerializeField] private Button m_ContinueButton;

        [SerializeField] private GameObject m_ConfirmationPanel;

        private void Start()
        {
            m_ContinueButton.interactable = FileHandler.HasFile(MapCompletion.FILENAME);
        }

        /// <summary>
        /// Начать новую игру
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
        /// Продолжить игру
        /// </summary>
        public void Continue()
        {
            SceneManager.LoadScene(1);
        }

        /// <summary>
        /// Выход
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }

        /// <summary>
        /// Отобразить окно подтверждения перезаписи файла сохранения
        /// </summary>
        public void ShowConfirmationPanel()
        {
            m_ConfirmationPanel.SetActive(true);
        }

        /// <summary>
        /// Скрыть окно подтверждения перезаписи файла сохранения
        /// </summary>
        public void HideConfirmationPanel()
        {
            m_ConfirmationPanel.SetActive(false);
        }

        /// <summary>
        /// Начать новую игру
        /// </summary>
        public void StartNewGame()
        {
            FileHandler.Reset(MapCompletion.FILENAME);
            FileHandler.Reset(Upgrades.FILENAME);
            SceneManager.LoadScene(1);
        }
    }
}