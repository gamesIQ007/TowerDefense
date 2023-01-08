using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefense
{
    [RequireComponent(typeof(CircleCollider2D))]
    /// <summary>
    /// Снаряд и его поведение
    /// </summary>
    public class Projectile : Entity
    {
        /// <summary>
        /// Перечисление типов урона
        /// </summary>
        public enum DamageType
        {
            /// <summary>
            /// Базовый тип урона
            /// </summary>
            Base,
            /// <summary>
            /// Магический тип урона
            /// </summary>
            Magic,
            /// <summary>
            /// Взрывной тип урона
            /// </summary>
            Explosion
        }

        /// <summary>
        /// Тип урона
        /// </summary>
        [SerializeField] private DamageType m_DamageType;

        /// <summary>
        /// Скорость снаряда
        /// </summary>
        [SerializeField] private float m_Velocity;

        /// <summary>
        /// Время жизни
        /// </summary>
        [SerializeField] private float m_LifeTime;

        /// <summary>
        /// Урон
        /// </summary>
        [SerializeField] private int m_Damage;

        /// <summary>
        /// Префаб посмертного эффекта
        /// </summary>
        [SerializeField] private ImpactEffect m_ImpactEffectPrefab;

        /// <summary>
        /// Таймер
        /// </summary>
        private float m_Timer;

        /// <summary>
        /// Дестрактибл родителя
        /// </summary>
        private Destructible m_Parent;
        public Destructible Parent => m_Parent;

        /// <summary>
        /// Признак самонаведения
        /// </summary>
        [SerializeField] private bool m_IsHoming;

        /// <summary>
        /// Цель самонаведения
        /// </summary>
        private Destructible m_HomingTarget;

        /// <summary>
        /// Наносит ли урон по области
        /// </summary>
        [SerializeField] private bool m_IsAreaDamage;

        /// <summary>
        /// Радиус урона по области
        /// </summary>
        [SerializeField] private float m_Radius;

        /// <summary>
        /// Список врагов в зоне поражения
        /// </summary>
        List<Enemy> m_EnemiesInArea;

        [Header("Effect")]
        /// <summary>
        /// Накладываемый эффект
        /// </summary>
        [SerializeField] private EnemyState m_State;

        /// <summary>
        /// Время эффекта
        /// </summary>
        [SerializeField] private int m_StateTime;

        #region Unity Events

        private void Start()
        {
            if (m_IsAreaDamage)
            {
                m_EnemiesInArea = new List<Enemy>();
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
                Enemy enemy = hit.collider.transform.root.GetComponent<Enemy>();

                if (enemy != null && m_IsAreaDamage == false)
                {
                    ApplyDamage(enemy);
                }

                if (m_IsAreaDamage)
                {
                    for (int i = 0; i < m_EnemiesInArea.Count; i++)
                    {
                        ApplyDamage(m_EnemiesInArea[i]);
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
                m_EnemiesInArea.Add(other.transform.root.GetComponent<Enemy>());
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (m_IsAreaDamage)
            {
                m_EnemiesInArea.Remove(other.transform.root.GetComponent<Enemy>());
            }
        }

        #endregion

        #region Public API

        /// <summary>
        /// Установить родителя проджектайла
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

        private void ApplyDamage(Enemy enemy)
        {
            enemy.TakeDamage(m_Damage, m_DamageType);

            if (m_State == EnemyState.Freezed)
            {
                enemy.GetComponent<SpaceShip>().ChangeState(m_StateTime);
            }
        }

        /// <summary>
        /// Действие при конце жизни снаряда
        /// </summary>
        /// <param name="col">Коллайдер</param>
        /// <param name="pos">Позиция</param>
        private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            if (m_ImpactEffectPrefab)
            {
                Instantiate(m_ImpactEffectPrefab, transform.position, Quaternion.identity);
            }
            
            Destroy(gameObject);
        }

        /// <summary>
        /// Поиск ближайшего корабля
        /// </summary>
        /// <param name="spaceShip">Корабль, запустивший снаряд</param>
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
        /// Цвет гизмо
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