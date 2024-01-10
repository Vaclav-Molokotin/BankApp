using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Libs
{
    /// <summary>
    /// Статический класс для работы с базой данных
    /// </summary>
    static class DB
    {
        // Строка соедиения с БД
        static MySqlConnection Connection = 
            new MySqlConnection("server=localhost;port=3306;username=root;password=539533;database=bankdb;convert zero datetime=True");
        // Таблица для хранения результатов запроса
        public static DataTable Table = new DataTable();
        // Адаптер для выполнения запросов
        public static MySqlDataAdapter Adapter = new MySqlDataAdapter();
        // Объект для хранения информации о запросе и его параметрах
        public static MySqlCommand Command = new MySqlCommand();

        /// <summary>
        /// Статический метод. Открывает соединение с БД
        /// </summary>
        static public bool OpenConnection()
        {
            if(Connection.State == ConnectionState.Closed)
            {
                try
                {
                    Connection.Open();
                }
                catch 
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Статический метод. Закрывает соединение с БД
        /// </summary>
        static public void CloseConnection()
        {
            if (Connection.State == ConnectionState.Open)
            {
                
                Connection.Close();
            }
        }

        /// <summary>
        /// Статический метод. Возращает текущее соединение с БД
        /// </summary>
        /// <returns></returns>
        static public MySqlConnection GetConnection()
        {
            return Connection;
        }

        /// <summary>
        /// Статический метод. Конвертирует типы данных MySQL в типы C#
        /// </summary>
        /// <typeparam name="T">Тип C#</typeparam>
        /// <param name="obj">Конвертируемый объект</param>
        /// <returns>Объект целевого типа</returns>
        public static T ConvertFromDBVal<T>(object? obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return default(T);
            }
            else
            {
                return (T)obj;
            }
        }
    }
}
