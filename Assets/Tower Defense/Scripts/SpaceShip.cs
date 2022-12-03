using UnityEngine;
using TowerDefense;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(AudioSource))]

    /// <summary>
    /// Класс космического корабля
    /// </summary>
    public class SpaceShip : Destructible
    {
        [Header("Space ship")]
        /// <summary>
        /// Масса, для автоматической установки в ригиде
        /// </summary>
        [SerializeField] private float m_Mass;

        /// <summary>
        /// Толкающая вперёд сила
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// Модификатор толкающей силы
        /// </summary>
        private float m_ThrustModifier;

        /// <summary>
        /// Время действия модификатора толкающей силы
        /// </summary>
        private float m_ThrustModifierTimer;

        /// <summary>
        /// Вращающая сила
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// Максимальная линейная скорость
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;
        public float MaxLinearVelocity => m_MaxLinearVelocity;

        /// <summary>
        /// Максимальная вращательная скорость в градусах/сек.
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;

        /// <summary>
        /// Сохранённая ссылка на ригид
        /// </summary>
        [SerializeField] private Rigidbody2D m_Rigid;

        /*
        /// <summary>
        /// Турели
        /// </summary>
        [SerializeField] private Turret[] m_Turrets;

        /// <summary>
        /// Максимальное количество энергии
        /// </summary>
        [SerializeField] private int m_MaxEnergy;

        /// <summary>
        /// Максимальное количество боеприпасов
        /// </summary>
        [SerializeField] private int m_MaxAmmo;

        /// <summary>
        /// Регенерация энергии в секунду
        /// </summary>
        [SerializeField] private int m_EnergyRegenPerSecond;

        /// <summary>
        /// Текущее количество энергии
        /// </summary>
        private float m_PrimaryEnergy;

        /// <summary>
        /// Текущее количество патронов
        /// </summary>
        private int m_SecondaryAmmo;
        */

        /// <summary>
        /// Изображение-превью корабля
        /// </summary>
        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;

        /// <summary>
        /// Звуки корабля (стрельбы)
        /// </summary>
        [HideInInspector] public new AudioSource audio;

        [Header("DeathEffect")]
        /// <summary>
        /// Префаб эффекта посмертного префаба
        /// </summary>
        [SerializeField] private GameObject m_EffectPrefab;

        #region Unity Events

        protected override void Start()
        {
            base.Start();
            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;
            m_Rigid.inertia = 1; // иннерциальные силы, чтобы было проще балансировать соотношение сил и легче управлять

            //InitArmament();

            m_ThrustModifier = 1;

            audio = GetComponent<AudioSource>();
        }

        protected override void Update()
        {
            base.Update();

            if (m_ThrustModifierTimer <= 0) return;

            m_ThrustModifierTimer -= Time.deltaTime;

            if (m_ThrustModifierTimer <= 0)
            {
                DisableTemporarySpeedUp();
            }
        }

        private void FixedUpdate()
        {
            UpdateRigitBody();

            //UpdateEnergyRegen();
        }

        #endregion

        #region Public API

        /// <summary>
        /// Управление линейной тягой. От -1.0 до +1.0
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Управление вращательной тягой. От -1.0 до +1.0
        /// </summary>
        public float TorqueControl { get; set; }

        /// <summary>
        /// TODO: заменить временный метод-заглушку
        /// Используется ИИ
        /// </summary>
        /// <param name="mode">Стрельба из турелей с режимом mode</param>
        public void Fire(TurretMode mode)
        {
            return;
        }

        /*
        /// <summary>
        /// Добавить энергию
        /// </summary>
        /// <param name="energy">Количество добавляемой энергии</param>
        public void AddEnergy(int energy)
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + energy, 0, m_MaxEnergy);
        }

        /// <summary>
        /// Добавить патронов
        /// </summary>
        /// <param name="energy">Количество добавляемых патронов</param>
        public void AddAmmo(int ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
        }
        */

        /// <summary>
        /// TODO: заменить временный метод-заглушку
        /// Используется турелью
        /// </summary>
        /// <param name="count">Количество отнимаемой энергии</param>
        /// <returns>Удачное ли изъяние энергии</returns>
        public bool DrawEnergy(int count)
        {
            return true;
        }

        /// <summary>
        /// TODO: заменить временный метод-заглушку
        /// Используется турелью
        /// </summary>
        /// <param name="count">Количество отнимаемых патронов</param>
        /// <returns>Удачное ли изъяние патронов</returns>
        public bool DrawAmmo(int count)
        {
            return true;
        }

        /*
        /// <summary>
        /// Назначить свойства турели
        /// </summary>
        /// <param name="prop">Назначаемое свойство турели</param>
        public void AssignWeapon(TurretProperties props)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadout(props);
            }
        }
        */

        /// <summary>
        /// Включение временного модификатора толкающей силы
        /// </summary>
        /// <param name="value">Величина модификатора толкающей силы</param>
        /// <param name="time">Время действия модификатора толкающей силы</param>
        public void EnableTemporarySpeedUp(float value, float time)
        {
            m_ThrustModifier = value;
            m_ThrustModifierTimer += time;
        }

        /// <summary>
        /// Применить настройки из ScriptableObject к ассету
        /// </summary>
        /// <param name="asset">Настройки</param>
        public new void Use(EnemyAsset asset)
        {
            m_MaxLinearVelocity = asset.moveSpeed;
            base.Use(asset);
        }

        #endregion

        /// <summary>
        /// Метод добавления сил кораблю для движения
        /// </summary>
        private void UpdateRigitBody()
        {
            m_Rigid.AddForce(ThrustControl * m_Thrust * m_ThrustModifier * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);
            m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust * m_ThrustModifier / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);
            m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        /*
        /// <summary>
        /// Инициализация вооружения
        /// </summary>
        private void InitArmament()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
        }

        /// <summary>
        /// Регенерация энергии
        /// </summary>
        private void UpdateEnergyRegen()
        {
            m_PrimaryEnergy += (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime;
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy);
        }
        */

        /// <summary>
        /// Отключение временного модификатора толкающей силы
        /// </summary>
        private void DisableTemporarySpeedUp()
        {
            m_ThrustModifier = 1;
            m_ThrustModifierTimer = 0;
        }

        protected override void OnDeath()
        {
            base.OnDeath();

            //GameObject effect = Instantiate(m_EffectPrefab, transform.position, Quaternion.identity);
        }
    }
}
