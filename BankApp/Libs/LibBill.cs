using BankApp.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
        public static bool AddBill(User user)
        {
            string number = generateNumber();
            if (number == "Error")
                return false;
            if (!DB.OpenConnection())
                return false;
            DB.Command.CommandText = "INSERT INTO bill (Number, Balance, OwnerID, StatusID) VALUES (@Number, @Balance, @OwnerID, @StatusID);";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@Number", MySqlDbType.VarChar).Value = number;
            DB.Command.Parameters.Add("@Balance", MySqlDbType.Float).Value = 0.0;
            DB.Command.Parameters.Add("@OwnerID", MySqlDbType.Int32).Value = user.Id;
            DB.Command.Parameters.Add("@StatusID", MySqlDbType.Int32).Value = (int)BillStatus.Активен;

            int rows = DB.Command.ExecuteNonQuery();

            DB.CloseConnection();

            if (rows == 0)
                return false;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        /// <param name="bill"></param>
        /// <returns></returns>
        public static bool BindCard(Card card, Bill bill)
        {
            if (!DB.OpenConnection())
                return false;

            DB.Command.CommandText = "UPDATE bill SET CardNumber = @CardNumber WHERE Number = @Number;";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@Number", MySqlDbType.VarChar).Value = bill.Number;
            DB.Command.Parameters.Add("@CardNumber", MySqlDbType.VarChar).Value = card.Number;

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

                if (!DB.OpenConnection())
                    return "Error";

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
            if (!DB.OpenConnection())
                return false;
            DB.Command.CommandText = "UPDATE bill SET StatusID = 3 WHERE Number = @Number";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@Number", MySqlDbType.VarChar).Value = bill.Number;

            int rows = DB.Command.ExecuteNonQuery();

            DB.CloseConnection();

            if (rows == 0)
                return false;
            return true;
        }

        /// <summary>
        /// Статический метод. Возвращает карту указанного счёта
        /// </summary>
        /// <param name="bill">Счёт</param>
        /// <returns>Актуальная карта счёта</returns>
        public static Card? GetCardByBill(Bill bill)
        {
            Card card = new Card();

            if (!DB.OpenConnection())
                return null;
            DB.Command.CommandText = "SELECT * FROM card c INNER JOIN bill b ON c.Number = b.CardNumber WHERE b.Number = @number;";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@number", MySqlDbType.VarChar).Value = bill.Number;

            DB.Adapter.SelectCommand = DB.Command;
            DB.Adapter.Fill(DB.Table);

            DB.CloseConnection();

            DataRow row = DB.Table.Rows[0];
            if (DB.Table.Rows.Count > 0)
            {
                card = new Card
                {
                    Number = DB.ConvertFromDBVal<string>(row.ItemArray[0]),
                    CVC = DB.ConvertFromDBVal<string>(row.ItemArray[1]),
                    DateTo = DB.ConvertFromDBVal<DateTime>(row.ItemArray[2]),
                    DateFrom = DB.ConvertFromDBVal<DateTime>(row.ItemArray[3]),
                    Status = DB.ConvertFromDBVal<CardStatus>(row.ItemArray[4])
                };

                DB.Table.Clear();
                return card;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Статический метод. Возвращает счёт по его номеру
        /// </summary>
        /// <param name="number">Счёт</param>
        /// <returns>Счёт</returns>
        public static Bill? GetBillByNumber(string number)
        {
            if (!DB.OpenConnection())
                return null;
            DB.Command.CommandText = "SELECT * FROM bill WHERE Number = @number;";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@number", MySqlDbType.VarChar).Value = number;

            DB.Adapter.SelectCommand = DB.Command;
            DB.Adapter.Fill(DB.Table);

            DB.CloseConnection();

            
            if (DB.Table.Rows.Count > 0)
            {
                DataRow row = DB.Table.Rows[0];
                Bill bill = new Bill
                {
                    Number = DB.ConvertFromDBVal<string>(row.ItemArray[0]),
                    Balance = DB.ConvertFromDBVal<float>(row.ItemArray[1]),
                    Owner = DB.ConvertFromDBVal<string>(row.ItemArray[2]),
                    CardNumber = DB.ConvertFromDBVal<string>(row.ItemArray[3]),
                    Name = DB.ConvertFromDBVal<string>(row.ItemArray[4]),
                    Status = DB.ConvertFromDBVal<BillStatus>(row.ItemArray[5])
                };

                DB.Table.Clear();
                return bill;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Статический метод. Возвращает счёт по указанной карте
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static Bill? GetBillByCard(Card card)
        {
            if (!DB.OpenConnection())
                return null;
            DB.Command.CommandText = "SELECT * FROM bill WHERE CardNumber = @CardNumber;";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@CardNumber", MySqlDbType.VarChar).Value = card.Number;

            DB.Adapter.SelectCommand = DB.Command;
            DB.Adapter.Fill(DB.Table);

            DB.CloseConnection();

            DataRow row = DB.Table.Rows[0];
            if (DB.Table.Rows.Count > 0)
            {
                Bill bill = new Bill
                {
                    Number = DB.ConvertFromDBVal<string>(row.ItemArray[0]),
                    Balance = DB.ConvertFromDBVal<float>(row.ItemArray[1]),
                    Owner = DB.ConvertFromDBVal<string>(row.ItemArray[2]),
                    CardNumber = DB.ConvertFromDBVal<string>(row.ItemArray[3]),
                    Name = DB.ConvertFromDBVal<string>(row.ItemArray[4]),
                    Status = DB.ConvertFromDBVal<BillStatus>(row.ItemArray[5])
                };

                DB.Table.Clear();
                return bill;
            }
            else
            {
                return null;
            }
        }
    }
}
