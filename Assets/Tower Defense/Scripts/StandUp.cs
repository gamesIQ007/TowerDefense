using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ������� ������� �� ����������� ����� � ���������� ����������� ��� ����������� �� ������������� �������
    /// </summary>
    public class StandUp : MonoBehaviour
    {
        /// <summary>
        /// ����� ��������
        /// </summary>
        private Rigidbody2D rig;

        /// <summary>
        /// ������
        /// </summary>
        private SpriteRenderer sr;

        private void Start()
        {
            rig = transform.root.GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()
        {
            transform.up = Vector2.up;

            sr.flipX = rig.velocity.x > 0.01f ? false : true;
        }
    }
}