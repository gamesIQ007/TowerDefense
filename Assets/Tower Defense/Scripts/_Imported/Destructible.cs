using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// ������������ ������. ��, ��� ����� ����� ���������.
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties

        /// <summary>
        /// ������ ���������� �����������.
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool IsIndestructible => m_Indestructible;

        /// <summary>
        /// ��������� ���������� ����������.
        /// </summary>
        [SerializeField] protected int m_HitPoints;
        public int MaxHitPoints => m_HitPoints;

        /// <summary>
        /// ������� ���������� ����������.
        /// </summary>
        private int m_CurrentHitPoints;
        public int HitPoints => m_CurrentHitPoints;

        /// <summary>
        /// �����, ������������ �� �������
        /// </summary>
        [SerializeField] protected UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        /// <summary>
        /// ����� ��������� ������������
        /// </summary>
        private float m_TimeOfTemporaryIndestructible;

        /// <summary>
        /// ����� ��� ��������� ������������
        /// </summary>
        [SerializeField] private UnityEvent m_EventOnEnableTemporaryIndestructible;

        /// <summary>
        /// ����� ��� ���������� ������������
        /// </summary>
        [SerializeField] private UnityEvent m_EventOnDisableTemporaryIndestructible;

        /// <summary>
        /// ID ����������� �������
        /// </summary>
        public const int TeamIdNeutral = 0;

        /// <summary>
        /// ID �������
        /// </summary>
        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;

        /// <summary>
        /// ���������� �����
        /// </summary>
        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;

        #endregion

        #region Unity Events

        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints;
        }

        protected virtual void Update()
        {
            if (m_TimeOfTemporaryIndestructible <= 0) return;

            m_TimeOfTemporaryIndestructible -= Time.deltaTime;

            if (m_TimeOfTemporaryIndestructible <= 0)
            {
                DisableTemporaryIndestructible();
            }
        }

        #endregion

        #region Public API

        /// <summary>
        /// ���������� ����� � �������.
        /// </summary>
        /// <param name="damage">��������� ������� ����.</param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return;

            m_CurrentHitPoints -= damage;
            if (m_CurrentHitPoints <= 0)
            {
                OnDeath();
            }
        }

        /// <summary>
        /// �������� ��������� ������������
        /// </summary>
        /// <param name="time">����� ��������� ������������</param>
        public void ApplyTemporaryIndestructible(float time)
        {
            m_TimeOfTemporaryIndestructible += time;
            m_Indestructible = true;
            m_EventOnEnableTemporaryIndestructible?.Invoke();
        }

        #endregion

        /// <summary>
        /// ���������������� ������� ����������� �������, ����� ��������� ������ 0.
        /// </summary>
        protected virtual void OnDeath()
        {
            Destroy(gameObject);
            m_EventOnDeath?.Invoke();
        }

        /// <summary>
        /// ��������� ��������� ������������
        /// </summary>
        private void DisableTemporaryIndestructible()
        {
            m_Indestructible = false;
            m_TimeOfTemporaryIndestructible = 0;
            m_EventOnDisableTemporaryIndestructible?.Invoke();
        }

        #region Destructible Collection

        /// <summary>
        /// ������ ���� ������������ ��������. HashSet - ������ List, ������ �������� �������
        /// </summary>
        private static HashSet<Destructible> m_AllDestructibles;
        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        protected virtual void OnEnable()
        {
            if (m_AllDestructibles == null)
            {
                m_AllDestructibles = new HashSet<Destructible>();
            }

            m_AllDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructibles.Remove(this);
        }

        #endregion
    }
}
