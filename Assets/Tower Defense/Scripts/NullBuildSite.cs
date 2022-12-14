using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    /// <summary>
    /// �������� ����� �� ����� ������������� ��� �������� ���������
    /// </summary>
    public class NullBuildSite : BuildSite
    {
        /// <summary>
        /// ��������� ����� �����
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnPointerDown(PointerEventData eventData)
        {
            HideControls();
        }
    }
}