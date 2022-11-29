/// <summary>
/// Класс таймера
/// </summary>
public class Timer
{
    /// <summary>
    /// Текущее время
    /// </summary>
    private float m_CurrentTime;

    /// <summary>
    /// Таймер вышел
    /// </summary>
    public bool IsFinished => m_CurrentTime <= 0;

    public Timer(float startTime)
    {
        Start(startTime);
    }

    /// <summary>
    /// Установить таймер
    /// </summary>
    /// <param name="startTime">Время</param>
    public void Start(float startTime)
    {
        m_CurrentTime = startTime;
    }

    /// <summary>
    /// Вычесть время из таймера
    /// </summary>
    /// <param name="deltaTime">Вычитаемое время</param>
    public void RemoveTime(float deltaTime)
    {
        if (m_CurrentTime <= 0) return;

        m_CurrentTime -= deltaTime;
    }
}
