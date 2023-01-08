using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// Вызов следующей волны
    /// </summary>
    public class UINextWave : MonoBehaviour
    {
        /// <summary>
        /// Текст, отображающий количество золота
        /// </summary>
        [SerializeField] private Text m_BonusAmount;

        /// <summary>
        /// Менеджер волн врагов
        /// </summary>
        private EnemyWaveManager m_Manager;

        /// <summary>
        /// Время до следующей волны
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
        /// Вызов следующей волны
        /// </summary>
        public void CallWave()
        {
            m_Manager.ForceNextWave();
        }
    }
}