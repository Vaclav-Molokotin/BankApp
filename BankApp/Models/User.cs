using BankApp.Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FName
        {
            get { return fName; }
            set
            {
                value = value.Trim();
                fName = value;

                List<ModelError> errors = new List<ModelError>();

                if (value.Length > 75)
                    errors.Add(new ModelError { FieldName = "FName", Description = "Длина поля не может превышать 75 символов!" });
                if (value == string.Empty)
                    errors.Add(new ModelError { FieldName = "FName", Description = "Поле должно быть заполнено!" });
                else
                {
                    if (!Regex.Match(value, "^[а-яА-Я]*$").Success)
                        errors.Add(new ModelError { FieldName = "FName", Description = "Введите имя на русском!" });
                }

                if (errors.Count > 0)
                    addErrors(errors);
                else
                    deleteErrorsByField("FName");
            }
        }
        public string LName
        {
            get
            {
                return lName;
            }
            set
            {
                value = value.Trim();
                lName = value;

                List<ModelError> errors = new List<ModelError>();

                if (value.Length > 75)
                    errors.Add(new ModelError { FieldName = "LName", Description = "Длина поля не может превышать 75 символов!" });
                if (value == string.Empty)
                    errors.Add(new ModelError { FieldName = "LName", Description = "Поле должно быть заполнено!" });
                else
                {
                    if (!Regex.Match(value, "^[а-яА-Я]*$").Success)
                        errors.Add(new ModelError { FieldName = "LName", Description = "Введите фамилию на русском!" });
                }

                if (errors.Count > 0)
                    addErrors(errors);
                else
                    deleteErrorsByField("LName");
            }
        }
        public string MName
        {
            get
            {
                return mName;
            }
            set
            {
                value = value.Trim();
                mName = value;

                List<ModelError> errors = new List<ModelError>();

                if (value.Length > 75)
                    errors.Add(new ModelError { FieldName = "MName", Description = "Длина поля не может превышать 75 символов!" });
                if (value == string.Empty)
                    errors.Add(new ModelError { FieldName = "MName", Description = "Поле должно быть заполнено!" });
                else
                {
                    if (!Regex.Match(value, "^[а-яА-Я]*$").Success)
                        errors.Add(new ModelError { FieldName = "MName", Description = "Введите отчество на русском!" });
                }

                if (errors.Count > 0)
                    addErrors(errors);
                else
                    deleteErrorsByField("MName");
            }
        }
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                value = value.Trim();
                login = value;

                List<ModelError> errors = new List<ModelError>();

                if (value.Length > 75)
                    errors.Add(new ModelError { FieldName = "Login", Description = "Длина поля не может превышать 50 символов!" });
                if (value == string.Empty)
                    errors.Add(new ModelError { FieldName = "Login", Description = "Поле должно быть заполнено!" });
                else
                {
                    if (!Regex.Match(value, "^[a-zA-Z]*$").Success)
                        errors.Add(new ModelError { FieldName = "Login", Description = "Введите логин на английском!" });
                }
                if (LibUser.IsLoginExists(value))
                    errors.Add(new ModelError { FieldName = "Login", Description = "Такой логин уже существует!" });



                if (errors.Count > 0)
                    addErrors(errors);
                else
                    deleteErrorsByField("Login");
            }
        }
        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;

                List<ModelError> errors = new List<ModelError>();

                if (value.Length < 5)
                    errors.Add(new ModelError { FieldName = "Password", Description = "Длина пароля должна быть не менее 5 символов!" });
                if (value.Length > 100)
                    errors.Add(new ModelError { FieldName = "Password", Description = "Длина пароля должна быть не более 100 символов!" });
            }
        }
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;

                List<ModelError> errors = new List<ModelError>();

                if (value == string.Empty)
                    errors.Add(new ModelError { FieldName = "Phone", Description = "Поле должно быть заполнено!" });
                else
                {
                    if (!Regex.Match(value, "^[+][0-9][-][0-9]{3}[-][0-9]{3}[-][0-9]{2}[-][0-9]{2}$").Success)
                        errors.Add(new ModelError { FieldName = "Phone", Description = "Введите номер в формате +7-918-111-22-33!" });
                }
                if (LibUser.IsPhoneBusy(value))
                    errors.Add(new ModelError { FieldName = "Phone", Description = "Номер занят!" });

                addErrors(errors);
            }
        }
        public int RoleID { get; set; }


        private string fName;
        private string lName;
        private string mName;
        private string login;
        private string password;
        private string phone;



        public List<ModelError> Errors { get; set; } = new();

        private void addErrors(List<ModelError> errors)
        {
            foreach (ModelError error in errors)
            {
                Errors.Add(error);
            }
        }

        private void deleteErrorsByField(string fieldName)
        {
            for (int i = 0; i < Errors.Count; i++)
            {
                if (Errors[i].FieldName == fieldName)
                    Errors.RemoveAt(i);
            }
        }

    }
}
