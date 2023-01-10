using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// ����� �������� �����
    /// </summary>
    [CreateAssetMenu]
    public class TowerAsset : ScriptableObject
    {
        /// <summary>
        /// ���������
        /// </summary>
        public int goldCost = 100;

        /// <summary>
        /// ������ ����������
        /// </summary>
        public Sprite GUISprite;

        /// <summary>
        /// ������
        /// </summary>
        public Sprite sprite;

        /// <summary>
        /// ��������� ������
        /// </summary>
        public TurretProperties turret;

        /// <summary>
        /// ��������� �������
        /// </summary>
        [SerializeField] private UpgradeAsset requiredUpgrade;

        /// <summary>
        /// ��������� ������� ��������
        /// </summary>
        [SerializeField] private int requiredUpgradeLevel;

        /// <summary>
        /// ����������� �����
        /// </summary>
        /// <returns>�������� �� �����</returns>
        public bool IsAvailable()
        {
            if (requiredUpgrade)
            {
                return requiredUpgradeLevel <= Upgrades.GetUpgradeLevel(requiredUpgrade);
            }
            else return true;
        }

        /// <summary>
        /// � ����� ����� ����� ���������� ��� �����
        /// </summary>
        public TowerAsset[] m_UpgradesTo;
    }
}
