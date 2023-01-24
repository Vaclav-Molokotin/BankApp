using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BankApp.Libs
{
    public class Bill
    {
        public string Number { get; set; }
        public float Balance { get; set; }

        public Bill(string number, string balance)
        {
            Number = number;
            Balance = Single.Parse(balance);
        }

        static public Bill FindBillByNumber(string number)
        {
            
            StreamReader streamReader = new StreamReader(@"..\..\..\Data\Accounts.txt", true);
            while (!streamReader.EndOfStream)
            {
                if (streamReader.ReadLine() == number)
                {
                    string balance = streamReader.ReadLine();
                    return new Bill(number, balance);
                }
            }
            return null;
        }
    }
}
