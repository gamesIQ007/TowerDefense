using UnityEngine;

/// <summary>
/// Базовый класс для синглтонов
/// </summary>
[DisallowMultipleComponent]
public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("Singleton")]
    [SerializeField] private bool m_DoNotDestroyOnLoad;

    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("MonoSingleton: object of type already exist, instance will be destroyed = " + typeof(T).Name);
            Destroy(gameObject);
            return;
        }

        Instance = this as T;

        if (m_DoNotDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
