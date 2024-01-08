using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models
{
    /// <summary>
    /// Класс модели счёта
    /// </summary>
    public class Bill
    {
        /// <summary>
        /// Номер счёта
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// Баланс счёта
        /// </summary>
        public float Balance { get; set; }
        /// <summary>
        /// ID владельца счёта
        /// </summary>
        public uint OwnerID { get; set; }
        /// <summary>
        /// Номер привязанной карты
        /// </summary>
        public string? CardNumber { get; set; }
        /// <summary>
        /// Статус карты
        /// </summary>
        public bool IsFrozen { get; set; }
    }
}
