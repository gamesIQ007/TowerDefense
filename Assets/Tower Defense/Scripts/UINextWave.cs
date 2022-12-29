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
        private EnemyWaveManager manager;

        /// <summary>
        /// ����� �� ��������� �����
        /// </summary>
        private float timeToNextWave;

        private void Start()
        {
            manager = FindObjectOfType<EnemyWaveManager>();
            EnemyWave.OnWavePrepare += (float time) =>
            {
                timeToNextWave = time;
            };
        }

        private void Update()
        {
            timeToNextWave -= Time.deltaTime;

            int bonus = (int)timeToNextWave;

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
            manager.ForceNextWave();
        }
    }
}