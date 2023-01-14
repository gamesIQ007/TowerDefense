using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ’ук дл€ проигрывани€ звуков
    /// </summary>
    public class SoundHook : MonoBehaviour
    {
        public Sound m_Sound;

        public void Play()
        {
            m_Sound.Play();
        }
    }
}