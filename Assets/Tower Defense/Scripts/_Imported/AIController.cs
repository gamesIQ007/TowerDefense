using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]

    /// <summary>
    /// ������������� ���������
    /// </summary>
    public class AIController : MonoBehaviour
    {
        /// <summary>
        /// ������������ ��������� AI
        /// </summary>
        public enum AIBehaviour
        {
            Null,
            Patrol
        }

        /// <summary>
        /// ���������
        /// </summary>
        [SerializeField] private AIBehaviour m_AIBehaviour;

        /// <summary>
        /// ������� ��������������
        /// </summary>
        [SerializeField] private AIPointPatrol m_PatrolPoint;

        /// <summary>
        /// �������� �����������
        /// </summary>
        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLinear;

        /// <summary>
        /// �������� ��������
        /// </summary>
        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular;

        /// <summary>
        /// ������ ��������� �������
        /// </summary>
        [SerializeField] private float m_RandomSelectMovePointTime;

        /// <summary>
        /// ������ ��������� ����
        /// </summary>
        [SerializeField] private float m_FindNewTargetTime;

        /// <summary>
        /// ������ ��������
        /// </summary>
        [SerializeField] private float m_ShootDelay;

        /// <summary>
        /// ����� ��������
        /// </summary>
        [SerializeField] private float m_EvadeRayLength;

        /// <summary>
        /// ������ �� ���� �������
        /// </summary>
        private SpaceShip m_SpaceShip;

        /// <summary>
        /// ����� ����������
        /// </summary>
        private Vector3 m_MovePosition;

        /// <summary>
        /// ���� ��������������
        /// </summary>
        private enum PatrolType
        {
            Random,
            Route
        }

        /// <summary>
        /// ��� ��������������
        /// </summary>
        [SerializeField] private PatrolType m_PatrolType;

        /// <summary>
        /// ����� ��������������
        /// </summary>
        [SerializeField] private Vector3[] m_PatrolRoutePoints;

        /// <summary>
        /// ������� ����� ��������������
        /// </summary>
        private int m_CurrentPatrolPoint = 0;

        /// <summary>
        /// ����
        /// </summary>
        private Destructible m_SelectedTarget;

        /// <summary>
        /// ������ ����� �����������
        /// </summary>
        private Timer m_RandomizeDirectionTimer;

        /// <summary>
        /// ������ ��������
        /// </summary>
        private Timer m_FireTimer;

        /// <summary>
        /// ������ ������ ����� ����
        /// </summary>
        private Timer m_FindNewTargetTimer;

        #region Unity Events

        private void Start()
        {
            m_SpaceShip = GetComponent<SpaceShip>();

            InitTimers();

            //����� ��������� ����� ��������������
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
        /// ������������� ��������
        /// </summary>
        private void InitTimers()
        {
            m_RandomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime);
            m_FireTimer = new Timer(m_ShootDelay);
            m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
        }

        /// <summary>
        /// ���������� ��������
        /// </summary>
        private void UpdateTimers()
        {
            m_RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
            m_FireTimer.RemoveTime(Time.deltaTime);
            m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
        }

        #endregion

        /// <summary>
        /// ���������� ��
        /// </summary>
        private void UpdateAI()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                UpdateBehaviourPatrol();
            }
        }

        /// <summary>
        /// �������� ��������� ��� ��������������
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
        /// ����� ����� ���� �����������
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
        /// ���������� ���� � �����������
        /// </summary>
        /// <returns>������������ ���� � �����������</returns>
        private Vector3 MakeLead()
        {
            SpaceShip ship = m_SelectedTarget.GetComponent<SpaceShip>();
            Rigidbody2D rigid = ship.GetComponent<Rigidbody2D>();

            float k = (Mathf.Abs(rigid.velocity.x) + Mathf.Abs(rigid.velocity.y)) * ship.ThrustControl;

            return m_SelectedTarget.transform.position + m_SelectedTarget.transform.up * k;
        }

        /// <summary>
        /// ��������� ������������
        /// </summary>
        private void ActionEvadeCollision()
        {
            if (Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength))
            {
                m_MovePosition = transform.position + transform.right * 5.0f; // �������� ������ ������, ����� �� � ����������. �� ���� ����
            }
        }

        /// <summary>
        /// ���������� ������������
        /// </summary>
        private void ActionControlShip()
        {
            m_SpaceShip.ThrustControl = m_NavigationLinear;
            m_SpaceShip.TorqueControl = ComputeAliginTorqueNormalized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;
        }

        /// <summary>
        /// ������������ ���� ��������
        /// </summary>
        private const float MAX_ANGLE = 45.0f;

        /// <summary>
        /// ������ ��������� ��� �������� ����� ����� � �������
        /// </summary>
        /// <param name="targetPosition">����</param>
        /// <param name="ship">�������</param>
        /// <returns>��������� ��������</returns>
        private static float ComputeAliginTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

            return -angle;
        }

        /// <summary>
        /// ����� ����� ���� �����
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
        /// ����� ��������� ����
        /// </summary>
        /// <returns>����</returns>
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
        /// �����
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
        /// ������ ��������� ��������������
        /// </summary>
        /// <param name="point">������� ��������������</param>
        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBehaviour = AIBehaviour.Patrol;
            m_PatrolPoint = point;
        }

        /// <summary>
        /// ����� ��������� ����� ��������������
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
        /// ����� ��� ����������� ����� ��������
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