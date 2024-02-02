using BankApp.Libs;
using BankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static BankApp.Models.User;

namespace BankApp.UI.Common.Winds
{
    /// <summary>
    /// Логика взаимодействия для WndRegistration.xaml
    /// </summary>
    public partial class WndRegistration : Window
    {
        public List<TextBlock> ErrorBlocks;
        public WndRegistration()
        {
            InitializeComponent();
            List<TextBlock> errorBlocks = new List<TextBlock>
            {
                TblFNameError,
                TblLNameError,
                TblMNameError,
                TblLoginError,
                TblPasswordError,
                TblPhoneError
            };

            ErrorBlocks = errorBlocks;
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (!validate())
            {

                return;
            }
            User user = new User
            {
                FName = TbFName.Text,
                LName = TbLName.Text,
                MName = TbMName.Text,
                Login = TbLogin.Text,
                Password = PwbxPassword.Password,
                Phone = TbPhone.Text,
                Role = UserRole.Клиент,
                Status = UserStatus.Активен
            };



            if (LibUser.AddUser(user))
            {
                MessageBox.Show("Вы успешно зарегистрировались!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            else
            {
                MessageBox.Show("Во время регистрации произошла ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                printErrors(user);
            }
        }

        private void printErrors(User user)
        {
            for (int i = 0; i < ErrorBlocks.Count; i++)
                ErrorBlocks[i].Text = string.Empty;

            foreach (ModelError error in user.Errors)
            {
                for (int i = 0; i < ErrorBlocks.Count; i++)
                {
                    if (ErrorBlocks[i].Tag.Equals(error.FieldName))
                    {
                        ErrorBlocks[i].Text = error.Description + "\n";
                        break;
                    }
                }
            }
        }

        private bool validate()
        {
            return PwbxPassword.Password == PwbxPassword.Password;
        }

        private void TbLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LibUser.IsLoginExists(TbLogin.Text))
                TblLoginError.Text = "Такой логин уже существует!";
            else
                TblLoginError.Text = string.Empty;
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PwbxPassword.Password != PwbxConfirmPassword.Password)
                TblConfirmPasswordError.Text = "Пароли не совпадают!";
            else
                TblConfirmPasswordError.Text = string.Empty;
        }

        private void TbPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LibUser.IsPhoneBusy(TbPhone.Text))
                TblPhoneError.Text = "Телефон занят!";
            else
                TblPhoneError.Text = string.Empty;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Owner.Show();
        }
    }
}
