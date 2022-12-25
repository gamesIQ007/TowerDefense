using System;
using System.IO;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Работа с файлом сохранений
    /// </summary>
    [Serializable]
    public class Saver<T>
    {
        /// <summary>
        /// Переменная для загружаемых в конструкторе данных
        /// </summary>
        public T m_Data;

        /// <summary>
        /// Попытка загрузить файл и его загрузка
        /// </summary>
        /// <param name="filename">Имя файла</param>
        /// <param name="data">Ссылка на данные, куда грузить</param>
        public static void TryLoad(string filename, ref T data)
        {
            var path = FileHandler.m_Path(filename);
            if (File.Exists(path))
            {
                var dataString = File.ReadAllText(path);
                var saver = JsonUtility.FromJson<Saver<T>>(dataString);
                data = saver.m_Data;
            }
        }

        /// <summary>
        /// Сохранение данных в файл
        /// </summary>
        /// <param name="filename">Имя файла</param>
        /// <param name="data">Сохраняемые данные</param>
        public static void Save(string filename, T data)
        {
            // без обёртки массивы напрямую не пишутся в файл
            var wrapper = new Saver<T> { m_Data = data };
            var dataString = JsonUtility.ToJson(wrapper);
            File.WriteAllText(FileHandler.m_Path(filename), dataString);
        }
    }

    /// <summary>
    /// Обработчик файла. Выделен из Saver для независимости.
    /// </summary>
    public static class FileHandler
    {
        /// <summary>
        /// Генерация полного пути
        /// </summary>
        /// <param name="filename">Имя файла</param>
        /// <returns>Полный путь к файлу</returns>
        public static string m_Path(string filename)
        {
            return $"{Application.persistentDataPath}/{filename}";
        }

        /// <summary>
        /// Очистка файла сохранений
        /// </summary>
        /// <param name="filename">Имя файла</param>
        public static void Reset(string filename)
        {
            var path = m_Path(filename);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        /// <summary>
        /// Проверка наличия файла
        /// </summary>
        /// <param name="filename">Имя файла</param>
        /// <returns>Имеется ли файл</returns>
        public static bool HasFile(string filename)
        {
            return File.Exists(m_Path(filename));
        }
    }
}