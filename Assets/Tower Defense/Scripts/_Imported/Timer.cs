/// <summary>
/// ����� �������
/// </summary>
public class Timer
{
    /// <summary>
    /// ������� �����
    /// </summary>
    private float m_CurrentTime;

    /// <summary>
    /// ������ �����
    /// </summary>
    public bool IsFinished => m_CurrentTime <= 0;

    public Timer(float startTime)
    {
        Start(startTime);
    }

    /// <summary>
    /// ���������� ������
    /// </summary>
    /// <param name="startTime">�����</param>
    public void Start(float startTime)
    {
        m_CurrentTime = startTime;
    }

    /// <summary>
    /// ������� ����� �� �������
    /// </summary>
    /// <param name="deltaTime">���������� �����</param>
    public void RemoveTime(float deltaTime)
    {
        if (m_CurrentTime <= 0) return;

        m_CurrentTime -= deltaTime;
    }
}
