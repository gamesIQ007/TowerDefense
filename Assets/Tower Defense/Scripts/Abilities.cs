using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// Способности игрока
    /// </summary>
    public class Abilities : SingletonBase<Abilities>
    {
        /// <summary>
        /// Способность урона на площади
        /// </summary>
        [Serializable]
        public class FireAbility
        {
            /// <summary>
            /// Стоимость
            /// </summary>
            [SerializeField] private int m_Cost = 50;

            /// <summary>
            /// Урон
            /// </summary>
            [SerializeField] private int m_Damage = 10;

            /// <summary>
            /// Цвет прицела
            /// </summary>
            [SerializeField] private Color m_TargetingColor;

            public void Use()
            {
                ClickProtection.Instance.Activate((Vector2 v) =>
                {
                    Vector3 position = v;
                    position.z = -Camera.main.transform.position.z;
                    position = Camera.main.ScreenToWorldPoint(position);
                    foreach (var collider in Physics2D.OverlapCircleAll(position, 5))//-------------------------------!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    { 
                        if (collider.transform.parent.TryGetComponent<Enemy>(out var enemy))
                        {
                            enemy.TakeDamage(m_Damage, Projectile.DamageType.Magic);
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Способность временного замедления врагов
        /// </summary>
        [Serializable]
        public class TimeAbility
        {
            /// <summary>
            /// Стоимость
            /// </summary>
            [SerializeField] private int m_Cost = 50;

            /// <summary>
            /// Длительность
            /// </summary>
            [SerializeField] private float m_Duration = 5;

            /// <summary>
            /// Частота использования
            /// </summary>
            [SerializeField] private int m_Cooldown = 15;

            public void Use()
            {
                void Slow(Enemy enemy)
                {
                    enemy.GetComponent<SpaceShip>().HalfMaxLinearVelocity();
                }

                foreach (var ship in FindObjectsOfType<SpaceShip>())
                {
                    ship.HalfMaxLinearVelocity();
                }
                EnemyWaveManager.OnEnemySpawn += Slow;

                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_Duration);
                    foreach (var ship in FindObjectsOfType<SpaceShip>())
                    {
                        ship.RestoreMaxLinearVelocity();
                    }
                    EnemyWaveManager.OnEnemySpawn -= Slow;
                }

                Instance.StartCoroutine(Restore());

                IEnumerator TimeAbilityButton()
                {
                    Instance.m_TimeButton.interactable = false;
                    yield return new WaitForSeconds(m_Cooldown);
                    Instance.m_TimeButton.interactable = true;

                }

                Instance.StartCoroutine(TimeAbilityButton());
            }
        }

        /// <summary>
        /// Круг-прицел
        /// </summary>
        [SerializeField] private Image m_TargetingCircle;

        /// <summary>
        /// Кнопка способности замедления времени
        /// </summary>
        [SerializeField] private Button m_TimeButton;

        [SerializeField] private FireAbility m_FireAbility;
        public void UseFireAbility() => m_FireAbility.Use();

        [SerializeField] private TimeAbility m_TimeAbility;
        public void UseTimeAbility() => m_TimeAbility.Use();
    }
}