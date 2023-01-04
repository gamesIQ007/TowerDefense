using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    /// <summary>
    /// ScriptableObject ��������
    /// </summary>
    public class UpgradeAsset : ScriptableObject
    {
        /// <summary>
        /// ������
        /// </summary>
        public Sprite sprite;

        /// <summary>
        /// ������ ���
        /// </summary>
        public int[] costByLevel = { 3 };
    }
}