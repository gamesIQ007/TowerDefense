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
        /// ��������� ��������� ���������� � ������ ����������� ������
        /// </summary>
        public int m_LevelScore => 1;

        private new void Start()
        {
            base.Start();
            // ���� ������ ����� ������-�������. ���������� ������� ��� ����������, ������� ����� ���������� �� ������� OnPlayerDead. ��� ��� �� ������ ��������� �������.
            TDPlayer.Instance.OnPlayerDead += () =>
            {
                StopLevelActivity();
                LevelResultController.Instance.ShowResult(false);
            };
            m_LevelCompleted.AddListener(() =>
            {
                StopLevelActivity();
                MapCompletion.SaveEpisodeResult(m_LevelScore);
            });
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
        }
    }
}