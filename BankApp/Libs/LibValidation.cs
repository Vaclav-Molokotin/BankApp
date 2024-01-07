using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Libs
{
    /// <summary>
    /// Структура для хранения информации об ошибках валидации моделей
    /// </summary>
    public struct ModelError
    {
        /// <summary>
        /// Имя поля
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// Описание ошибки
        /// </summary>
        public string Description { get; set; }
    }
}
