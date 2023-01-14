using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(AudioSource))]

    /// <summary>
    /// ������������� ������
    /// </summary>
    public class SoundPlayer : SingletonBase<SoundPlayer>
    {
        /// <summary>
        /// ������ ������
        /// </summary>
        [SerializeField] private Sounds m_Sounds;

        /// <summary>
        /// ���� ������� ������ �� ���������
        /// </summary>
        [SerializeField] private AudioClip m_BGM;

        private AudioSource m_AudioSource;

        private new void Awake()
        {
            base.Awake();
            m_AudioSource = GetComponent<AudioSource>();
            Instance.m_AudioSource.clip = m_BGM;
            Instance.m_AudioSource.Play();
        }

        public void Play(Sound sound)
        {
            m_AudioSource.PlayOneShot(m_Sounds[sound]);
        }
    }
}