using UnityEngine;
using TowerDefense;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(AudioSource))]

    /// <summary>
    /// ����� ������������ �������
    /// </summary>
    public class SpaceShip : Destructible
    {
        [Header("Space ship")]
        /// <summary>
        /// �����, ��� �������������� ��������� � ������
        /// </summary>
        [SerializeField] private float m_Mass;

        /// <summary>
        /// ��������� ����� ����
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// ����������� ��������� ����
        /// </summary>
        private float m_ThrustModifier;

        /// <summary>
        /// ����� �������� ������������ ��������� ����
        /// </summary>
        private float m_ThrustModifierTimer;

        /// <summary>
        /// ��������� ����
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// ������������ �������� ��������
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;
        public float MaxLinearVelocity => m_MaxLinearVelocity;

        /// <summary>
        /// ������������ ������������ �������� � ��������/���.
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;

        /// <summary>
        /// ���������� ������ �� �����
        /// </summary>
        [SerializeField] private Rigidbody2D m_Rigid;

        /*
        /// <summary>
        /// ������
        /// </summary>
        [SerializeField] private Turret[] m_Turrets;

        /// <summary>
        /// ������������ ���������� �������
        /// </summary>
        [SerializeField] private int m_MaxEnergy;

        /// <summary>
        /// ������������ ���������� �����������
        /// </summary>
        [SerializeField] private int m_MaxAmmo;

        /// <summary>
        /// ����������� ������� � �������
        /// </summary>
        [SerializeField] private int m_EnergyRegenPerSecond;

        /// <summary>
        /// ������� ���������� �������
        /// </summary>
        private float m_PrimaryEnergy;

        /// <summary>
        /// ������� ���������� ��������
        /// </summary>
        private int m_SecondaryAmmo;
        */

        /// <summary>
        /// �����������-������ �������
        /// </summary>
        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;

        /// <summary>
        /// �����������
        /// </summary>
        private SpriteRenderer sprite;

        /// <summary>
        /// ����� ������� (��������)
        /// </summary>
        [HideInInspector] public new AudioSource audio;

        [Header("DeathEffect")]
        /// <summary>
        /// ������ ������� ����������� �������
        /// </summary>
        [SerializeField] private GameObject m_EffectPrefab;

        #region Unity Events

        protected override void Start()
        {
            base.Start();
            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;
            m_Rigid.inertia = 1; // ������������� ����, ����� ���� ����� ������������� ����������� ��� � ����� ���������

            //InitArmament();

            m_ThrustModifier = 1;

            audio = GetComponent<AudioSource>();

            sprite = GetComponentInChildren<SpriteRenderer>();
            m_NormalStateColor = sprite.color;

            InitTimers();
        }

        protected override void Update()
        {
            base.Update();

            UpdateTimers();

            if (stateTimer.IsFinished && state == EnemyState.Freezed)
            {
                RemoveState();
            }

            if (m_ThrustModifierTimer <= 0) return;

            m_ThrustModifierTimer -= Time.deltaTime;

            if (m_ThrustModifierTimer <= 0)
            {
                DisableTemporarySpeedUp();
            }
        }

        private void FixedUpdate()
        {
            if (state != EnemyState.Freezed)
            {
                UpdateRigitBody();
            }

            //UpdateEnergyRegen();
        }

        #endregion

        #region Public API

        /// <summary>
        /// ���������� �������� �����. �� -1.0 �� +1.0
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// ���������� ������������ �����. �� -1.0 �� +1.0
        /// </summary>
        public float TorqueControl { get; set; }

        /// <summary>
        /// TODO: �������� ��������� �����-��������
        /// ������������ ��
        /// </summary>
        /// <param name="mode">�������� �� ������� � ������� mode</param>
        public void Fire(TurretMode mode)
        {
            return;
        }

        /*
        /// <summary>
        /// �������� �������
        /// </summary>
        /// <param name="energy">���������� ����������� �������</param>
        public void AddEnergy(int energy)
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + energy, 0, m_MaxEnergy);
        }

        /// <summary>
        /// �������� ��������
        /// </summary>
        /// <param name="energy">���������� ����������� ��������</param>
        public void AddAmmo(int ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
        }
        */

        /// <summary>
        /// TODO: �������� ��������� �����-��������
        /// ������������ �������
        /// </summary>
        /// <param name="count">���������� ���������� �������</param>
        /// <returns>������� �� ������� �������</returns>
        public bool DrawEnergy(int count)
        {
            return true;
        }

        /// <summary>
        /// TODO: �������� ��������� �����-��������
        /// ������������ �������
        /// </summary>
        /// <param name="count">���������� ���������� ��������</param>
        /// <returns>������� �� ������� ��������</returns>
        public bool DrawAmmo(int count)
        {
            return true;
        }

        /*
        /// <summary>
        /// ��������� �������� ������
        /// </summary>
        /// <param name="prop">����������� �������� ������</param>
        public void AssignWeapon(TurretProperties props)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadout(props);
            }
        }
        */

        /// <summary>
        /// ��������� ���������� ������������ ��������� ����
        /// </summary>
        /// <param name="value">�������� ������������ ��������� ����</param>
        /// <param name="time">����� �������� ������������ ��������� ����</param>
        public void EnableTemporarySpeedUp(float value, float time)
        {
            m_ThrustModifier = value;
            m_ThrustModifierTimer += time;
            sprite.color = m_FreezedStateColor;
        }

        /// <summary>
        /// ��������� ��������� �� ScriptableObject � ������
        /// </summary>
        /// <param name="asset">���������</param>
        public new void Use(EnemyAsset asset)
        {
            m_MaxLinearVelocity = asset.moveSpeed;
            base.Use(asset);
        }

        #endregion

        /// <summary>
        /// ����� ���������� ��� ������� ��� ��������
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
        /// ������������� ����������
        /// </summary>
        private void InitArmament()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
        }

        /// <summary>
        /// ����������� �������
        /// </summary>
        private void UpdateEnergyRegen()
        {
            m_PrimaryEnergy += (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime;
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy);
        }
        */

        /// <summary>
        /// ���������� ���������� ������������ ��������� ����
        /// </summary>
        private void DisableTemporarySpeedUp()
        {
            m_ThrustModifier = 1;
            m_ThrustModifierTimer = 0;
            sprite.color = m_NormalStateColor;
        }

        protected override void OnDeath()
        {
            base.OnDeath();

            //GameObject effect = Instantiate(m_EffectPrefab, transform.position, Quaternion.identity);
        }

        #region State

        /// <summary>
        /// ������
        /// </summary>
        private EnemyState state;

        /// <summary>
        /// ���� ��� ������� ���������
        /// </summary>
        [SerializeField] private Color m_FreezedStateColor;

        /// <summary>
        /// ���������� ����
        /// </summary>
        [SerializeField] private Color m_NormalStateColor;

        private Timer stateTimer;

        /// <summary>
        /// ��������� ���������
        /// </summary>
        /// <param name="value">�������� ������������ ��������� ����</param>
        /// <param name="time">����� �������� ������������ ��������� ����</param>
        public void ChangeState(float time)
        {
            state = EnemyState.Freezed;
            stateTimer = new Timer(time);
            sprite.color = m_FreezedStateColor;
            m_Rigid.velocity = new Vector2(0, 0);
            m_Rigid.angularVelocity = 0;
            m_Rigid.isKinematic = true;
        }

        /// <summary>
        /// ���������� ���������
        /// </summary>
        private void RemoveState()
        {
            state = EnemyState.None;
            sprite.color = m_NormalStateColor;
            m_Rigid.isKinematic = false;
        }

        #endregion

        #region Timers

        private void InitTimers()
        {
            stateTimer = new Timer(0);
        }

        private void UpdateTimers()
        {
            stateTimer.RemoveTime(Time.deltaTime);
        }

        #endregion
    }
}
