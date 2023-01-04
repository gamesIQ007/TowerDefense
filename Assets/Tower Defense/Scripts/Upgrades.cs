using System;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Купленные апгрейды
    /// </summary>
    public class Upgrades : SingletonBase<Upgrades>
    {
        /// <summary>
        /// Файл сохранения
        /// </summary>
        public const string FILENAME = "upgrades.dat";

        /// <summary>
        /// Имеющиеся у игрока апгрейды
        /// </summary>
        [Serializable]
        private class UpgrageSave
        {
            /// <summary>
            /// Ассет апгрейда
            /// </summary>
            public UpgradeAsset asset;

            /// <summary>
            /// Уровень апгрейда
            /// </summary>
            public int level = 0;
        }

        /// <summary>
        /// Массив купленных апгрейдов
        /// </summary>
        [SerializeField] private UpgrageSave[] m_Save;

        private new void Awake()
        {
            base.Awake();
            Saver<UpgrageSave[]>.TryLoad(FILENAME, ref m_Save);
        }

        /// <summary>
        /// Покупка апгрейда
        /// </summary>
        public static void BuyUpgrade(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.m_Save)
            {
                if (upgrade.asset == asset)
                {
                    upgrade.level++;
                    Saver<UpgrageSave[]>.Save(FILENAME, Instance.m_Save);
                }
            }
        }

        /// <summary>
        /// Выдаём уровень апгрейда по ассету
        /// </summary>
        /// <param name="asset">Ассет</param>
        /// <returns>Уровень апгрейда</returns>
        public static int GetUpgradeLevel(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.m_Save)
            {
                if (upgrade.asset == asset)
                {
                    return upgrade.level;
                }
            }
            return 0;
        }

        public static int GetTotalCost()
        {
            int result = 0;

            foreach (var upgrade in Instance.m_Save)
            {
                for (int i = 0; i < upgrade.level; i++)
                {
                    result += upgrade.asset.costByLevel[i];
                }
            }

            return result;
        }
    }
}