using UnityEngine;
using SpaceShooter;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefense
{
    /// <summary>
    /// �����
    /// </summary>
    public class Tower : MonoBehaviour
    {
        /// <summary>
        /// ������
        /// </summary>
        [SerializeField] private float m_Radius;

        /// <summary>
        /// �������
        /// </summary>
        private Turret[] turrets;

        /// <summary>
        /// ����
        /// </summary>
        private Destructible target;

        private void Update()
        {
            if (target != null)
            {
                Vector2 targetVector = target.transform.position - transform.position;

                if (targetVector.magnitude <= m_Radius)
                {
                    foreach (Turret turret in turrets)
                    {
                        turret.transform.up = targetVector;
                        turret.Fire();
                    }
                }
                else
                {
                    target = null;
                }
            }

            if (target == null)
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Radius);
                
                if (enter != null)
                {
                    target = enter.transform.root.GetComponent<Destructible>();
                }
            }
        }

        /// <summary>
        /// ��������� ����� �����
        /// </summary>
        /// <param name="asset">�����</param>
        public void Use(TowerAsset asset)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = asset.sprite;
            turrets = GetComponentsInChildren<Turret>();
            foreach (var turret in turrets)
            {
                turret.AssignLoadout(asset.turret);
            }
        }

#if UNITY_EDITOR
        /// <summary>
        /// ���� �����
        /// </summary>
        private static Color GizmoColor = new Color(0, 1, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Handles.color = GizmoColor;
            Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius);
        }
#endif
    }
}

