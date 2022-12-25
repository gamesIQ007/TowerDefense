using System;
using System.IO;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ������ � ������ ����������
    /// </summary>
    [Serializable]
    public class Saver<T>
    {
        /// <summary>
        /// ���������� ��� ����������� � ������������ ������
        /// </summary>
        public T m_Data;

        /// <summary>
        /// ������� ��������� ���� � ��� ��������
        /// </summary>
        /// <param name="filename">��� �����</param>
        /// <param name="data">������ �� ������, ���� �������</param>
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
        /// ���������� ������ � ����
        /// </summary>
        /// <param name="filename">��� �����</param>
        /// <param name="data">����������� ������</param>
        public static void Save(string filename, T data)
        {
            // ��� ������ ������� �������� �� ������� � ����
            var wrapper = new Saver<T> { m_Data = data };
            var dataString = JsonUtility.ToJson(wrapper);
            File.WriteAllText(FileHandler.m_Path(filename), dataString);
        }
    }

    /// <summary>
    /// ���������� �����. ������� �� Saver ��� �������������.
    /// </summary>
    public static class FileHandler
    {
        /// <summary>
        /// ��������� ������� ����
        /// </summary>
        /// <param name="filename">��� �����</param>
        /// <returns>������ ���� � �����</returns>
        public static string m_Path(string filename)
        {
            return $"{Application.persistentDataPath}/{filename}";
        }

        /// <summary>
        /// ������� ����� ����������
        /// </summary>
        /// <param name="filename">��� �����</param>
        public static void Reset(string filename)
        {
            var path = m_Path(filename);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        /// <summary>
        /// �������� ������� �����
        /// </summary>
        /// <param name="filename">��� �����</param>
        /// <returns>������� �� ����</returns>
        public static bool HasFile(string filename)
        {
            return File.Exists(m_Path(filename));
        }
    }
}