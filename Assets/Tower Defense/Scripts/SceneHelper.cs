using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense
{
    /// <summary>
    /// �������� ������ �� �������
    /// </summary>
    public class SceneHelper : MonoBehaviour
    {
        public void LoadLevel(int buildIndex)
        {
            SceneManager.LoadScene(buildIndex);
        }
    }
}