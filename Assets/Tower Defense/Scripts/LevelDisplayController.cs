using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ��������� �������
    /// </summary>
    public class LevelDisplayController : MonoBehaviour
    {
        /// <summary>
        /// ������ ������ �� ������� �� �����
        /// </summary>
        [SerializeField] private MapLevel[] m_Levels;

        private void Start()
        {
            int drawLevel = 0;

            int score = 1;
            while (score != 0 && drawLevel < m_Levels.Length && MapCompletion.Instance.TryIndex(drawLevel, out var episode, out score))
            {
                m_Levels[drawLevel].SetLevelData(episode, score);
                drawLevel++;
            }

            for (int i = drawLevel; i < m_Levels.Length; i++)
            {
                m_Levels[i].gameObject.SetActive(false);
            }
        }
    }
}