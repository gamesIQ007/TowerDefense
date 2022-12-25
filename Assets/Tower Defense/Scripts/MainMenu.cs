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

        private void Start()
        {
            m_ContinueButton.interactable = FileHandler.HasFile(MapCompletion.FILENAME);
        }

        /// <summary>
        /// Начать новую игру
        /// </summary>
        public void NewGame()
        {
            FileHandler.Reset(MapCompletion.FILENAME);
            SceneManager.LoadScene(1);
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
    }
}