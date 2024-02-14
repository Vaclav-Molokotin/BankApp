using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BankApp.Libs
{
    public static class LibImage
    {
        private static string defaultPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\BankApp\\Resources\\Default.png";

        public static BitmapSource GetImageSource(string imagePath)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            try
            {
                bitmap.UriSource = new Uri(imagePath);
                bitmap.EndInit();  
            }
            catch 
            {
                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(defaultPath);
                bitmap.EndInit();
            }

            return bitmap;
        }
    }
}
