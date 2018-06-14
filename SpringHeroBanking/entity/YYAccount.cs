using System;
using System.Security.Cryptography;

namespace SpringHeroBanking.entity
{
    public class YYAccount
    {
        private string _accountNumber;
        private string _userName;
        private string _password;
        private string _salt;
        private decimal _balance;
        private string _identityCard;
        private string _fullName;
        private string _email;
        private string _phoneNumber;
        private string _address;
        private string _dob;
        private int _gender;
        private string _createdAt;
        private string _updatedAt;
        private int _status;

        public YYAccount()
        {
           
        }

//        public Boolean CheckEncryptedPassWord(string password)
//        {
//            var checkPassword = EncryptString(password, _salt);
//            return checkPassword == _password;
//        }

        public void EncryptString()
        {
            if (string.IsNullOrEmpty(_password))
            {
                throw new ArgumentException("Password is null or empyt");
            }
            
            var str_md5 = "";
            
            byte[] byteArrayPasswordSalt = System.Text.Encoding.UTF8.GetBytes(_password + _salt);
            MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byteArrayPasswordSalt = md5CryptoServiceProvider.ComputeHash(byteArrayPasswordSalt);
            foreach (var b in byteArrayPasswordSalt)
            {
                str_md5 += b.ToString("X2");
            }

            _password = str_md5;
        }
        
        public string EncryptLoginPass(String password, String salt)
        {
            var str_md5 = "";
            
            byte[] byteArrayPasswordSalt = System.Text.Encoding.UTF8.GetBytes(password + salt);
            MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byteArrayPasswordSalt = md5CryptoServiceProvider.ComputeHash(byteArrayPasswordSalt);
            foreach (var b in byteArrayPasswordSalt)
            {
                str_md5 += b.ToString("X2");
            }
            return str_md5;
        }

//        public void EncryPassword()
//        {
//            if (string.IsNullOrEmpty(_password))
//            {
//                throw new ArgumentException("Password is null or empyt");
//            }
//
//            _password = EncryptString();
//        }

//        private void GennerateAccountNumber()
//        {
//            _accountNumber = Int32.Parse(Guid.NewGuid().ToString());
//        }

        private void GenerateSalt()
        {
            _salt = Guid.NewGuid().ToString().Substring(0, 7);
        }

        public YYAccount(string userName, string password, decimal balance, string identityCard, string fullName,
            string email, string phoneNumber, string address, string dob, int gender, int status)
        {
            _userName = userName;
            _password = password;
            _balance = balance;
            _identityCard = identityCard;
            _fullName = fullName;
            _email = email;
            _phoneNumber = phoneNumber;
            _address = address;
            _dob = dob;
            _gender = gender;
            _status = status;
            _accountNumber = Guid.NewGuid().ToString();
            _salt = Guid.NewGuid().ToString().Substring(0, 7);
        }

        public string Salt
        {
            get => _salt;
            set => _salt = value;
        }

        public string AccountNumber
        {
            get => _accountNumber;
            set => _accountNumber = value;
        }

        public string UserName
        {
            get => _userName;
            set => _userName = value;
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public decimal Balance
        {
            get => _balance;
            set => _balance = value;
        }

        public string IdentityCard
        {
            get => _identityCard;
            set => _identityCard = value;
        }

        public string FullName
        {
            get => _fullName;
            set => _fullName = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        public string Address
        {
            get => _address;
            set => _address = value;
        }

        public string Dob
        {
            get => _dob;
            set => _dob = value;
        }

        public int Gender
        {
            get => _gender;
            set => _gender = value;
        }

        public string CreatedAt
        {
            get => _createdAt;
            set => _createdAt = value;
        }

        public string UpdatedAt
        {
            get => _updatedAt;
            set => _updatedAt = value;
        }

        public int Status
        {
            get => _status;
            set => _status = value;
        }

        public string Gender1()
        {
            string result = "";
            if (Gender == 0)
            {
                result = "Male";
            }
            else if (Gender == 1)
            {
                result = "Female";
            }
            else if (Gender == 2)
            {
                result = "Rather not say.";
            }

            return result;
        }

        public string Status1()
        {
            string result = "";
            if (Status == 0)
            {
                result = "Locked";
            }
            else if (Status == 1)
            {
                result = "Active";
            }
            else if (Status == 2)
            {
                result = "Inctive";
            }

            return result;
        }
    }
}