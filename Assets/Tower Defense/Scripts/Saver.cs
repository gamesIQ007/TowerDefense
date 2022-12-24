using System;
using System.Collections;
using System.Collections.Generic;
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
        /// ��������� ������� ����
        /// </summary>
        /// <param name="filename">��� �����</param>
        /// <returns>������ ���� � �����</returns>
        private static string m_Path(string filename)
        {
            return $"{Application.persistentDataPath}/{filename}";
        }

        /// <summary>
        /// ������� ��������� ���� � ��� ��������
        /// </summary>
        /// <param name="filename">��� �����</param>
        /// <param name="data">������ �� ������, ���� �������</param>
        public static void TryLoad(string filename, ref T data)
        {
            var path = m_Path(filename);
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
            File.WriteAllText(m_Path(filename), dataString);
        }
    }
}