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

        /// <summary>
        /// ������ ������ �� �������������� ������� �� �����
        /// </summary>
        [SerializeField] private BranchLevel[] m_BranchLevels;

        private void Start()
        {
            int drawLevel = 0;

            int score = 1;
            while (score != 0 && drawLevel < m_Levels.Length)
            {
                m_Levels[drawLevel].Initialise();
                score = MapCompletion.Instance.GetEpisodeScore(m_Levels[drawLevel].Episode);
                drawLevel++;
            }

            for (int i = drawLevel; i < m_Levels.Length; i++)
            {
                m_Levels[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < m_BranchLevels.Length; i++)
            {
                m_BranchLevels[i].TryActivate();
            }
        }
    }
}