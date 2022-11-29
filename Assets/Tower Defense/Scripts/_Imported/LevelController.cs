using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// ��������� � ��������� ������
    /// </summary>
    public interface ILevelCondition
    {
        /// <summary>
        /// ������� �� �������
        /// </summary>
        bool IsCompleted { get; }
    }

    /// <summary>
    /// ���������� ������
    /// </summary>
    public class LevelController : SingletonBase<LevelController>
    {
        /// <summary>
        /// ��������� ����� ����������� ������
        /// </summary>
        [SerializeField] private int m_ReferenceTime;
        public int ReferenceTime => m_ReferenceTime;

        /// <summary>
        /// ����� ��� ���������� ������
        /// </summary>
        [SerializeField] private UnityEvent m_LevelCompleted;

        /// <summary>
        /// ������ ������� ���������� ������
        /// </summary>
        private ILevelCondition[] m_Conditions;

        /// <summary>
        /// ������� �� �������
        /// </summary>
        private bool m_IsLevelCompleted;

        /// <summary>
        /// ����� ����������� ������
        /// </summary>
        private float m_LevelTime;
        public float LevelTime => m_LevelTime;

        #region UnityEvents

        private void Start()
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
        /// �������� ������� ���������� ������
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