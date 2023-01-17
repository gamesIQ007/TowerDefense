namespace TowerDefense
{
    /// <summary>
    /// ������������ ������
    /// </summary>
    public enum Sound
    {
        Arrow = 0,
        ArrowHit = 1,
        EnemyDie = 2,
        /// <summary>
        /// ���� ��������� � ������
        /// </summary>
        EnemyWin = 3,
        PlayerWin = 4,
        PlayerLose = 5,
        /// <summary>
        /// ������� ������
        /// </summary>
        BGM = 6,
        MagicArrow = 7,
        MagicArrowHit = 8
    }

    /// <summary>
    /// �����-���������� ��� ������������
    /// </summary>
    public static class SoundExtensions
    {
        /// <summary>
        /// ������������ �����.
        /// ��� �� ����� ����� ���� �������� ����� ��� Sound.BGM.Play();
        /// </summary>
        /// <param name="sound">����</param>
        public static void Play(this Sound sound)
        {
            SoundPlayer.Instance.Play(sound);
        }
    }
}