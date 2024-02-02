using BankApp.Models;
using BankApp.ViewModels;
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
    /// Статический метод для работы с транзакциями.
    /// </summary>
    public class LibTransaction
    {
        /// <summary>
        /// Статический метод. Возвращает список транзакций, инициированных клиентом
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static List<TransactionTransferView>? GetTransferTransactionsByClient(Client client) 
        {
            List<TransactionTransferView> transactions = new List<TransactionTransferView>();

            if (!DB.OpenConnection())
                return null;

            DB.Command.CommandText = "SELECT * FROM v_transaction_transfer WHERE OwnerID = @OwnerID;";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@OwnerID", MySqlDbType.UInt32).Value = client.Id;

            DB.Adapter.SelectCommand = DB.Command;
            DataTable dataTable = new DataTable();
            DB.Adapter.Fill(dataTable);

            DB.CloseConnection();
            

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    TransactionTransferView transaction = new TransactionTransferView
                    {
                        Type = DB.ConvertFromDBVal<string>(row.ItemArray[0]),
                        BillFromNumber = DB.ConvertFromDBVal<string>(row.ItemArray[1]),
                        BillToNumber = DB.ConvertFromDBVal<string>(row.ItemArray[2]),
                        Recipient = DB.ConvertFromDBVal<string>(row.ItemArray[3]),
                        Amount = DB.ConvertFromDBVal<float>(row.ItemArray[4]),
                        TransactionID = DB.ConvertFromDBVal<uint>(row.ItemArray[5]),                                              
                        Date = DB.ConvertFromDBVal<DateTime>(row.ItemArray[6]),
                        Status = DB.ConvertFromDBVal<string>(row.ItemArray[7])  
                    };

                    transactions.Add(transaction);
                }
                DB.Table.Clear();
                return transactions;
            }

            return null;
        }

        /// <summary>
        /// Статический метод. Добавляет транзакцию в БД
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static bool AddTransaction(Transaction transaction)
        {
            if (!DB.OpenConnection())
                return false;

            DB.Command.CommandText = "INSERT INTO transaction (TypeID, BillToNumber, BillFromNumber, Amount, Date, Sender) VALUES (@TypeID, @BillToNumber, @BillFromNumber, @Amount, @Date, @Sender);";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@TypeID", MySqlDbType.Int32).Value = (int)transaction.Type;
            DB.Command.Parameters.Add("@BillToNumber", MySqlDbType.VarChar).Value = transaction.BillToNumber;
            DB.Command.Parameters.Add("@BillFromNumber", MySqlDbType.VarChar).Value = transaction.BillFromNumber;
            DB.Command.Parameters.Add("@Amount", MySqlDbType.VarChar).Value = transaction.Amount;
            DB.Command.Parameters.Add("@Date", MySqlDbType.VarChar).Value = transaction.Date;
            DB.Command.Parameters.Add("@Sender", MySqlDbType.VarChar).Value = transaction.Sender;

            int rows = DB.Command.ExecuteNonQuery();

            DB.CloseConnection();

            if (rows == 0)
                return false;

            return true;
        }

        /// <summary>
        /// Возвращает транзакции по счёту
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        public static List<Transaction>? GetTransactionsByBill(Bill bill) 
        {
            List<Transaction> transactions = new List<Transaction>();

            if (!DB.OpenConnection())
                return null;

            DB.Command.CommandText = "SELECT * FROM transaction WHERE BillFromNumber = @BillNumber;";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@BillNumber", MySqlDbType.VarChar).Value = bill.Number;

            DB.Adapter.SelectCommand = DB.Command;
            DB.Adapter.Fill(DB.Table);

            DB.CloseConnection();

            if (DB.Table.Rows.Count > 0)
            {
                foreach (DataRow row in DB.Table.Rows)
                {
                    Transaction transaction = new Transaction
                    {
                        Id = DB.ConvertFromDBVal<uint>(row.ItemArray[0]),
                        Type = DB.ConvertFromDBVal<TransactionType>(row.ItemArray[1]),
                        BillToNumber = DB.ConvertFromDBVal<string>(row.ItemArray[2]),
                        BillFromNumber = DB.ConvertFromDBVal<string>(row.ItemArray[3]),
                        Status = DB.ConvertFromDBVal<TransactionStatus>(row.ItemArray[4]),
                        Amount = DB.ConvertFromDBVal<float>(row.ItemArray[5]),
                        Date = DB.ConvertFromDBVal<DateTime>(row.ItemArray[6]),
                        Sender = DB.ConvertFromDBVal<string>(row.ItemArray[7])
                    };

                    transactions.Add(transaction);
                }
                DB.Table.Clear();
                return transactions;
            }

            return null;
        }

        /// <summary>
        /// Статический метод. Возвращает транзакции по клиенту 
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static List<Transaction>? GetTransactionsByClient(Client client)
        {
            List<Transaction> transactions = new List<Transaction>();

            if (!DB.OpenConnection())
                return null;

            DB.Command.CommandText = "SELECT * FROM transaction WHERE SenderID = @SenderID;";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@SenderID", MySqlDbType.UInt32).Value = client.Id;

            DB.Adapter.SelectCommand = DB.Command;
            DB.Adapter.Fill(DB.Table);

            DB.CloseConnection();

            if (DB.Table.Rows.Count > 0)
            {
                foreach (DataRow row in DB.Table.Rows)
                {
                    Transaction transaction = new Transaction
                    {
                        Id = DB.ConvertFromDBVal<uint>(row.ItemArray[0]),
                        Type = DB.ConvertFromDBVal<TransactionType>(row.ItemArray[1]),
                        BillToNumber = DB.ConvertFromDBVal<string>(row.ItemArray[2]),
                        BillFromNumber = DB.ConvertFromDBVal<string>(row.ItemArray[3]),
                        Status = DB.ConvertFromDBVal<TransactionStatus>(row.ItemArray[4]),
                        Amount = DB.ConvertFromDBVal<float>(row.ItemArray[5]),
                        Date = DB.ConvertFromDBVal<DateTime>(row.ItemArray[6]),
                        Sender = DB.ConvertFromDBVal<string>(row.ItemArray[7])
                    };

                    transactions.Add(transaction);
                }
                DB.Table.Clear();
                return transactions;
            }

            return null;
        }
    }
}
