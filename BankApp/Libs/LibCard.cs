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
    public static class LibCard
    {
        /// <summary>
        /// Статический метод. Создаёт запись о карте в БД
        /// </summary>
        /// <returns>Результат добавления записи в БД</returns>
        public static string AddCard()
        {
            string number = generateNumber();
            uint cvc = generateCvc();

            if (number == "Error")
                return null;
            if (!DB.OpenConnection())
                return null;

            DB.Command.CommandText = "INSERT INTO card VALUES (@Number, @CVC, @DateTo, @DateFrom, @StatusID);";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@Number", MySqlDbType.VarChar).Value = number;
            DB.Command.Parameters.Add("@CVC", MySqlDbType.UInt32).Value = cvc;
            DB.Command.Parameters.Add("@DateTo", MySqlDbType.DateTime).Value = DateTime.Now.AddYears(1);
            DB.Command.Parameters.Add("@DateFrom", MySqlDbType.DateTime).Value = DateTime.Now;
            DB.Command.Parameters.Add("@StatusID", MySqlDbType.Int32).Value = (int)CardStatus.Активна;

            int rows = DB.Command.ExecuteNonQuery();

            DB.CloseConnection();

            if (rows == 0)
                return null;
            return number;
        }

        /// <summary>
        /// Статический метод. Возвращает информацию о карте по номеру
        /// </summary>
        /// <param name="number">Номер карты</param>
        /// <returns>Информация о карте</returns>
        public static Card GetCardByNumber(string number)
        {
            if (!DB.OpenConnection())
                return null;
            DB.Command.CommandText = "SELECT * FROM card WHERE Number = @Number";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@Number", MySqlDbType.VarChar).Value = number;

            DB.Adapter.SelectCommand = DB.Command;

            DataTable table = new DataTable();
            DB.Adapter.Fill(table);

            DB.CloseConnection();

            if (table.Rows.Count > 0)
            {
                Card card = new Card
                {
                    Number = DB.ConvertFromDBVal<string>(table.Rows[0].ItemArray[0]),
                    CVC = DB.ConvertFromDBVal<uint>(table.Rows[0].ItemArray[1]),
                    DateTo = DB.ConvertFromDBVal<DateTime>(table.Rows[0].ItemArray[2]),
                    DateFrom = DB.ConvertFromDBVal<DateTime>(table.Rows[0].ItemArray[3]),
                    Status = DB.ConvertFromDBVal<CardStatus>(table.Rows[0].ItemArray[4]),
                };

                return card;
            }

            table.Clear();
            return null;
        }


        public static bool UpdateCard(Card card)
        {
            if (!DB.OpenConnection())
                return false;

            DB.Command.CommandText = "UPDATE CARD SET Number = @Number, CVC = @CVC, DateTo = @DateTo, DateFrom = @DateFrom, StatusID = @StatusID WHERE Number = @Number;";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@Number", MySqlDbType.VarChar).Value = card.Number;
            DB.Command.Parameters.Add("@CVC", MySqlDbType.UInt32).Value = card.CVC;
            DB.Command.Parameters.Add("@DateTo", MySqlDbType.Date).Value = card.DateTo;
            DB.Command.Parameters.Add("@DateFrom", MySqlDbType.Date).Value = card.DateFrom;
            DB.Command.Parameters.Add("@StatusID", MySqlDbType.UInt32).Value = card.Status;

            int rows = DB.Command.ExecuteNonQuery();

            DB.CloseConnection();

            if (rows == 0)
                return false;
            return true;
        }

        /// <summary>
        /// Статический метод. Генерирует трёхзначный CVC код карты
        /// </summary>
        /// <returns>CVC код карты</returns>
        private static uint generateCvc()
        {
            Random random = new Random((int)DateTime.Now.Ticks);

            return (uint)random.Next(0, 999);
        }

        /// <summary>
        /// Статический метод. Генерирует 16-ти значный номер карты
        /// </summary>
        /// <returns>Номер карты</returns>
        private static string generateNumber()
        {
            string result = string.Empty;

            while (true)
            {
                Random random = new Random((int)DateTime.Now.Ticks);

                for (int i = 0; i < 16; i++)
                {
                    result += random.Next(0, 9);
                }

                if (!DB.OpenConnection())
                    return "Error";

                DB.Command.CommandText = "SELECT * FROM card WHERE Number = @Number;";
                DB.Command.Connection = DB.GetConnection();
                DB.Command.Parameters.Clear();

                DB.Command.Parameters.Add("@Number", MySqlDbType.VarChar).Value = result;

                int rows = DB.Command.ExecuteNonQuery();
                if (rows <= 0)
                    break;
            }
            return result;

        }

    }
}
