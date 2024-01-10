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

            if (!DB.OpenConnection())
                return null;
            DB.Command.CommandText = "SELECT * FROM v_bill WHERE Owner = @user;";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@user", MySqlDbType.VarChar).Value = user.Login;

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
                        Owner = DB.ConvertFromDBVal<string>(row.ItemArray[2]),
                        CardNumber = DB.ConvertFromDBVal<string>(row.ItemArray[3]),
                        Name = DB.ConvertFromDBVal<string>(row.ItemArray[4]),
                        Status = DB.ConvertFromDBVal<BillStatus>(row.ItemArray[5]),
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
