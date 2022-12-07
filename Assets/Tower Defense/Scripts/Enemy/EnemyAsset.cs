using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ScriptableObject �� ���������� ������
    /// </summary>
    [CreateAssetMenu]
    public class EnemyAsset : ScriptableObject
    {
        [Header("������� ���")]
        /// <summary>
        /// ����
        /// </summary>
        public Color color = Color.white;

        /// <summary>
        /// ������ �������
        /// </summary>
        public Vector2 spriteScale = new Vector2(4, 4);

        /// <summary>
        /// ��������
        /// </summary>
        public RuntimeAnimatorController animations;
        
        [Header("������� ���������")]

        /// <summary>
        /// �������� ��������
        /// </summary>
        public float moveSpeed = 1.0f;

        /// <summary>
        /// ��������
        /// </summary>
        public int hp = 10;

        /// <summary>
        /// ����
        /// </summary>
        public int damage = 1;

        /// <summary>
        /// ����
        /// </summary>
        public int score = 100;

        /// <summary>
        /// ������ ����������
        /// </summary>
        public float radius = 0.4f;

        /// <summary>
        /// �������� ����������
        /// </summary>
        public float colliderOffsetY = -0.1f;

        /// <summary>
        /// ������
        /// </summary>
        public int gold = 10;
    }
}