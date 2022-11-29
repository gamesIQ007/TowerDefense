using UnityEngine;

namespace SpaceShooter
{

    /// <summary>
    /// Самоуничтожение через заданное время
    /// </summary>
    public class ImpactEffect : MonoBehaviour
    {
        /// <summary>
        /// Время жизни
        /// </summary>
        [SerializeField] private float m_LifeTime;

        /// <summary>
        /// Таймер
        /// </summary>
        private float m_Timer;

        #region Unity Events

        private void Update()
        {
            if (m_Timer < m_LifeTime)
            {
                m_Timer += Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}