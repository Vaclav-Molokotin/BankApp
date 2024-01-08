using BankApp.Models;
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
    /// Статический метод для работы с пользователями
    /// </summary>
    public static class LibUser
    {
        /// <summary>
        /// Статическое поле для хранения информации о текущем пользователе
        /// </summary>
        public static User? CurrentUser { get; set; }

        /// <summary>
        /// Статический метод. Возвращает информацию о пользователе по логину и паролю
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>Информация о пользователе</returns>
        public static User? GetUserByLogopas(string login, string password)
        {
            DB.OpenConnection();
            DB.Command.CommandText = "SELECT * FROM user WHERE login = @uL AND password = MD5(@uP)";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = login;
            DB.Command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = password;


            DB.Adapter.SelectCommand = DB.Command;

            DataTable table = new DataTable();
            DB.Adapter.Fill(table);

            DB.CloseConnection();

            if (table.Rows.Count > 0)
            {
                password = DB.ConvertFromDBVal<string>(table.Rows[0].ItemArray[5]);
                string phone = DB.ConvertFromDBVal<string>(table.Rows[0].ItemArray[6]);
                uint roleID = DB.ConvertFromDBVal<uint>(table.Rows[0].ItemArray[7]);

                User user = new User
                {
                    Id = DB.ConvertFromDBVal<uint>(table.Rows[0].ItemArray[0]),
                    FName = DB.ConvertFromDBVal<string>(table.Rows[0].ItemArray[1]),
                    LName = DB.ConvertFromDBVal<string>(table.Rows[0].ItemArray[2]),
                    MName = DB.ConvertFromDBVal<string>(table.Rows[0].ItemArray[3]),
                    Login = DB.ConvertFromDBVal<string>(table.Rows[0].ItemArray[4]),
                    Password = password,
                    Phone = phone,
                    RoleID = roleID                    
                };
                
                return user;
            }
            
            table.Clear();
            return null;
        }

        /// <summary>
        /// Статический метод. Создаёт запись пользователя в БД
        /// </summary>
        /// <param name="user">Добавляемый пользователь</param>
        /// <returns>Результат добавления</returns>
        public static bool CreateUser(User user)
        {             

            if (user.Errors.Count > 0)
                return false;

            DB.OpenConnection();

            DB.Command.CommandText = "INSERT INTO user (FName, LName, MName, Login, Password, Phone, RoleID) VALUES (@FName, @LName, @MName, @Login, MD5(@Password), @Phone, @RoleID);";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@FName", MySqlDbType.VarChar).Value = user.FName;
            DB.Command.Parameters.Add("@LName", MySqlDbType.VarChar).Value = user.LName;
            DB.Command.Parameters.Add("@MName", MySqlDbType.VarChar).Value = user.MName;
            DB.Command.Parameters.Add("@Login", MySqlDbType.VarChar).Value = user.Login;
            DB.Command.Parameters.Add("@Password", MySqlDbType.VarChar).Value = user.Password;
            DB.Command.Parameters.Add("@Phone", MySqlDbType.VarChar).Value = user.Phone;
            DB.Command.Parameters.Add("@RoleID", MySqlDbType.VarChar).Value = user.RoleID;

            int rows = DB.Command.ExecuteNonQuery();

            DB.CloseConnection();

            if (rows == 0)
                return false;

            return true;
        }

        /// <summary>
        /// Статический метод. Проверяет, занят ли указанный логин
        /// </summary>
        /// <param name="login">Логин</param>
        /// <returns>Занятость логина</returns>
        public static bool IsLoginExists(string login)
        {
            DB.OpenConnection();
            DB.Command.CommandText = "SELECT * FROM `user` WHERE `login` = @uL";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = login;

            DB.Adapter.SelectCommand = DB.Command;
            DataTable table = new DataTable();
            DB.Adapter.Fill(table);

            int rowsCount = table.Rows.Count;

            DB.CloseConnection();
            table.Clear();
            if (rowsCount > 0)
                return true;

            return false;
        }

        /// <summary>
        /// Статический метод. Проверяет занятость номера телефона
        /// </summary>
        /// <param name="phone">Номер телефона</param>
        /// <returns>Занятость телефона</returns>
        public static bool IsPhoneBusy(string phone)
        {
            DB.OpenConnection();
            DB.Command.CommandText = "SELECT * FROM `user` WHERE `phone` = @uP";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = phone;

            DB.Adapter.SelectCommand = DB.Command;
            DataTable table = new DataTable();
            DB.Adapter.Fill(table);

            int rowsCount = table.Rows.Count;

            DB.CloseConnection();
            table.Clear();
            if (rowsCount > 0)
                return true;

            return false;
        }
    }

}
