using System;
using System.IO;
using MySql.Data.MySqlClient;
using SpringHeroBanking.entity;

namespace SpringHeroBanking.model
{
    public class YYAccountModel
    {
        public Boolean Save(YYAccount ac)
        {
            DbConnection.Instance().OpenConnection();
            string query = ("insert into `account`" +
                            "(accountNumber,userName,password,salt,balance,identityCard,fullName,email,phoneNumber,address," +
                            "dob,gender,status) values" +
                            "(@accountNumber,@userName,@password,@salt,@balance,@identityCard,@fullName,@email,@phoneNumber,@address," +
                            "@dob,@gender,@status)");
            MySqlCommand cmd = new MySqlCommand(query, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@accountNumber", ac.AccountNumber);
            cmd.Parameters.AddWithValue("@userName", ac.UserName);
            cmd.Parameters.AddWithValue("@password", ac.Password);
            cmd.Parameters.AddWithValue("@salt", ac.Salt);
            cmd.Parameters.AddWithValue("@balance", ac.Balance);
            cmd.Parameters.AddWithValue("@identityCard", ac.IdentityCard);
            cmd.Parameters.AddWithValue("@fullName", ac.FullName);
            cmd.Parameters.AddWithValue("@email", ac.Email);
            cmd.Parameters.AddWithValue("@phoneNumber", ac.PhoneNumber);
            cmd.Parameters.AddWithValue("@address", ac.Address);
            cmd.Parameters.AddWithValue("@dob", ac.Dob);
            cmd.Parameters.AddWithValue("@gender", ac.Gender);
            cmd.Parameters.AddWithValue("@status", ac.Status);
            cmd.ExecuteNonQuery();
            DbConnection.Instance().CloseConnection();
            return true;
        }

        public Boolean CheckExistUserName(string username)
        {
            DbConnection.Instance().OpenConnection();
            var queryString = "select * from account where userName = @username";
            var cmd = new MySqlCommand(queryString, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@username", username);
            var reader = cmd.ExecuteReader();
            var isExist = reader.Read();
            DbConnection.Instance().CloseConnection();
            return isExist;
        }

        public YYAccount GetByAccountNumber(string anumber)
        {
            DbConnection.Instance().OpenConnection();
            var queryString = "select * from account where accountNumber = @accounnumber";
            var cmd = new MySqlCommand(queryString, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@accounnumber", anumber);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                YYAccount ac = new YYAccount();
                ac.AccountNumber = reader.GetString(reader.GetOrdinal("accountNumber"));
                ac.UserName = reader.GetString(reader.GetOrdinal("userName"));
                ac.Salt = reader.GetString(reader.GetOrdinal("salt"));
                ac.Balance = reader.GetDecimal(reader.GetOrdinal("balance"));
                ac.IdentityCard = reader.GetString(reader.GetOrdinal("identityCard"));
                ac.FullName = reader.GetString(reader.GetOrdinal("fullName"));
                ac.Email = reader.GetString(reader.GetOrdinal("email"));
                ac.PhoneNumber = reader.GetString(reader.GetOrdinal("phoneNumber"));
                ac.Address = reader.GetString(reader.GetOrdinal("address"));
                ac.Gender = reader.GetInt16(reader.GetOrdinal("gender"));
                ac.Status = reader.GetInt16(reader.GetOrdinal("status"));
                DbConnection.Instance().CloseConnection();
                return ac;
            }
            DbConnection.Instance().CloseConnection();
            return null;
        }

        public YYAccount GetByLogin(string accountnumber, string password)
        {
            DbConnection.Instance().OpenConnection();
            var queryString = "select * from account where accountNumber = @accountnumber and password = @password";
            var cmd = new MySqlCommand(queryString, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@accountnumber", accountnumber);
            cmd.Parameters.AddWithValue("@password", password);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                YYAccount ac = new YYAccount();
                String anumber = reader.GetString("accountNumber");
                String pword = reader.GetString("password");
                String salt = reader.GetString("salt");
                return ac;
            }
            DbConnection.Instance().CloseConnection();
            return null;
        }

        public void DepositBalance(decimal uamount, String anumber)
        {
            DbConnection.Instance().OpenConnection();
            var updateBalance = "update account set balance = @balance + balance, updatedAt = CURRENT_TIMESTAMP where accountNumber = @accountNumber";
            var cmd = new MySqlCommand(updateBalance, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@balance", uamount);
            cmd.Parameters.AddWithValue("@accountNumber", anumber);
            cmd.ExecuteNonQuery();
            DbConnection.Instance().CloseConnection();
        }
        
        public void WithdrawalBalance(decimal uamount, String anumber)
        {
            DbConnection.Instance().OpenConnection();
            var updateBalance = "update account set balance = balance - @balance, updatedAt = CURRENT_TIMESTAMP where accountNumber = @accountNumber";
            var cmd = new MySqlCommand(updateBalance, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@balance", uamount);
            cmd.Parameters.AddWithValue("@accountNumber", anumber);
            cmd.ExecuteNonQuery();
            DbConnection.Instance().CloseConnection();
        }

        public void uAUserName(string anumber, string username)
        {
            DbConnection.Instance().OpenConnection();
            var accountLock = "update account set userName = @username, updatedAt = CURRENT_TIMESTAMP where accountNumber = @accountNumber";
            var cmd = new MySqlCommand(accountLock, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@accountNumber", anumber);
            cmd.ExecuteNonQuery();
            DbConnection.Instance().CloseConnection();
        }
        public void uAPassword(string anumber, string password)
        {
            DbConnection.Instance().OpenConnection();
            var accountLock = "update account set password = @password, updatedAt = CURRENT_TIMESTAMP where accountNumber = @accountNumber";
            var cmd = new MySqlCommand(accountLock, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@accountNumber", anumber);
            cmd.ExecuteNonQuery();
            DbConnection.Instance().CloseConnection();
        }
        public void uAIdentityCard(string anumber, string identityCard)
        {
            DbConnection.Instance().OpenConnection();
            var accountLock = "update account set identityCard = @identityCard, updatedAt = CURRENT_TIMESTAMP where accountNumber = @accountNumber";
            var cmd = new MySqlCommand(accountLock, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@identityCard", identityCard);
            cmd.Parameters.AddWithValue("@accountNumber", anumber);
            cmd.ExecuteNonQuery();
            DbConnection.Instance().CloseConnection();
        }
        public void uAFullName(string anumber, string fullName)
        {
            DbConnection.Instance().OpenConnection();
            var accountLock = "update account set fullName = @fullName, updatedAt = CURRENT_TIMESTAMP where accountNumber = @accountNumber";
            var cmd = new MySqlCommand(accountLock, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@fullName", fullName);
            cmd.Parameters.AddWithValue("@accountNumber", anumber);
            cmd.ExecuteNonQuery();
            DbConnection.Instance().CloseConnection();
        }
        public void uAEmail(string anumber, string email)
        {
            DbConnection.Instance().OpenConnection();
            var accountLock = "update account set email = @email, updatedAt = CURRENT_TIMESTAMP where accountNumber = @accountNumber";
            var cmd = new MySqlCommand(accountLock, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@accountNumber", anumber);
            cmd.ExecuteNonQuery();
            DbConnection.Instance().CloseConnection();
        }
        public void uAPhoneNumber(string anumber, string phoneNumber)
        {
            DbConnection.Instance().OpenConnection();
            var accountLock = "update account set phoneNumber = @phoneNumber, updatedAt = CURRENT_TIMESTAMP where accountNumber = @accountNumber";
            var cmd = new MySqlCommand(accountLock, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
            cmd.Parameters.AddWithValue("@accountNumber", anumber);
            cmd.ExecuteNonQuery();
            DbConnection.Instance().CloseConnection();
        }
        public void uAAddress(string anumber, string address)
        {
            DbConnection.Instance().OpenConnection();
            var accountLock = "update account set address = @address, updatedAt = CURRENT_TIMESTAMP where accountNumber = @accountNumber";
            var cmd = new MySqlCommand(accountLock, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@accountNumber", anumber);
            cmd.ExecuteNonQuery();
            DbConnection.Instance().CloseConnection();
        }
        public void uADob(string anumber, string dob)
        {
            DbConnection.Instance().OpenConnection();
            var accountLock = "update account set dob = @dob, updatedAt = CURRENT_TIMESTAMP where accountNumber = @accountNumber";
            var cmd = new MySqlCommand(accountLock, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@dob", dob);
            cmd.Parameters.AddWithValue("@accountNumber", anumber);
            cmd.ExecuteNonQuery();
            DbConnection.Instance().CloseConnection();
        }
        public void uAGender(string anumber, string gender)
        {
            DbConnection.Instance().OpenConnection();
            var accountLock = "update account set gender = @gender, updatedAt = CURRENT_TIMESTAMP where accountNumber = @accountNumber";
            var cmd = new MySqlCommand(accountLock, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@accountNumber", anumber);
            cmd.ExecuteNonQuery();
            DbConnection.Instance().CloseConnection();
        }
        public void AccountLock(String anumber)
        {
            DbConnection.Instance().OpenConnection();
            var accountLock = "update account set status = 0 where accountNumber = @accountNumber";
            var cmd = new MySqlCommand(accountLock, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@accountNumber", anumber);
            cmd.ExecuteNonQuery();
            DbConnection.Instance().CloseConnection();
        }
    }
}