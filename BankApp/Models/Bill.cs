using BankApp.Libs;
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
        public string Owner { get; set; }
        /// <summary>
        /// Номер привязанной карты
        /// </summary>
        public string? CardNumber { get; set; }
        /// <summary>
        /// Название счёта
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Статус счёта
        /// </summary>
        public BillStatus Status { get; set; }


        public Card Card
        {
            get
            {
                return LibCard.GetCardByNumber(CardNumber);
            }

            set
            {
                LibBill.BindCard(value, this);
                CardNumber = value.Number;
            }
        }


    }
    public enum BillStatus
    {
        Заморожен = 1,
        Активен = 2,
        Закрыт = 3
    }
}
