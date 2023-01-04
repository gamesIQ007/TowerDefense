using System;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// Класс игрока в TowerDefense
    /// </summary>
    public class TDPlayer : Player
    {
        /// <summary>
        /// Инстанс для ТДигрока
        /// </summary>
        public static new TDPlayer Instance
        {
            get { return Player.Instance as TDPlayer; }
        }

        /// <summary>
        /// Системное событие на изменение золота
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
        /// Системное событие на изменение жизни
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
        /// Золото
        /// </summary>
        [SerializeField] private int m_Gold = 0;
        public int Gold => m_Gold;

        /// <summary>
        /// Изменить количество золота
        /// </summary>
        /// <param name="gold">Золото</param>
        public void ChangeGold(int gold)
        {
            m_Gold += gold;
            OnGoldUpdate(m_Gold);
        }

        /// <summary>
        /// Апгрейд увеличения жизней
        /// </summary>
        [SerializeField] private UpgradeAsset m_HealthUpgrade;

        private new void Awake()
        {
            base.Awake();
            int level = Upgrades.GetUpgradeLevel(m_HealthUpgrade);
            TakeDamage(-level);
        }

        /// <summary>
        /// Изменить количество здоровья
        /// </summary>
        /// <param name="gold">Здоровье</param>
        public void ReduceLife(int damage)
        {
            TakeDamage(damage);
            OnLifeUpdate(m_NumLives);
        }

        /// <summary>
        /// Префаб башни
        /// </summary>
        [SerializeField] private Tower m_TowerPrefab;

        /// <summary>
        /// Попытка построить башню
        /// </summary>
        /// <param name="towerAsset">Ассет</param>
        /// <param name="buildSite">Позиция</param>
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