using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ScriptableObject со свойствами врагов
    /// </summary>
    [CreateAssetMenu]
    public class EnemyAsset : ScriptableObject
    {
        [Header("Внешний вид")]
        /// <summary>
        /// Цвет
        /// </summary>
        public Color color = Color.white;

        /// <summary>
        /// Размер спрайта
        /// </summary>
        public Vector2 spriteScale = new Vector2(4, 4);

        /// <summary>
        /// Анимация
        /// </summary>
        public RuntimeAnimatorController animations;
        
        [Header("Игровые параметры")]

        /// <summary>
        /// Скорость движения
        /// </summary>
        public float moveSpeed = 1.0f;

        /// <summary>
        /// Здоровье
        /// </summary>
        public int hp = 10;

        /// <summary>
        /// Урон
        /// </summary>
        public int damage = 1;

        /// <summary>
        /// Очки
        /// </summary>
        public int score = 100;

        /// <summary>
        /// Радиус коллайдера
        /// </summary>
        public float radius = 0.4f;

        /// <summary>
        /// Смещение коллайдера
        /// </summary>
        public float colliderOffsetY = -0.1f;

        /// <summary>
        /// Золото
        /// </summary>
        public int gold = 10;
    }
}