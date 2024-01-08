using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models
{
    /// <summary>
    /// Класс модели ролей пользователей
    /// </summary>
    public class Role
    {
        /// <summary>
        /// ID роли
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название роли
        /// </summary>
        public string Name { get; set; }

    }
}
