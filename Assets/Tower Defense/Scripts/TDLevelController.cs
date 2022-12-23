using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// Новый контроллер уровня с поддержкой провала уровня
    /// </summary>
    public class TDLevelController : LevelController
    {
        private new void Start()
        {
            base.Start();
            // дэто сейчас будет лямбда-функция. Безымянная функция без параметров, которая будет вызываться на событие OnPlayerDead. Это шоб не писать отдельную функцию.
            TDPlayer.Instance.OnPlayerDead += () =>
            {
                StopLevelActivity();
                LevelResultController.Instance.ShowResult(false);
            };
            m_LevelCompleted.AddListener(StopLevelActivity);
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
        }
    }
}