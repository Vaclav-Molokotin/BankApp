using BankApp.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Libs
{
    /// <summary>
    /// Статический класс для работы со счетами
    /// </summary>
    public static class LibBill
    {
        /// <summary>
        /// Статический метод. Создаёт запись о счёте указанного пользователя в БД
        /// </summary>
        /// <param name="user">Пользователь, для которого создаётся счёт</param>
        /// <returns>Результат добавления</returns>
        public static bool CreateBill(User user)
        {
            string number = generateNumber();

            DB.OpenConnection();
            DB.Command.CommandText = "INSERT INTO bill VALUES (@Number, @Balance, @OwnerID, @CardNumber, @IsFrozen);";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@Number", MySqlDbType.VarChar).Value = number;
            DB.Command.Parameters.Add("@Balance", MySqlDbType.Float).Value = 0.0;
            DB.Command.Parameters.Add("@OwnerID", MySqlDbType.Int32).Value = user.Id;
            DB.Command.Parameters.Add("@CardNumber", MySqlDbType.VarChar).Value = null;
            DB.Command.Parameters.Add("@IsFrozen", MySqlDbType.Bit).Value = 0;

            int rows = DB.Command.ExecuteNonQuery();

            DB.CloseConnection();

            if (rows == 0)
                return false;
            return true;
        }

        /// <summary>
        /// Статический метод. Создаёт уникальный номер счёта
        /// </summary>
        /// <returns>Уникальный номер счёта</returns>
        private static string generateNumber()
        {            
            string result = string.Empty;

            while (true)
            {
                Random random = new Random((int)DateTime.Now.Ticks);

                for (int i = 0; i < 20; i++)
                {
                    result += random.Next(0, 9);
                }

                DB.OpenConnection();
                DB.Command.CommandText = "SELECT * FROM bill WHERE Number = @Number;";
                DB.Command.Connection = DB.GetConnection();
                DB.Command.Parameters.Clear();

                DB.Command.Parameters.Add("@Number", MySqlDbType.VarChar).Value = result;

                int rows = DB.Command.ExecuteNonQuery();
                if (rows <= 0)
                    break;
            }
            return result;
        }

        /// <summary>
        /// Статический метод. Удаляет указанный счёт
        /// </summary>
        /// <param name="bill">Удаляемый счёт</param>
        /// <returns>Результат удаления</returns>
        public static bool DeleteBill(Bill bill)
        {
            DB.OpenConnection();
            DB.Command.CommandText = "DELETE FROM bill WHERE Number = @Number";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@Number", MySqlDbType.VarChar).Value = bill.Number;

            int rows = DB.Command.ExecuteNonQuery();

            DB.CloseConnection();

            if (rows == 0) 
                return false;
            return true;
        }
    }
}
