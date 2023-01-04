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
        private static event Action<int> OnGoldUpdate;
        public static void GoldUpdateSubscription(Action<int> action)
        {
            OnGoldUpdate += action;
            action(Instance.m_Gold);
        }
        public static void GoldUpdateUnsubscribe(Action<int> action)
        {
            OnGoldUpdate -= action;
            action(Instance.m_Gold);
        }

        /// <summary>
        /// ��������� ������� �� ��������� �����
        /// </summary>
        public static event Action<int> OnLifeUpdate;
        public static void LifeUpdateSubscription(Action<int> action)
        {
            OnLifeUpdate += action;
            action(Instance.m_NumLives);
        }
        public static void LifeUpdateUnsubscribe(Action<int> action)
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
        /// �������� ���������� ������
        /// </summary>
        /// <param name="gold">������</param>
        public void ChangeGold(int gold)
        {
            m_Gold += gold;
            OnGoldUpdate(m_Gold);
        }

        /// <summary>
        /// ������� ���������� ������
        /// </summary>
        [SerializeField] private UpgradeAsset m_HealthUpgrade;

        private new void Awake()
        {
            base.Awake();
            int level = Upgrades.GetUpgradeLevel(m_HealthUpgrade);
            TakeDamage(-level);
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
                tower.GetComponentInChildren<SpriteRenderer>().sprite = towerAsset.sprite;
                tower.GetComponentInChildren<Turret>().AssignLoadout(towerAsset.turret);
                Destroy(buildSite.gameObject);
            }
        }
    }
}