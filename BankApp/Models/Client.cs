using BankApp.Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models
{
    public class Client : User
    {
        /// <summary>
        /// Список счетов клиента
        /// </summary>
        public List<Bill> Bills
        {
            get
            {
                return LibClient.GetBillsByClient(this);
            }
        }

        /// <summary>
        /// Конструктор. Создаёт клиента на основе пользователя
        /// </summary>
        /// <param name="user"></param>
        public Client(User user)
        {
            Id = user.Id;
            FName = user.FName;
            LName = user.LName;
            MName = user.MName;
            Login = user.Login;
            Password = user.Password;
            Phone = user.Phone;
        }
    }
}
