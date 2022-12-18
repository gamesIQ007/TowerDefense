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
    }
}
