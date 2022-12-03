using UnityEngine;

namespace TowerDefense
{

    /// <summary>
    /// �������
    /// </summary>
    public class EntitySpawner : Spawner
    {
        /// <summary>
        /// ������ �������� ��� ������
        /// </summary>
        [SerializeField] private GameObject[] m_EntityPrefabs;

        /// <summary>
        /// ���������� ����������� ������
        /// </summary>
        protected override GameObject GenerateSpawnedEntity()
        {
            return Instantiate(m_EntityPrefabs[Random.Range(0, m_EntityPrefabs.Length)]);
        }
    }
}