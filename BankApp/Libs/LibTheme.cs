using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Libs
{
    public static class LibTheme
    {
        public static Theme CurrentTheme
        {
            get
            {
                return getCurrentTheme();
            }
        }
        private static Theme getCurrentTheme()
        {
            int isLight = (int)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", "SystemUsesLightTheme", null);
            if(isLight == 1)
                return Theme.Light;
            else
                return Theme.Dark;
        }

        public enum Theme
        {
            Light,
            Dark
        }
    }
}
