using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BankApp.Libs
{
    public static class Transaction
    {
        public static void WriteTransaction(Client client, string senderBillNumber, string benefitBillNumber, TypesOfTransaction type, float amount, DateTime dateTime)
        {
            StreamWriter strmWriter = new StreamWriter("../../../Data/Transactions.txt", true);
            strmWriter.WriteLine($"{client.Login}\n{senderBillNumber}\n{benefitBillNumber}\n{type}\n{amount.ToString()}\n{dateTime.ToString()};\n");
            strmWriter.Close();
        }

        public static List<string[]> ReadTransactionsByClient(Client client)
        {
            List<String[]> transactions = new List<string[]>();
            StreamReader strmReader = new StreamReader("../../../Data/Transactions.txt");
            byte counter = 0;
            while (!strmReader.EndOfStream && counter != 100)
            {
                if (strmReader.ReadLine() == client.Login)
                {
                    transactions.Add(new string[5]);
                    for (int i = 0; i < 5; i++)
                    {
                        transactions[counter][i] = strmReader.ReadLine();
                    }
                    counter++;
                }
            }
            return transactions;
        }
    }
    public enum TypesOfTransaction
    {
        Refill,
        Transfer,
        Withdrawal
    }
}
