using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BankApp.Libs
{
    /// <summary>
    /// Статический класс для работы с настройками
    /// </summary>
    public static class LibSettings
    {
        // Путь к файлу настроек
        private static readonly string settingsPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/BankApp/Data/Settings.xml";

        // Шаблон настроек
        private static List<Setting> settingsTemplate = new List<Setting>
        {
            new Setting { Key = "MainBill", Name = "Основной счёт"},
            new Setting { Key = "SeeAllBills", Name = "Показать все счета", Value = "false" },
        };

        /// <summary>
        /// Статический метод. Возвращает список настроек из файла
        /// </summary>
        /// <returns>Список настроек</returns>
        public static List<Setting> ReadSettings()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Setting>));

            List<Setting> settings = new List<Setting>();

            try
            {
                using (FileStream fs = new FileStream(Path.GetFullPath(settingsPath), FileMode.OpenOrCreate))
                {
                    settings = (List<Setting>)xmlSerializer.Deserialize(fs);
                    fs.Close();
                }
            }
            catch
            {
                using (FileStream fs = new FileStream(Path.GetFullPath(settingsPath), FileMode.OpenOrCreate))
                {
                    xmlSerializer.Serialize(fs, settingsTemplate);
                    fs.Close();
                }
                return settingsTemplate;
            }

            return settings;
        }

        /// <summary>
        /// Статический метод. Записывает настройки в файл
        /// </summary>
        /// <param name="settings">Настройки</param>
        /// <returns>Результат записи</returns>
        public static bool WriteSettings(List<Setting> settings)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Setting>));

            try
            {
                using (FileStream fs = new FileStream(Path.GetFullPath(settingsPath), FileMode.OpenOrCreate))
                {
                    xmlSerializer.Serialize(fs, settings);
                    fs.Close();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Статический метод. Возвращает настройку по ключу
        /// </summary>
        /// <param name="setting">Ключевое значение настройки</param>
        /// <returns>Настройка</returns>
        public static Setting? GetSetting(Settings setting)
        {
            List<Setting> settings = ReadSettings();
            for (int i = 0; i < settings.Count; i++)
            {
                if (settings[i].Key == setting.ToString())
                    return settings[i];
            }

            return null;
        }

        /// <summary>
        /// Класс для хранения информации о настройках
        /// </summary>
        public class Setting
        {
            public string Key { get; set; }
            public string Name { get; set; }
            public string? Value { get; set; }
            public List<Setting> Subsettings = new List<Setting>();
        }

        public enum Settings
        {
            MainBill,
            SeeAllBills
        }
    }
}
