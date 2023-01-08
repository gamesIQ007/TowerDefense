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
    /// ����. �������� ��������� �� ScriptableObject
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        /// <summary>
        /// �������� ����� �����
        /// </summary>
        public enum ArmorType
        {
            /// <summary>
            /// ������� ��� �����
            /// </summary>
            Base = 0,
            /// <summary>
            /// ���������� ��� �����
            /// </summary>
            Magic = 1,
            /// <summary>
            /// ����� �� �������
            /// </summary>
            Explosion = 2
        }

        /// <summary>
        /// ������ ������� �� ����������� ����� � ����������� �� ����� ������� � �����
        /// </summary>
        private static Func<int, Projectile.DamageType, int, int>[] ArmorDamageFunctions =
        {
            // ������ ����� ��� ������� �����
            (int power, Projectile.DamageType type, int armor) =>
            {
                switch (type)
                {
                    case Projectile.DamageType.Magic: return power;
                    default: return Mathf.Max(power - armor, 1);
                }
            },
            // ������ ����� ��� ���������� �����
            (int power, Projectile.DamageType type, int armor) =>
            {
                if (type == Projectile.DamageType.Base)
                {
                    armor = armor / 2;
                }
                return Mathf.Max(power - armor, 1);
            },
            // ������ ����� ��� ����� �� �������
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
        /// ����
        /// </summary>
        [SerializeField] private int m_Damage;

        /// <summary>
        /// ��� �����
        /// </summary>
        [SerializeField] private ArmorType m_ArmorType;

        /// <summary>
        /// �����
        /// </summary>
        [SerializeField] private int m_Armor;

        /// <summary>
        /// ������
        /// </summary>
        [SerializeField] private int m_Gold;

        private Destructible m_Destructible;

        /// <summary>
        /// ���������
        /// </summary>
        private EnemyState m_State;

        /// <summary>
        /// ������� ��� ����� ������������� �����
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
        /// ��������� ��������� �� ScriptableObject � �����
        /// </summary>
        /// <param name="asset">���������</param>
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
        /// ��������� ����� ������
        /// </summary>
        public void DamagePlayer()
        {
            TDPlayer.Instance.ReduceLife(m_Damage);
        }

        /// <summary>
        /// ���������� ������ ������
        /// </summary>
        public void GetPlayerGold()
        {
            TDPlayer.Instance.ChangeGold(m_Gold);
        }

        /// <summary>
        /// ��������� �����������
        /// </summary>
        /// <param name="damage">����</param>
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