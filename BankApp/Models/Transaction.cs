using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models
{
    public class Transaction
    {
        public uint Id { get; set; }
        public TransactionType Type { get; set; }
        public string BillToNumber { get; set; }
        public string BillFromNumber { get; set; }
        public TransactionStatus Status { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public string Sender { get; set; }
    }

    /// <summary>
    /// Перечисление типов транзакций
    /// </summary>
    public enum TransactionType
    {
        Перевод = 1,
        ПереводМеждуСчетами = 2,
        Снятие = 3,
        Пополнение = 4
    }

    /// <summary>
    /// Перечисление статусов транзакций
    /// </summary>
    public enum TransactionStatus
    {
        Отправлен = 1,
        Доставлен = 3,
        Отменён = 2
    }
}
