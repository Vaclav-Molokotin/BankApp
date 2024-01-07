using BankApp.Libs;
using BankApp.UI.Client.Windows;
using BankApp.UI.Common.Winds;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BankApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WndAuthorization : Window
    {
        public WndAuthorization()
        {
            InitializeComponent();
        }

        private void btnAuthorize_Click(object sender, RoutedEventArgs e)
        {
            if (authorize())
            {                
                WndHome wnd = new WndHome();
                wnd.Owner = this;
                wnd.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Неверно введён логин или пароль, попробуйте снова", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool authorize()
        {
            bool result = false;
            if(chbxShowPassword.IsChecked == true)
               result = LibUser.FindUserByLogopas(tbLogin.Text, tbPassword.Text);
            else
                result = LibUser.FindUserByLogopas(tbLogin.Text, pwbxPassword.Password);
            return result;
        }

        private void chbxShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if(chbxShowPassword.IsChecked == true)
            {
                tbPassword.Text = pwbxPassword.Password;
                pwbxPassword.Visibility = Visibility.Collapsed;
                tbPassword.Visibility = Visibility.Visible;
            }
            else
            {
                pwbxPassword.Password = tbPassword.Text;
                pwbxPassword.Visibility = Visibility.Visible;
                tbPassword.Visibility = Visibility.Collapsed;
            }
        }

        private void Password_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key.ToString() == "Return")
            {
                if (authorize())
                {
                    WndHome wnd = new WndHome();
                    wnd.Owner = this;
                    wnd.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Неверно введён логин или пароль, попробуйте снова", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            WndRegistration wnd = new WndRegistration();
            wnd.Show();
            Close();
        }
    }
}
