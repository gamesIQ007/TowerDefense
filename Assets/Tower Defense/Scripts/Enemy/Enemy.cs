using System;
using UnityEngine;
using SpaceShooter;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefense
{
    [RequireComponent(typeof(TDPatrolController))]
    [RequireComponent(typeof(Destructible))]

    /// <summary>
    /// Враг. Получает настройки из ScriptableObject
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        /// <summary>
        /// Перечень типов брони
        /// </summary>
        public enum ArmorType
        {
            /// <summary>
            /// Базовый тип брони
            /// </summary>
            Base = 0,
            /// <summary>
            /// Магический тип брони
            /// </summary>
            Magic = 1,
            /// <summary>
            /// Броня от взрывов
            /// </summary>
            Explosion = 2
        }

        /// <summary>
        /// Массив функций на определение урона в зависимости от типов снаряда и брони
        /// </summary>
        private static Func<int, Projectile.DamageType, int, int>[] ArmorDamageFunctions =
        {
            // расчёт урона для базовой брони
            (int power, Projectile.DamageType type, int armor) =>
            {
                switch (type)
                {
                    case Projectile.DamageType.Magic: return power;
                    default: return Mathf.Max(power - armor, 1);
                }
            },
            // расчёт урона для магической брони
            (int power, Projectile.DamageType type, int armor) =>
            {
                if (type == Projectile.DamageType.Base)
                {
                    armor = armor / 2;
                }
                return Mathf.Max(power - armor, 1);
            },
            // расчёт урона для брони от взрывов
            (int power, Projectile.DamageType type, int armor) =>
            {
                switch (type)
                {
                    case Projectile.DamageType.Explosion: return Mathf.Max(power - armor, 1);
                    default: return power;
                }
            }
        };

        /// <summary>
        /// Урон
        /// </summary>
        [SerializeField] private int m_Damage;

        /// <summary>
        /// Тип брони
        /// </summary>
        [SerializeField] private ArmorType m_ArmorType;

        /// <summary>
        /// Броня
        /// </summary>
        [SerializeField] private int m_Armor;

        /// <summary>
        /// Золото
        /// </summary>
        [SerializeField] private int m_Gold;

        private Destructible m_Destructible;

        /// <summary>
        /// Состояние
        /// </summary>
        private EnemyState m_State;

        /// <summary>
        /// Событие при конце существования врага
        /// </summary>
        public event Action OnEnd;

        private void Awake()
        {
            m_Destructible = GetComponent<Destructible>();
        }

        private void OnDestroy()
        {
            OnEnd?.Invoke();
        }

        /// <summary>
        /// Применить настройки из ScriptableObject к врагу
        /// </summary>
        /// <param name="asset">Настройки</param>
        public void Use(EnemyAsset asset)
        {
            SpriteRenderer sr = transform.GetComponentInChildren<SpriteRenderer>();
            sr.color = asset.color;
            sr.transform.localScale = new Vector3(asset.spriteScale.x, asset.spriteScale.y, 1);

            sr.GetComponentInChildren<Animator>().runtimeAnimatorController = asset.animations;

            GetComponent<SpaceShip>().Use(asset);

            CircleCollider2D col = transform.GetComponentInChildren<CircleCollider2D>();
            col.radius = asset.radius;
            col.offset = new Vector2 (0, asset.colliderOffsetY);

            m_Damage = asset.damage;

            m_ArmorType = asset.armorType;
            m_Armor = asset.armor;

            m_Gold = asset.gold;
        }

        /// <summary>
        /// Нанесение урона игроку
        /// </summary>
        public void DamagePlayer()
        {
            TDPlayer.Instance.ReduceLife(m_Damage);
        }

        /// <summary>
        /// Добавление золота игроку
        /// </summary>
        public void GetPlayerGold()
        {
            TDPlayer.Instance.ChangeGold(m_Gold);
        }

        /// <summary>
        /// Получение повреждений
        /// </summary>
        /// <param name="damage">Урон</param>
        public void TakeDamage(int damage, Projectile.DamageType damageType)
        {
            m_Destructible.ApplyDamage(ArmorDamageFunctions[(int)m_ArmorType](damage, damageType, m_Armor));
        }
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(Enemy))]
    public class EnemyInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EnemyAsset asset = EditorGUILayout.ObjectField(null, typeof(EnemyAsset), false) as EnemyAsset;

            if (asset != null)
            {
                (target as Enemy).Use(asset);
            }
        }
    }

#endif
}