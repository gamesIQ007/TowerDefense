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
        private class UpgradeSave
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
        [SerializeField] private UpgradeSave[] m_Save;

        private new void Awake()
        {
            base.Awake();
            Saver<UpgradeSave[]>.TryLoad(FILENAME, ref m_Save);
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
                    Saver<UpgradeSave[]>.Save(FILENAME, Instance.m_Save);
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

        /// <summary>
        /// Выдаём модификатор апгрейда по ассету
        /// </summary>
        /// <param name="asset">Ассет</param>
        /// <returns>Модификатор апгрейда</returns>
        public static float GetUpgradeModifier(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.m_Save)
            {
                if (upgrade.asset == asset)
                {
                    return upgrade.asset.modifier;
                }
            }
            return 1;
        }

        /// <summary>
        /// Получаем полную стоимость апгрейдов
        /// </summary>
        /// <returns>Полная стоимость</returns>
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