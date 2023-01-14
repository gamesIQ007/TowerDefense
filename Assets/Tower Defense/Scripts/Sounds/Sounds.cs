using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefense
{
    /// <summary>
    /// “ут будут все звуки
    /// </summary>
    
    [CreateAssetMenu]
    public class Sounds : ScriptableObject
    {
        [SerializeField] private AudioClip[] m_Sounds;

        /// <summary>
        /// »ндексер дл€ звуков.
        /// Ўоб т€гать звуки по номеру.
        /// </summary>
        /// <param name="sound">«вук</param>
        /// <returns></returns>
        public AudioClip this[Sound sound] => m_Sounds[(int)sound];

#if UNITY_EDITOR

        [CustomEditor(typeof(Sounds))]
        public class SoundsInspector : Editor
        {
            private static readonly int soundCount = Enum.GetValues(typeof(Sound)).Length;

            private new Sounds target => base.target as Sounds;

            public override void OnInspectorGUI()
            {
                if (target.m_Sounds.Length < soundCount)
                {
                    Array.Resize(ref target.m_Sounds, soundCount);
                }

                for (int i = 0; i < target.m_Sounds.Length; i++)
                {
                    target.m_Sounds[i] = EditorGUILayout.ObjectField($"{(Sound)i}", target.m_Sounds[i], typeof(AudioClip), false) as AudioClip;
                }
            }
        }

#endif

    }
}