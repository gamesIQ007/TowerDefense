using System;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// ����� ������ � TowerDefense
    /// </summary>
    public class TDPlayer : Player
    {
        /// <summary>
        /// ������� ��� ��������
        /// </summary>
        public static new TDPlayer Instance
        {
            get { return Player.Instance as TDPlayer; }
        }

        /// <summary>
        /// ��������� ������� �� ��������� ������
        /// </summary>
        private event Action<int> OnGoldUpdate;
        public void GoldUpdateSubscription(Action<int> action)
        {
            OnGoldUpdate += action;
            action(Instance.m_Gold);
        }
        public void GoldUpdateUnsubscribe(Action<int> action)
        {
            OnGoldUpdate -= action;
            action(Instance.m_Gold);
        }

        /// <summary>
        /// ��������� ������� �� ��������� �����
        /// </summary>
        public event Action<int> OnLifeUpdate;
        public void LifeUpdateSubscription(Action<int> action)
        {
            OnLifeUpdate += action;
            action(Instance.m_NumLives);
        }
        public void LifeUpdateUnsubscribe(Action<int> action)
        {
            OnLifeUpdate -= action;
            action(Instance.m_NumLives);
        }

        /// <summary>
        /// ������
        /// </summary>
        [SerializeField] private int m_Gold = 0;
        public int Gold => m_Gold;

        /// <summary>
        /// ������������ ���������� ����
        /// </summary>
        [SerializeField] private float m_MaxMana;

        /// <summary>
        /// ����
        /// </summary>
        private float m_Mana;
        public float Mana => m_Mana;

        /// <summary>
        /// �������� �������������� ����
        /// </summary>
        [SerializeField] private float m_ManaRestoreSpeed = 0;

        /// <summary>
        /// ��������� ������� �� ��������� ����
        /// </summary>
        private event Action<int> OnManaUpdate;
        public void ManaUpdateSubscription(Action<int> action)
        {
            OnManaUpdate += action;
            action((int)Instance.Mana);
        }
        public void ManaUpdateUnsubscribe(Action<int> action)
        {
            OnManaUpdate -= action;
            action((int)Instance.Mana);
        }

        /// <summary>
        /// �������� ���������� ������
        /// </summary>
        /// <param name="gold">������</param>
        public void ChangeGold(int gold)
        {
            m_Gold += gold;

            int goldUpgrade = (int)(Upgrades.GetUpgradeLevel(m_GoldUpgrade) * Upgrades.GetUpgradeModifier(m_GoldUpgrade));
            if (gold > 0)
            {
                m_Gold += goldUpgrade;
            }

            OnGoldUpdate(m_Gold);
        }

        /// <summary>
        /// ������� ���������� ������
        /// </summary>
        [SerializeField] private UpgradeAsset m_HealthUpgrade;

        /// <summary>
        /// ������� ���������� ������
        /// </summary>
        [SerializeField] private UpgradeAsset m_GoldUpgrade;

        private new void Awake()
        {
            base.Awake();
            int hpUpgrade = (int) (Upgrades.GetUpgradeLevel(m_HealthUpgrade) * Upgrades.GetUpgradeModifier(m_HealthUpgrade));
            TakeDamage(-hpUpgrade);
            m_Mana = m_MaxMana;
        }

        private void Update()
        {
            // ��� �� �����, ���� �������� ����-����, ���� ���� �� ����� ���������� ������ ����
            if (m_Mana < m_MaxMana)
            {
                m_Mana += m_ManaRestoreSpeed * Time.deltaTime;
                if (m_Mana > m_MaxMana)
                {
                    m_Mana = m_MaxMana;
                }
                OnManaUpdate((int)m_Mana);
            }
        }

        /// <summary>
        /// �������� ���������� ����
        /// </summary>
        /// <param name="mana">����</param>
        public void ReduceMana(int mana)
        {
            m_Mana -= mana;
            OnManaUpdate((int)m_Mana);
        }

        /// <summary>
        /// �������� ���������� ��������
        /// </summary>
        /// <param name="gold">��������</param>
        public void ReduceLife(int damage)
        {
            TakeDamage(damage);
            OnLifeUpdate(m_NumLives);
        }

        /// <summary>
        /// ������ �����
        /// </summary>
        [SerializeField] private Tower m_TowerPrefab;

        /// <summary>
        /// ������� ��������� �����
        /// </summary>
        /// <param name="towerAsset">�����</param>
        /// <param name="buildSite">�������</param>
        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            if (m_Gold >= towerAsset.goldCost)
            {
                ChangeGold(-towerAsset.goldCost);
                Tower tower = Instantiate(m_TowerPrefab, buildSite.position, Quaternion.identity);
                tower.Use(towerAsset);
                Destroy(buildSite.gameObject);
            }
        }
    }
}