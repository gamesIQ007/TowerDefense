using UnityEngine;
using SpaceShooter;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefense
{
    /// <summary>
    /// Башня
    /// </summary>
    public class Tower : MonoBehaviour
    {
        /// <summary>
        /// Радиус
        /// </summary>
        [SerializeField] private float m_Radius;

        /// <summary>
        /// Туррели
        /// </summary>
        private Turret[] m_Turrets;

        /// <summary>
        /// Цель
        /// </summary>
        private Destructible m_Target;

        private void Update()
        {
            if (m_Target != null)
            {
                Vector2 targetVector = m_Target.transform.position - transform.position;

                if (targetVector.magnitude <= m_Radius)
                {
                    foreach (Turret turret in m_Turrets)
                    {
                        turret.transform.up = targetVector;
                        turret.Fire();
                    }
                }
                else
                {
                    m_Target = null;
                }
            }

            if (m_Target == null)
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Radius);
                
                if (enter != null)
                {
                    m_Target = enter.transform.root.GetComponent<Destructible>();
                }
            }
        }

        /// <summary>
        /// Применить ассет башни
        /// </summary>
        /// <param name="asset">Ассет</param>
        public void Use(TowerAsset asset)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = asset.sprite;
            m_Turrets = GetComponentsInChildren<Turret>();
            foreach (var turret in m_Turrets)
            {
                turret.AssignLoadout(asset.turret);
            }
        }

#if UNITY_EDITOR
        /// <summary>
        /// Цвет гизмо
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

