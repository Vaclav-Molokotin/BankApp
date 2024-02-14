using BankApp.Libs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using static BankApp.Libs.LibTheme;

namespace BankApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string lightIconPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\BankApp\\Resources\\Assets\\LogoWhite.ico";
        private static string darkIconPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\BankApp\\Resources\\Assets\\Logo.ico";
        public static string IconPath
        {
            get
            {
                var theme = CurrentTheme;
                switch (theme)
                {
                    case Theme.Light:
                        return darkIconPath;
                    case Theme.Dark:
                        return lightIconPath;
                    default:
                        return lightIconPath;
                }
            }
        }
    }
}
