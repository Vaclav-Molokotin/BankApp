using BankApp.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Libs
{
    public static class LibUser
    {

        public static bool FindUserByLogopas(string login, string password)
        {
            DB.OpenConnection();
            DB.Command.CommandText = "SELECT * FROM `user` WHERE `login` = @uL AND `password` = MD5(@uP)";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = login;
            DB.Command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = password;


            DB.Adapter.SelectCommand = DB.Command;
            DB.Adapter.Fill(DB.Table);

            DB.CloseConnection();

            if (DB.Table.Rows.Count > 0)
                return true;

            return false;
        }

        public static bool CreateUser(User user)
        {             

            if (user.Errors.Count > 0)
                return false;

            DB.OpenConnection();

            DB.Command.CommandText = "INSERT INTO user (FName, LName, MName, Login, Password, Phone, RoleID) VALUES (@FName, @LName, @MName, @Login, MD5(@Password), @Phone, @RoleID);";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@FName", MySqlDbType.VarChar).Value = user.FName;
            DB.Command.Parameters.Add("@LName", MySqlDbType.VarChar).Value = user.LName;
            DB.Command.Parameters.Add("@MName", MySqlDbType.VarChar).Value = user.MName;
            DB.Command.Parameters.Add("@Login", MySqlDbType.VarChar).Value = user.Login;
            DB.Command.Parameters.Add("@Password", MySqlDbType.VarChar).Value = user.Password;
            DB.Command.Parameters.Add("@Phone", MySqlDbType.VarChar).Value = user.Phone;
            DB.Command.Parameters.Add("@RoleID", MySqlDbType.VarChar).Value = user.RoleID;

            int rows = DB.Command.ExecuteNonQuery();

            DB.CloseConnection();

            if (rows == 0)
            {
                return false;
            }

            return true;
        }

        public static bool IsLoginExists(string login)
        {
            DB.OpenConnection();
            DB.Command.CommandText = "SELECT * FROM `user` WHERE `login` = @uL";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = login;

            DB.Adapter.SelectCommand = DB.Command;
            DB.Adapter.Fill(DB.Table);

            int rowsCount = DB.Table.Rows.Count;

            DB.CloseConnection();

            if (rowsCount > 0)
                return true;

            return false;
        }

        public static bool IsPhoneBusy(string phone)
        {
            DB.OpenConnection();
            DB.Command.CommandText = "SELECT * FROM `user` WHERE `phone` = @uP";
            DB.Command.Connection = DB.GetConnection();
            DB.Command.Parameters.Clear();

            DB.Command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = phone;

            DB.Adapter.SelectCommand = DB.Command;
            DB.Adapter.Fill(DB.Table);

            int rowsCount = DB.Table.Rows.Count;

            DB.CloseConnection();

            if (rowsCount > 0)
                return true;

            return false;
        }
    }
}
