using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TowerDefense;

namespace SpaceShooter
{
    /// <summary>
    /// Уничтожаемый объект. То, что может иметь хитпоинты.
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties

        /// <summary>
        /// Объект игнорирует повреждения.
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool IsIndestructible => m_Indestructible;

        /// <summary>
        /// Стартовое количество хитпоинтов.
        /// </summary>
        [SerializeField] protected int m_HitPoints;
        public int MaxHitPoints => m_HitPoints;

        /// <summary>
        /// Текущее количество хитпоинтов.
        /// </summary>
        private int m_CurrentHitPoints;
        public int HitPoints => m_CurrentHitPoints;

        /// <summary>
        /// Ивент, происходящий со смертью
        /// </summary>
        [SerializeField] protected UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        /// <summary>
        /// Время временной неуязвимости
        /// </summary>
        private float m_TimeOfTemporaryIndestructible;

        /// <summary>
        /// Ивент при включении неуязвимости
        /// </summary>
        [SerializeField] private UnityEvent m_EventOnEnableTemporaryIndestructible;

        /// <summary>
        /// Ивент при выключении неуязвимости
        /// </summary>
        [SerializeField] private UnityEvent m_EventOnDisableTemporaryIndestructible;

        /// <summary>
        /// ID нейтральной команды
        /// </summary>
        public const int TeamIdNeutral = 0;

        /// <summary>
        /// ID команды
        /// </summary>
        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;

        /// <summary>
        /// Количество очков
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
        /// Применение урона к объекту.
        /// </summary>
        /// <param name="damage">Наносимый объекту урон.</param>
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
        /// Включить временную неуязвимость
        /// </summary>
        /// <param name="time">Время временной неуязвимости</param>
        public void ApplyTemporaryIndestructible(float time)
        {
            m_TimeOfTemporaryIndestructible += time;
            m_Indestructible = true;
            m_EventOnEnableTemporaryIndestructible?.Invoke();
        }

        /// <summary>
        /// Применить настройки из ScriptableObject к ассету
        /// </summary>
        /// <param name="asset">Настройки</param>
        protected void Use(EnemyAsset asset)
        {
            m_HitPoints = asset.hp;
            m_ScoreValue = asset.score;
        }

        #endregion

        /// <summary>
        /// Переопределяемое событие уничтожения объекта, когда хитпоинты меньше 0.
        /// </summary>
        protected virtual void OnDeath()
        {
            Destroy(gameObject);
            m_EventOnDeath?.Invoke();
        }

        /// <summary>
        /// Отключить временную неуязвимость
        /// </summary>
        private void DisableTemporaryIndestructible()
        {
            m_Indestructible = false;
            m_TimeOfTemporaryIndestructible = 0;
            m_EventOnDisableTemporaryIndestructible?.Invoke();
        }

        #region Destructible Collection

        /// <summary>
        /// Список всех уничтожаемых объектов. HashSet - аналог List, иногда работает быстрее
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
            if (m_AllDestructibles != null)
            {
                m_AllDestructibles.Remove(this);
            }
        }

        #endregion
    }
}
