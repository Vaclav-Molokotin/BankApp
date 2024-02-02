using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.ViewModels
{
    public class TransactionTransferView
    {
        public string Type {
            get
            {
                return type;                
            }

            set
            {
                type = value;
                for (int i = 1; i < type.Length; i++)
                {
                    if (type[i].ToString().Any(char.IsUpper))
                    {
                        type = type.Insert(i, " ");
                        i++;
                        string buf = type[i].ToString().ToLower();
                        type = type.Insert(i + 1, buf);
                        type = type.Remove(i, 1);
                    }
                }
            }
        }
        public string BillFromNumber { get; set; }
        public string BillToNumber { get; set; }
        public string Recipient { get; set; }
        public float Amount { get; set; }
        public uint TransactionID { get;
            set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        private string type;
    }
}
