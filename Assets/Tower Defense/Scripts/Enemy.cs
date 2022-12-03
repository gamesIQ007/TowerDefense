using UnityEngine;
using SpaceShooter;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefense
{
    [RequireComponent(typeof(TDPatrolController))]

    /// <summary>
    /// Враг. Получает настройки из ScriptableObject
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        /// <summary>
        /// Применить настройки из ScriptableObject к врагу
        /// </summary>
        /// <param name="asset">Настройки</param>
        public void Use(EnemyAsset asset)
        {
            SpriteRenderer sr = transform.GetComponentInChildren<SpriteRenderer>();
            sr.color = asset.color;
            sr.transform.localScale = new Vector3(asset.spriteScale.x, asset.spriteScale.y, 1);

            sr.GetComponentInChildren<Animator>().runtimeAnimatorController = asset.animations;

            GetComponent<SpaceShip>().Use(asset);

            CircleCollider2D col = transform.GetComponentInChildren<CircleCollider2D>();
            col.radius = asset.radius;
            col.offset = new Vector2 (0, asset.colliderOffsetY);
        }
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(Enemy))]
    public class EnemyInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EnemyAsset asset = EditorGUILayout.ObjectField(null, typeof(EnemyAsset), false) as EnemyAsset;

            if (asset != null)
            {
                (target as Enemy).Use(asset);
            }
        }
    }

#endif
}