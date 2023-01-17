namespace TowerDefense
{
    /// <summary>
    /// Перечисление звуков
    /// </summary>
    public enum Sound
    {
        Arrow = 0,
        ArrowHit = 1,
        EnemyDie = 2,
        /// <summary>
        /// Враг прорвался к игроку
        /// </summary>
        EnemyWin = 3,
        PlayerWin = 4,
        PlayerLose = 5,
        /// <summary>
        /// Фоновая музыка
        /// </summary>
        BGM = 6,
        MagicArrow = 7,
        MagicArrowHit = 8
    }

    /// <summary>
    /// Метод-расширение для перечисления
    /// </summary>
    public static class SoundExtensions
    {
        /// <summary>
        /// Проигрывание звука.
        /// Это всё чтобы можно было вызывать звуки как Sound.BGM.Play();
        /// </summary>
        /// <param name="sound">Звук</param>
        public static void Play(this Sound sound)
        {
            SoundPlayer.Instance.Play(sound);
        }
    }
}