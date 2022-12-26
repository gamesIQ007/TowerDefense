using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// Интерфейс с условиями уровня
    /// </summary>
    public interface ILevelCondition
    {
        /// <summary>
        /// Пройден ли уровень
        /// </summary>
        bool IsCompleted { get; }
    }

    /// <summary>
    /// Контроллер уровня
    /// </summary>
    public class LevelController : SingletonBase<LevelController>
    {
        /// <summary>
        /// Требуемое время прохождения уровня
        /// </summary>
        [SerializeField] protected float m_ReferenceTime;
        public float ReferenceTime => m_ReferenceTime;

        /// <summary>
        /// Ивент при завершении уровня
        /// </summary>
        [SerializeField] protected UnityEvent m_LevelCompleted;

        /// <summary>
        /// Массив условий завершения уровня
        /// </summary>
        private ILevelCondition[] m_Conditions;

        /// <summary>
        /// Пройден ли уровень
        /// </summary>
        private bool m_IsLevelCompleted;

        /// <summary>
        /// Время прохождения уровня
        /// </summary>
        private float m_LevelTime;
        public float LevelTime => m_LevelTime;

        #region UnityEvents

        protected void Start()
        {
            m_Conditions = GetComponentsInChildren<ILevelCondition>();
        }

        private void Update()
        {
            if (m_IsLevelCompleted == false)
            {
                m_LevelTime += Time.deltaTime;

                CheckLevelConditions();
            }
        }

        #endregion

        /// <summary>
        /// Проверка условий завершения уровня
        /// </summary>
        private void CheckLevelConditions()
        {
            if (m_Conditions == null || m_Conditions.Length == 0) return;

            int numCompleted = 0;

            foreach (var v in m_Conditions)
            {
                if (v.IsCompleted)
                {
                    numCompleted++;
                }
            }

            if (numCompleted == m_Conditions.Length)
            {
                m_IsLevelCompleted = true;

                m_LevelCompleted?.Invoke();

                LevelSequenceController.Instance?.FinishCurrentLevel(true);
            }
        }
    }
}