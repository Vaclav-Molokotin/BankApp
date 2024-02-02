using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models
{
    /// <summary>
    /// Класс модели карты
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Номер карты
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// CVC-код
        /// </summary>
        public string CVC { get; set; }
        /// <summary>
        /// Дата начала действия карты
        /// </summary>
        public DateTime DateTo { get; set; }
        /// <summary>
        /// Дата окончания действия карты
        /// </summary>
        public DateTime DateFrom { get; set; }
        /// <summary>
        /// Статус карты
        /// </summary>
        public CardStatus Status { get; set; }
    }

    /// <summary>
    /// Перечисление статусов карты
    /// </summary>
    public enum CardStatus
    {
        Активна = 1,
        Заморожена = 2,
        Заблокирована = 3
    }
}
