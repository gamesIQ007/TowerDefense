using System;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ��������� ��������
    /// </summary>
    public class Upgrades : SingletonBase<Upgrades>
    {
        /// <summary>
        /// ���� ����������
        /// </summary>
        public const string FILENAME = "upgrades.dat";

        /// <summary>
        /// ��������� � ������ ��������
        /// </summary>
        [Serializable]
        private class UpgrageSave
        {
            /// <summary>
            /// ����� ��������
            /// </summary>
            public UpgradeAsset asset;

            /// <summary>
            /// ������� ��������
            /// </summary>
            public int level = 0;
        }

        /// <summary>
        /// ������ ��������� ���������
        /// </summary>
        [SerializeField] private UpgrageSave[] m_Save;

        private new void Awake()
        {
            base.Awake();
            Saver<UpgrageSave[]>.TryLoad(FILENAME, ref m_Save);
        }

        /// <summary>
        /// ������� ��������
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
        /// ����� ������� �������� �� ������
        /// </summary>
        /// <param name="asset">�����</param>
        /// <returns>������� ��������</returns>
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