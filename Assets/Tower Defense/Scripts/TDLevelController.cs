using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    /// <summary>
    /// ����� ���������� ������ � ���������� ������� ������
    /// </summary>
    public class TDLevelController : LevelController
    {
        /// <summary>
        /// ���� ����������� ������
        /// </summary>
        private int m_LevelScore = 3;

        /// <summary>
        /// ������� ������� �� �������
        /// </summary>
        [SerializeField] private UpgradeAsset m_TimeUpgrade;

        private new void Start()
        {
            base.Start();
            // ���� ������ ����� ������-�������. ���������� ������� ��� ����������, ������� ����� ���������� �� ������� OnPlayerDead. ��� ��� �� ������ ��������� �������.
            TDPlayer.Instance.OnPlayerDead += () =>
            {
                StopLevelActivity();
                LevelResultController.Instance.ShowResult(false);
            };

            m_ReferenceTime += Time.time;
            m_LevelCompleted.AddListener(() =>
            {
                StopLevelActivity();

                if (m_ReferenceTime < Time.time)
                {
                    m_LevelScore--;
                }
                
                MapCompletion.SaveEpisodeResult(m_LevelScore);
            });

            // ��������� �������, ����� ��� ������������ ��������� ���������� ������, ����������� ��������
            void LifeScoreChange(int _)
            {
                m_LevelScore--;
                TDPlayer.OnLifeUpdate -= LifeScoreChange;
            }
            TDPlayer.OnLifeUpdate += LifeScoreChange;
        }

        private new void Awake()
        {
            base.Awake();
            int timeUpgrade = (int)(Upgrades.GetUpgradeLevel(m_TimeUpgrade) * Upgrades.GetUpgradeModifier(m_TimeUpgrade));
            m_ReferenceTime += timeUpgrade;
        }

        /// <summary>
        /// ��������� ���������� �� ������
        /// </summary>
        private void StopLevelActivity()
        {
            foreach (var enemy in FindObjectsOfType<Enemy>())
            {
                enemy.GetComponent<SpaceShip>().enabled = false;
                enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }

            // ���� ��� ������� ������������ ������ � ��������� ��
            void DisableAll<T>() where T : MonoBehaviour
            {
                foreach (var obj in FindObjectsOfType<T>())
                {
                    obj.enabled = false;
                }
            }

            DisableAll<Spawner>();
            DisableAll<Projectile>();
            DisableAll<Tower>();
            DisableAll<UINextWave>();
        }
    }
}