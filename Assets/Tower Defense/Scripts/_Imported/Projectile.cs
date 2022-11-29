using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    /// <summary>
    /// ������ � ��� ���������
    /// </summary>
    public class Projectile : Entity
    {
        /// <summary>
        /// �������� �������
        /// </summary>
        [SerializeField] private float m_Velocity;

        /// <summary>
        /// ����� �����
        /// </summary>
        [SerializeField] private float m_LifeTime;

        /// <summary>
        /// ����
        /// </summary>
        [SerializeField] private int m_Damage;

        /// <summary>
        /// ������ ����������� �������
        /// </summary>
        [SerializeField] private ImpactEffect m_ImpactEffectPrefab;

        /// <summary>
        /// ������
        /// </summary>
        private float m_Timer;

        /// <summary>
        /// ����������� ��������
        /// </summary>
        private Destructible m_Parent;
        public Destructible Parent => m_Parent;

        /// <summary>
        /// ������� �������������
        /// </summary>
        [SerializeField] private bool m_IsHoming;

        /// <summary>
        /// ���� �������������
        /// </summary>
        private Destructible m_HomingTarget;

        /// <summary>
        /// ������� �� ���� �� �������
        /// </summary>
        [SerializeField] private bool m_IsAreaDamage;

        /// <summary>
        /// ������ ����� �� �������
        /// </summary>
        [SerializeField] private float m_Radius;

        /// <summary>
        /// ������ ������������� � ���� ���������
        /// </summary>
        List<Destructible> m_DestructiblesInArea;

        #region Unity Events

        private void Start()
        {
            if (m_IsAreaDamage)
            {
                m_DestructiblesInArea = new List<Destructible>();
            }
        }

        private void Update()
        {
            m_Timer += Time.deltaTime;

            if (m_Timer > m_LifeTime)
            {
                Destroy(gameObject);
            }

            float stepLength = m_Velocity * Time.deltaTime;
            Vector2 step;
            
            if (m_IsHoming && m_HomingTarget != null)
            {
                step = (m_HomingTarget.transform.position - transform.position).normalized * stepLength;
            }
            else
            {
                step = transform.up * stepLength;
            }

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            if (hit)
            {
                Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

                if (dest != null && dest != m_Parent && m_IsAreaDamage == false)
                {
                    ApplyDamage(dest);
                }

                if (m_IsAreaDamage)
                {
                    for (int i = 0; i < m_DestructiblesInArea.Count; i++)
                    {
                        ApplyDamage(m_DestructiblesInArea[i]);
                    }
                }

                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            transform.position += new Vector3(step.x, step.y, 0);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (m_IsAreaDamage)
            {
                m_DestructiblesInArea.Add(other.transform.root.GetComponent<Destructible>());
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (m_IsAreaDamage)
            {
                m_DestructiblesInArea.Remove(other.transform.root.GetComponent<Destructible>());
            }
        }

        #endregion

        #region Public API

        /// <summary>
        /// ���������� �������� ������������
        /// </summary>
        /// <param name="parent"></param>
        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;

            if (m_IsHoming)
            {
                m_HomingTarget = FindNearestDestructibleTarget(parent.GetComponent<SpaceShip>());
            }
        }

        #endregion

        private void ApplyDamage(Destructible destructible)
        {
            destructible.ApplyDamage(m_Damage);

            if (m_Parent == Player.Instance.ActiveShip)
            {
                Player.Instance.AddScore(destructible.ScoreValue);

                if (destructible.HitPoints < 0)
                {
                    Player.Instance.AddKill();
                }
            }
        }

        /// <summary>
        /// �������� ��� ����� ����� �������
        /// </summary>
        /// <param name="col">���������</param>
        /// <param name="pos">�������</param>
        private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            Instantiate(m_ImpactEffectPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }

        /// <summary>
        /// ����� ���������� �������
        /// </summary>
        /// <param name="spaceShip">�������, ����������� ������</param>
        /// <returns></returns>
        private Destructible FindNearestDestructibleTarget(SpaceShip spaceShip)
        {
            float maxDistance = float.MaxValue;
            Destructible potencialTarget = null;

            foreach (var destructible in FindObjectsOfType<Destructible>())
            {
                if (destructible.GetComponent<SpaceShip>() == spaceShip) continue;

                float dist = Vector2.Distance(spaceShip.transform.position, destructible.transform.position);

                if (dist < maxDistance)
                {
                    maxDistance = dist;
                    potencialTarget = destructible;
                }
            }

            return potencialTarget;
        }

#if UNITY_EDITOR
        /// <summary>
        /// ���� �����
        /// </summary>
        private static Color GizmoColor = new Color(0, 1, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Handles.color = GizmoColor;
            Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius);
        }

        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_Radius;
        }
#endif
    }
}