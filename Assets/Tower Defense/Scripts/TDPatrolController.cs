using UnityEngine;
using UnityEngine.Events;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// Контроллер патрулирования по маршруту
    /// </summary>
    public class TDPatrolController : AIController
    {
        /// <summary>
        /// Путь
        /// </summary>
        private Path m_Path;

        /// <summary>
        /// Индекс точки пути
        /// </summary>
        private int m_PathIndex;

        /// <summary>
        /// Событие при достижении конца маршрута
        /// </summary>
        [SerializeField] private UnityEvent OnEndPath;

        /// <summary>
        /// Задать путь
        /// </summary>
        /// <param name="path">Задаваемый путь</param>
        public void SetPath(Path newPath)
        {
            m_Path = newPath;
            m_PathIndex = 0;
            SetPatrolBehaviour(m_Path[m_PathIndex]);
        }

        /// <summary>
        /// Поиск следующей точки патрулирования
        /// </summary>
        protected override void GetNewPointPatrol()
        {
            m_PathIndex++;

            if (m_Path.Length > m_PathIndex)
            {
                SetPatrolBehaviour(m_Path[m_PathIndex]);
            }
            else
            {
                OnEndPath.Invoke();
                Destroy(gameObject);
            }
        }
    }
}