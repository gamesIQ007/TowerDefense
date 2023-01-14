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
            public int Cost => m_Cost;

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
                if (TDPlayer.Instance.Gold >= m_Cost)
                {
                    TDPlayer.Instance.ChangeGold(-m_Cost);

                    ClickProtection.Instance.Activate((Vector2 v) =>
                    {
                        Vector3 position = v;
                        position.z = -Camera.main.transform.position.z;
                        position = Camera.main.ScreenToWorldPoint(position);
                        foreach (var collider in Physics2D.OverlapCircleAll(position, 5))//-------------------------------!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        {
                            if (collider.transform.parent.TryGetComponent<Enemy>(out var enemy))
                            {
                                enemy.TakeDamage(m_Damage * Upgrades.GetUpgradeLevel(Instance.m_FireAbilityAsset), Projectile.DamageType.Magic);
                            }
                        }
                    });
                }
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
            [SerializeField] private int m_Cost = 25;
            public int Cost => m_Cost;

            /// <summary>
            /// Длительность
            /// </summary>
            [SerializeField] private float m_Duration = 5;

            /// <summary>
            /// Частота использования
            /// </summary>
            [SerializeField] private int m_Cooldown = 15;

            /// <summary>
            /// Активность времени перезарядки использования заклинания
            /// </summary>
            private bool m_CooldownIsActive = false;
            public bool CooldownIsActive => m_CooldownIsActive;

            public void Use()
            {
                if (TDPlayer.Instance.Mana >= m_Cost)
                {
                    TDPlayer.Instance.ReduceMana(m_Cost);

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
                        yield return new WaitForSeconds(m_Duration * Upgrades.GetUpgradeLevel(Instance.m_TimeAbilityAsset));
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
                        m_CooldownIsActive = true;
                        yield return new WaitForSeconds(m_Cooldown);
                        Instance.m_TimeButton.interactable = true;
                        m_CooldownIsActive = false;

                    }

                    Instance.StartCoroutine(TimeAbilityButton());
                }
            }
        }

        [Header("Fire Ability Settings")]

        [SerializeField] private FireAbility m_FireAbility;
        public void UseFireAbility() => m_FireAbility.Use();

        /// <summary>
        /// Круг-прицел
        /// </summary>
        [SerializeField] private Image m_TargetingCircle;
        [SerializeField] private GameObject m_FireAbilityPanel;
        [SerializeField] private Button m_FireButton;
        [SerializeField] private Text m_FireCostText;
        [SerializeField] private UpgradeAsset m_FireAbilityAsset;

        [Header("Time Ability Settings")]
        [SerializeField] private TimeAbility m_TimeAbility;
        public void UseTimeAbility() => m_TimeAbility.Use();

        [SerializeField] private GameObject m_TimeAbilityPanel;
        [SerializeField] private Button m_TimeButton;
        [SerializeField] private Text m_TimeCostText;
        [SerializeField] private UpgradeAsset m_TimeAbilityAsset;
                
        private void Start()
        {
            m_FireAbilityPanel.SetActive(Upgrades.GetUpgradeLevel(m_FireAbilityAsset) > 0);
            m_TimeAbilityPanel.SetActive(Upgrades.GetUpgradeLevel(m_TimeAbilityAsset) > 0);

            TDPlayer.Instance.GoldUpdateSubscription(GoldStatusCheck);
            m_FireCostText.text = m_FireAbility.Cost.ToString();
            m_FireButton.GetComponent<Image>().sprite = m_FireAbilityAsset.sprite;

            TDPlayer.Instance.ManaUpdateSubscription(ManaStatusCheck);
            m_TimeCostText.text = m_TimeAbility.Cost.ToString();
            m_TimeButton.GetComponent<Image>().sprite = m_TimeAbilityAsset.sprite;
        }

        private void OnDestroy()
        {
            TDPlayer.Instance.GoldUpdateUnsubscribe(GoldStatusCheck);
            TDPlayer.Instance.ManaUpdateUnsubscribe(ManaStatusCheck);
        }

        /// <summary>
        /// Проверка, хватает ли золота
        /// </summary>
        /// <param name="gold">Золото</param>
        private void GoldStatusCheck(int gold)
        {
            if (gold >= m_FireAbility.Cost != m_FireButton.interactable)
            {
                m_FireButton.interactable = !m_FireButton.interactable;
                m_FireCostText.color = m_FireButton.interactable ? Color.white : Color.red;
            }
        }

        /// <summary>
        /// Проверка, хватает ли маны
        /// </summary>
        /// <param name="mana">Мана</param>
        private void ManaStatusCheck(int mana)
        {
            if (mana >= m_TimeAbility.Cost)
            {
                if (m_TimeCostText.color != Color.white)
                {
                    m_TimeCostText.color = Color.white;
                }
            }
            else
            {
                if (m_TimeCostText.color != Color.red)
                {
                    m_TimeCostText.color = Color.red;
                }
            }

            if (mana >= m_TimeAbility.Cost != m_TimeButton.interactable && m_TimeAbility.CooldownIsActive == false)
            {
                m_TimeButton.interactable = !m_TimeButton.interactable;
            }
        }
    }
}