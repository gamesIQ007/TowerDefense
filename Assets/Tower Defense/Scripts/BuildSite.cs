using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    /// <summary>
    /// ����� ��������� �����
    /// </summary>
    public class BuildSite : MonoBehaviour, IPointerDownHandler
    {
        /// <summary>
        /// ������ ��������� ��� ��������� �����
        /// </summary>
        public TowerAsset[] buildableTowers;
        public void SetBuildableTowers(TowerAsset[] towers) 
        { 
            if (towers == null || towers.Length == 0)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                buildableTowers = towers;
            }
        }

        /// <summary>
        /// ������� �� �����
        /// </summary>
        public static event Action<BuildSite> OnClickEvent;

        /// <summary>
        /// ��������� ����� �����
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent(this);
        }

        /// <summary>
        /// �������� ������� ����� ��� ���������
        /// </summary>
        public static void HideControls()
        {
            OnClickEvent(null);
        }
    }
}