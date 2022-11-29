using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ������ ����
    /// </summary>
    [CreateAssetMenu]
    public class Episode : ScriptableObject
    {
        /// <summary>
        /// ��� �������
        /// </summary>
        [SerializeField] private string m_EpisodeName;
        public string EpisodeName => m_EpisodeName;

        /// <summary>
        /// ������ ��� �������
        /// </summary>
        [SerializeField] private string[] m_Levels;
        public string[] Levels => m_Levels;

        /// <summary>
        /// �����������-������ �������
        /// </summary>
        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;
    }
}