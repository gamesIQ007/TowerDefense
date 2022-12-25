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

        private void Start()
        {
            m_ContinueButton.interactable = FileHandler.HasFile(MapCompletion.FILENAME);
        }

        /// <summary>
        /// ������ ����� ����
        /// </summary>
        public void NewGame()
        {
            FileHandler.Reset(MapCompletion.FILENAME);
            SceneManager.LoadScene(1);
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
    }
}