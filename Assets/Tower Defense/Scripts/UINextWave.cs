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
        private EnemyWaveManager manager;

        /// <summary>
        /// Время до следующей волны
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
        /// Вызов следующей волны
        /// </summary>
        public void CallWave()
        {
            manager.ForceNextWave();
        }
    }
}