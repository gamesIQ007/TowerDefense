using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// Класс игрока
    /// </summary>
    public class Player : SingletonBase<Player>
    {
        /// <summary>
        /// Количество жизней
        /// </summary>
        [SerializeField] private int m_NumLives;

        /// <summary>
        /// Корабль игрока
        /// </summary>
        [SerializeField] private SpaceShip m_Ship;
        public SpaceShip ActiveShip => m_Ship;

        /// <summary>
        /// Префаб корабля игрока
        /// </summary>
        [SerializeField] private GameObject m_PlayerShipPrefab;

        /// <summary>
        /// Контроллер камеры
        /// </summary>
        //[SerializeField] private CameraController m_CameraController;

        /// <summary>
        /// Контроллер управления
        /// </summary>
        //[SerializeField] private MovementController m_MovementController;

        /// <summary>
        /// Ивент на изменение количества очков
        /// </summary>
        [HideInInspector] public UnityEvent m_EventScoreChanged;

        #region Unity Events

        protected override void Awake()
        {
            base.Awake();

            if (m_Ship != null)
            {
                Destroy(m_Ship.gameObject);
            }
        }

        private void Start()
        {
            if (m_Ship)
            {
                m_Ship.EventOnDeath.AddListener(OnShipDeath);
            }

            Respawn();
        }

        #endregion

        /// <summary>
        /// Получить урон
        /// </summary>
        /// <param name="damage">Урон</param>
        public void TakeDamage(int damage)
        {
            m_NumLives -= damage;

            if (m_NumLives <= 0)
            {
                LevelSequenceController.Instance.FinishCurrentLevel(false);
            }
        }

        /// <summary>
        /// Действие при смерти корабля
        /// </summary>
        private void OnShipDeath()
        {
            m_NumLives--;

            if (m_NumLives > 0)
            {
                Respawn();
            }
            else
            {
                LevelSequenceController.Instance.FinishCurrentLevel(false);
            }
        }

        /// <summary>
        /// Респаун
        /// </summary>
        private void Respawn()
        {
            if (LevelSequenceController.PlayerShip != null)
            {
                var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);

                foreach (var projectiles in FindObjectsOfType<Projectile>())
                {
                    if (projectiles.Parent != m_Ship) continue;

                    projectiles.SetParentShooter(newPlayerShip);
                }

                m_Ship = newPlayerShip.GetComponent<SpaceShip>();
                //m_CameraController.SetTarget(m_Ship.transform);
                //m_MovementController.SetTargetShip(m_Ship);
                m_Ship.EventOnDeath.AddListener(OnShipDeath);
            }
        }

        #region Score

        /// <summary>
        /// Очки
        /// </summary>
        private int m_Score;
        public int Score => m_Score;

        /// <summary>
        /// Количество убийств
        /// </summary>
        private int m_NumKills;
        public int NumKills => m_NumKills;

        /// <summary>
        /// Увеличение количество очков
        /// </summary>
        /// <param name="score">Количество добавляемых очков</param>
        public void AddScore(int score)
        {
            m_Score += score;

            m_EventScoreChanged?.Invoke();
        }

        /// <summary>
        /// Увеличение счётчика убийств
        /// </summary>
        public void AddKill()
        {
            m_NumKills++;
        }

        #endregion
    }
}