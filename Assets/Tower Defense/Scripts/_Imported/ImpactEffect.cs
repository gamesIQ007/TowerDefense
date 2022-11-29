using UnityEngine;

namespace SpaceShooter
{

    /// <summary>
    /// ��������������� ����� �������� �����
    /// </summary>
    public class ImpactEffect : MonoBehaviour
    {
        /// <summary>
        /// ����� �����
        /// </summary>
        [SerializeField] private float m_LifeTime;

        /// <summary>
        /// ������
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