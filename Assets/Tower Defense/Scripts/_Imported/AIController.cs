using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]

    /// <summary>
    /// Искусственный интеллект
    /// </summary>
    public class AIController : MonoBehaviour
    {
        /// <summary>
        /// Перечисление поведений AI
        /// </summary>
        public enum AIBehaviour
        {
            Null,
            Patrol
        }

        /// <summary>
        /// Поведение
        /// </summary>
        [SerializeField] private AIBehaviour m_AIBehaviour;

        /// <summary>
        /// Область патрулирования
        /// </summary>
        [SerializeField] private AIPointPatrol m_PatrolPoint;

        /// <summary>
        /// Скорость перемещения
        /// </summary>
        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLinear;

        /// <summary>
        /// Скорость вращения
        /// </summary>
        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular;

        /// <summary>
        /// Таймер изменения позиции
        /// </summary>
        [SerializeField] private float m_RandomSelectMovePointTime;

        /// <summary>
        /// Таймер изменения цели
        /// </summary>
        [SerializeField] private float m_FindNewTargetTime;

        /// <summary>
        /// Таймер стрельбы
        /// </summary>
        [SerializeField] private float m_ShootDelay;

        /// <summary>
        /// Длина рейкаста
        /// </summary>
        [SerializeField] private float m_EvadeRayLength;

        /// <summary>
        /// Ссылка на свой корабль
        /// </summary>
        private SpaceShip m_SpaceShip;

        /// <summary>
        /// Точка назначения
        /// </summary>
        private Vector3 m_MovePosition;

        /// <summary>
        /// Типы патрулирования
        /// </summary>
        private enum PatrolType
        {
            Random,
            Route
        }

        /// <summary>
        /// Тип патрулирования
        /// </summary>
        [SerializeField] private PatrolType m_PatrolType;

        /// <summary>
        /// Точки патрулирования
        /// </summary>
        [SerializeField] private Vector3[] m_PatrolRoutePoints;

        /// <summary>
        /// Текущая точка патрулирования
        /// </summary>
        private int m_CurrentPatrolPoint = 0;

        /// <summary>
        /// Цель
        /// </summary>
        private Destructible m_SelectedTarget;

        /// <summary>
        /// Таймер смены направления
        /// </summary>
        private Timer m_RandomizeDirectionTimer;

        /// <summary>
        /// Таймер стрельбы
        /// </summary>
        private Timer m_FireTimer;

        /// <summary>
        /// Таймер поиска новой цели
        /// </summary>
        private Timer m_FindNewTargetTimer;

        #region Unity Events

        private void Start()
        {
            m_SpaceShip = GetComponent<SpaceShip>();

            InitTimers();

            //Задаём начальную точку патрулирования
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                if (m_PatrolPoint != null)
                {
                    if (m_PatrolType == PatrolType.Random)
                    {
                        bool isInsidePatrolZone = (m_PatrolPoint.transform.position - transform.position).sqrMagnitude < m_PatrolPoint.Radius * m_PatrolPoint.Radius;

                        if (isInsidePatrolZone)
                        {
                            Vector2 newPoint = Random.onUnitSphere * m_PatrolPoint.Radius + m_PatrolPoint.transform.position;

                            m_MovePosition = newPoint;

                            m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                        }
                        else
                        {
                            m_MovePosition = m_PatrolPoint.transform.position;
                        }
                    }
                }

                if (m_PatrolType == PatrolType.Route)
                {
                    m_MovePosition = m_PatrolRoutePoints[m_CurrentPatrolPoint];
                }
            }
        }

        private void Update()
        {
            UpdateTimers();

            UpdateAI();
        }

        #endregion

        #region Timers

        /// <summary>
        /// Инициализация таймеров
        /// </summary>
        private void InitTimers()
        {
            m_RandomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime);
            m_FireTimer = new Timer(m_ShootDelay);
            m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
        }

        /// <summary>
        /// Обновление таймеров
        /// </summary>
        private void UpdateTimers()
        {
            m_RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
            m_FireTimer.RemoveTime(Time.deltaTime);
            m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
        }

        #endregion

        /// <summary>
        /// Обновление ИИ
        /// </summary>
        private void UpdateAI()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                UpdateBehaviourPatrol();
            }
        }

        /// <summary>
        /// Обновить поведение при патрулировании
        /// </summary>
        private void UpdateBehaviourPatrol()
        {
            ActionFindNewMovePosition();
            ActionControlShip();
            ActionFindNewAttackTarget();
            ActionFire();
            ActionEvadeCollision();
        }

        /// <summary>
        /// Найти новую цель перемещения
        /// </summary>
        private void ActionFindNewMovePosition()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                if (m_SelectedTarget != null)
                {
                    m_MovePosition = MakeLead();
                }
                else
                {
                    if (m_PatrolPoint != null)
                    {
                        if (m_PatrolType == PatrolType.Random)
                        {
                            bool isInsidePatrolZone = (m_PatrolPoint.transform.position - transform.position).sqrMagnitude < m_PatrolPoint.Radius * m_PatrolPoint.Radius;

                            if (isInsidePatrolZone)
                            {
                                GetNewPointPatrol();
                            }
                            else
                            {
                                m_MovePosition = m_PatrolPoint.transform.position;
                            }
                        }
                    }

                    if (m_PatrolType == PatrolType.Route)
                    {
                        if (m_PatrolRoutePoints.Length < 2)
                        {
                            m_PatrolType = PatrolType.Random;
                        }

                        if (m_RandomizeDirectionTimer.IsFinished)
                        {
                            m_CurrentPatrolPoint++;

                            if (m_CurrentPatrolPoint == m_PatrolRoutePoints.Length)
                            {
                                m_CurrentPatrolPoint = 0;
                            }

                            m_MovePosition = m_PatrolRoutePoints[m_CurrentPatrolPoint];

                            m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Следование цели с упреждением
        /// </summary>
        /// <returns>Расположение цели с упреждением</returns>
        private Vector3 MakeLead()
        {
            SpaceShip ship = m_SelectedTarget.GetComponent<SpaceShip>();
            Rigidbody2D rigid = ship.GetComponent<Rigidbody2D>();

            float k = (Mathf.Abs(rigid.velocity.x) + Mathf.Abs(rigid.velocity.y)) * ship.ThrustControl;

            return m_SelectedTarget.transform.position + m_SelectedTarget.transform.up * k;
        }

        /// <summary>
        /// Избегание столкновений
        /// </summary>
        private void ActionEvadeCollision()
        {
            if (Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength))
            {
                m_MovePosition = transform.position + transform.right * 5.0f; // довольно унылый уворот, можно бы и переделать. Но суть ясна
            }
        }

        /// <summary>
        /// Управление перемещением
        /// </summary>
        private void ActionControlShip()
        {
            m_SpaceShip.ThrustControl = m_NavigationLinear;
            m_SpaceShip.TorqueControl = ComputeAliginTorqueNormalized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;
        }

        /// <summary>
        /// Максимальный угол поворота
        /// </summary>
        private const float MAX_ANGLE = 45.0f;

        /// <summary>
        /// Расчёт множителя для поворота между целью и кораблём
        /// </summary>
        /// <param name="targetPosition">Цель</param>
        /// <param name="ship">Корабль</param>
        /// <returns>Множитель поворота</returns>
        private static float ComputeAliginTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

            return -angle;
        }

        /// <summary>
        /// Поиск новой цели атаки
        /// </summary>
        private void ActionFindNewAttackTarget()
        {
            if (m_FindNewTargetTimer.IsFinished)
            {
                m_SelectedTarget = FindNearestDestructibleTarget();

                m_FindNewTargetTimer.Start(m_FindNewTargetTime);
            }
        }

        /// <summary>
        /// Найти ближайшую цель
        /// </summary>
        /// <returns>Цель</returns>
        private Destructible FindNearestDestructibleTarget()
        {
            float maxDistance = float.MaxValue;
            Destructible potencialTarget = null;

            foreach (var v in Destructible.AllDestructibles)
            {
                if (v.GetComponent<SpaceShip>() == m_SpaceShip) continue;

                if (v.TeamId == Destructible.TeamIdNeutral) continue;

                if (v.TeamId == m_SpaceShip.TeamId) continue;

                float dist = Vector2.Distance(m_SpaceShip.transform.position, v.transform.position);

                if (dist < maxDistance)
                {
                    maxDistance = dist;
                    potencialTarget = v;
                }
            }

            return potencialTarget;
        }

        /// <summary>
        /// Атака
        /// </summary>
        private void ActionFire()
        {
            if (m_SelectedTarget != null)
            {
                if (m_FireTimer.IsFinished)
                {
                    m_SpaceShip.Fire(TurretMode.Primary);

                    m_FireTimer.Start(m_ShootDelay);
                }
            }
        }

        /// <summary>
        /// Задать поведение патрулирования
        /// </summary>
        /// <param name="point">Область патрулирования</param>
        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBehaviour = AIBehaviour.Patrol;
            m_PatrolPoint = point;
        }

        /// <summary>
        /// Поиск следующей точки патрулирования
        /// </summary>
        protected virtual void GetNewPointPatrol()
        {
            if (m_RandomizeDirectionTimer.IsFinished)
            {
                Vector2 newPoint = Random.onUnitSphere * m_PatrolPoint.Radius + m_PatrolPoint.transform.position;

                m_MovePosition = newPoint;

                m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
            }
        }

        /// <summary>
        /// Гизмо для определения длины рейкаста
        /// </summary>
        private static readonly Color GizmoColor = new Color(1, 0, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = GizmoColor;
            Gizmos.DrawLine(transform.position, transform.position + transform.up * m_EvadeRayLength);
            Gizmos.DrawLine(transform.position, transform.position + transform.right * 5.0f);
        }
    }
}