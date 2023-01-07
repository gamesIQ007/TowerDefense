using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// Новый контроллер уровня с поддержкой провала уровня
    /// </summary>
    public class TDLevelController : LevelController
    {
        /// <summary>
        /// Очки прохождения уровня
        /// </summary>
        private int m_LevelScore = 3;

        /// <summary>
        /// Апгрейд времени на уровень
        /// </summary>
        [SerializeField] private UpgradeAsset m_TimeUpgrade;

        private new void Start()
        {
            base.Start();
            // дэто сейчас будет лямбда-функция. Безымянная функция без параметров, которая будет вызываться на событие OnPlayerDead. Это шоб не писать отдельную функцию.
            TDPlayer.Instance.OnPlayerDead += () =>
            {
                StopLevelActivity();
                LevelResultController.Instance.ShowResult(false);
            };

            m_ReferenceTime += Time.time;
            m_LevelCompleted.AddListener(() =>
            {
                StopLevelActivity();

                if (m_ReferenceTime < Time.time)
                {
                    m_LevelScore--;
                }
                
                MapCompletion.SaveEpisodeResult(m_LevelScore);
            });

            // Локальная функция, чисто для отслеживания изменения количества жизней, одноразовая подписка
            void LifeScoreChange(int _)
            {
                m_LevelScore--;
                TDPlayer.OnLifeUpdate -= LifeScoreChange;
            }
            TDPlayer.OnLifeUpdate += LifeScoreChange;
        }

        private new void Awake()
        {
            base.Awake();
            int timeUpgrade = (int)(Upgrades.GetUpgradeLevel(m_TimeUpgrade) * Upgrades.GetUpgradeModifier(m_TimeUpgrade));
            m_ReferenceTime += timeUpgrade;
        }

        /// <summary>
        /// Остановка активности на уровне
        /// </summary>
        private void StopLevelActivity()
        {
            foreach (var enemy in FindObjectsOfType<Enemy>())
            {
                enemy.GetComponent<SpaceShip>().enabled = false;
                enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }

            // Ищем все объекты определённого класса и выключаем их
            void DisableAll<T>() where T : MonoBehaviour
            {
                foreach (var obj in FindObjectsOfType<T>())
                {
                    obj.enabled = false;
                }
            }

            DisableAll<Spawner>();
            DisableAll<Projectile>();
            DisableAll<Tower>();
            DisableAll<UINextWave>();
        }
    }
}