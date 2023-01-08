using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// ����� ��������� �����
    /// </summary>
    public class UINextWave : MonoBehaviour
    {
        /// <summary>
        /// �����, ������������ ���������� ������
        /// </summary>
        [SerializeField] private Text m_BonusAmount;

        /// <summary>
        /// �������� ���� ������
        /// </summary>
        private EnemyWaveManager m_Manager;

        /// <summary>
        /// ����� �� ��������� �����
        /// </summary>
        private float m_TimeToNextWave;

        private void Start()
        {
            m_Manager = FindObjectOfType<EnemyWaveManager>();
            EnemyWave.OnWavePrepare += (float time) =>
            {
                m_TimeToNextWave = time;
            };
        }

        private void Update()
        {
            m_TimeToNextWave -= Time.deltaTime;

            int bonus = (int)m_TimeToNextWave;

            if (bonus < 0)
            {
                bonus = 0;
            }
            m_BonusAmount.text = bonus.ToString();
        }

        /// <summary>
        /// ����� ��������� �����
        /// </summary>
        public void CallWave()
        {
            m_Manager.ForceNextWave();
        }
    }
}