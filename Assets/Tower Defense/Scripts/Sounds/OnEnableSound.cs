using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Проигрывание звука при активации
    /// </summary>
    public class OnEnableSound : MonoBehaviour
    {
        [SerializeField] private Sound m_Sound;

        private void OnEnable()
        {
            m_Sound.Play();
        }
    }
}