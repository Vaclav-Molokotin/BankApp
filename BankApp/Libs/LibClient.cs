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
    /// Статический класс для работы с клиентами банка
    /// </summary>
    public static class LibClient
    {
        /// <summary>
        /// Статический метод. Возвращает список счетов клиента
        /// </summary>
        /// <param name="user">Клиент</param>
        /// <returns>Список счетов клиента</returns>
        public static List<Bill>? GetBillsByClient(User user)
        {
            List<Bill> bills = new List<Bill>();

            DB.OpenConnection();
            DB.Command.CommandText = "SELECT * FROM bill WHERE OwnerID = @userID;";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@userID", MySqlDbType.UInt32).Value = user.Id;

            DB.Adapter.SelectCommand = DB.Command;
            DB.Adapter.Fill(DB.Table);

            DB.CloseConnection();
            

            if(DB.Table.Rows.Count > 0 )
            {
                foreach (DataRow row in DB.Table.Rows)
                {
                    Bill bill = new Bill
                    {
                        Number = DB.ConvertFromDBVal<string>(row.ItemArray[0]),
                        Balance = DB.ConvertFromDBVal<float>(row.ItemArray[1]),
                        OwnerID = DB.ConvertFromDBVal<uint>(row.ItemArray[2]),
                        CardNumber = DB.ConvertFromDBVal<string>(row.ItemArray[3]),
                        IsFrozen = DB.ConvertFromDBVal<bool>(row.ItemArray[4])
                    };

                    bills.Add(bill);
                }
                DB.Table.Clear();
                return bills;
            }
            DB.Table.Clear();
            return null;
        }
    }
}
