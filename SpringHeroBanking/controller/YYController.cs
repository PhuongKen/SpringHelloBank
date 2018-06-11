using System;
using System.Collections.Generic;
using SpringHeroBanking.entity;
using SpringHeroBanking.model;

namespace SpringHeroBanking.controller
{
    public  class YYController
    {
        YYAccountModel _model = new YYAccountModel();
        YYTransactionHistoryModel _history = new YYTransactionHistoryModel();
        YYTransactionModel _tras = new YYTransactionModel();
        YYChangeHistoryModel _change = new YYChangeHistoryModel();

        public void Login()
        {
            Console.WriteLine("Please enter Acount number: ");
            string anumber = Console.ReadLine();
            Console.WriteLine("Please enter password: ");
            string pword = Console.ReadLine();
            var acount = _model.GetByLogin(anumber, pword);
            if (acount == null)
            {
                Console.WriteLine("Username or password false!");
            }
            else
            {
                {
                    while (true)
                    {
                        Console.WriteLine("-----------Welcome to SHB--------------");
                        Console.WriteLine("1, Account Information.");
                        Console.WriteLine("2, The amount in the account");
                        Console.WriteLine("3, Deposit into account.");
                        Console.WriteLine("4, Transfers.");
                        Console.WriteLine("5, Withdrawal");
                        Console.WriteLine("6, Transaction history.");
                        Console.WriteLine("7, Change history.");
                        Console.WriteLine("8, Log out.");
                        Console.WriteLine("---------------------------------------");
                        Console.WriteLine("Please enter choice: ");
                        int choice = Int32.Parse(Console.ReadLine());

                        switch (choice)
                        {
                            case 1:
                                DbConnection.Instance().CloseConnection();
                                Console.WriteLine("You choice acount information.");
                                YYAccount ac = new YYAccount();
                                var infoAccount = _model.GetByAccountNumber(anumber);
                                Console.WriteLine("Account Number: " + infoAccount.AccountNumber +
                                                  ", User Name: " + infoAccount.UserName +
                                                  ", Balance: " + infoAccount.Balance +
                                                  ", Identity Card: " + infoAccount.IdentityCard +
                                                  ", Full Name: " + infoAccount.FullName +
                                                  "\n, Email: " + infoAccount.Email +
                                                  ", Phone Number: " + infoAccount.PhoneNumber +
                                                  ", Address: " + infoAccount.Address +
                                                  ", Gender: " + infoAccount.Gender1()
                                                  + ", Status: " + infoAccount.Status1());
                                Console.WriteLine("Do you want to change information(y/n): ");
                                string conts = Console.ReadLine();
                                switch (conts)
                                {
                                case "y":
                                    ChangeAccount(anumber);
                                    break;
                                case "n":
                                    break;
                                }
                                break;
                            case 2:
                                Console.WriteLine("You choice the amount in the account");
                                DbConnection.Instance().CloseConnection();
                                var amount = _model.GetByAccountNumber(anumber);
                                Console.WriteLine("Account balance available is: " + amount.Balance);
                                break;
                            case 3:
                                Console.WriteLine("You choice deposit into account.");
                                DbConnection.Instance().CloseConnection();
                                DepositBalance(anumber);
                                break;
                            case 4:
                                Console.WriteLine("You choice transfers.");
                                DbConnection.Instance().CloseConnection();
                                Trasaction(anumber);
                                break;
                            case 5:
                                Console.WriteLine("You choice withdrawal");
                                DbConnection.Instance().CloseConnection();
                                WithdrawalBalance(anumber);
                                break;
                            case 6:
                                Console.WriteLine("You choice transaction history.");
                                DbConnection.Instance().CloseConnection();
                                Console.WriteLine("You choice transaction history.");
                                Console.WriteLine("{0,-10}{1,-20}{2,-20}{3,-10}{4,-20}{5,-30},{6,-20}","Id","Account Number",
                                    "Type","Amount","","Trading account number","Created Date");
                                List<YYTransactionHistory> list = _history.queryHistory(anumber);
                                foreach (var th in list)
                                {
                                    Console.WriteLine("{0,-10}{1,-20}{2,-20}{3,-10}{4,-20}{5,-30},{6, -20}",
                                        th.Id,th.AccountNumber,th.Type1(),th.Amount,"(VNĐ)",th.TradingAcountNumber1(), th.CreatedDate);
                                }
                                break;
                            case 7:
                                Console.WriteLine("You choice change history.");
                                DbConnection.Instance().CloseConnection();
                                Console.WriteLine("{0,-60}{1,-40}","Account Number", "Content");
                                List<YYChangeHistory> list1 = _change.queryChange(anumber);
                                foreach (var ch in list1)
                                {
                                    Console.WriteLine("{0,-60}{1,-40}",ch.AccountNumber,ch.Content);
                                }
                                break;
                            case 8:
                                Console.WriteLine("Log out.");
                                DbConnection.Instance().CloseConnection();
                                break;
                            default:
                                Console.WriteLine("You enter fails. Please enter again!");
                                break;
                        }

                        if (choice == 8)
                        {
                            break;
                        }
                    }
                }
            }
        }
        
        public YYAccount Account()
        {
            var ac = new YYAccount();
            Console.WriteLine("Please enter Account Number: ");
            ac.AccountNumber = Console.ReadLine();

            Console.WriteLine("Please enter User Name: ");
            ac.UserName = Console.ReadLine();

            if (_model.CheckExistUserName(ac.UserName))
            {
                Console.WriteLine("Duplicate user name.");
            }
            else
            {
                Console.WriteLine("Please enter password: ");
                ac.Password = Console.ReadLine();
                Console.WriteLine("Please enter balance: ");
                ac.Balance = Decimal.Parse(Console.ReadLine());
                Console.WriteLine("Please enter identity card: ");
                ac.IdentityCard = Console.ReadLine();
                Console.WriteLine("Please enter full name: ");
                ac.FullName = Console.ReadLine();
                Console.WriteLine("Please enter email: ");
                ac.Email = Console.ReadLine();
                Console.WriteLine("Please enter phone number: ");
                ac.PhoneNumber = Console.ReadLine();
                Console.WriteLine("Please enter address: ");
                ac.Address = Console.ReadLine();
                Console.WriteLine("Please enter date of birth(Yeah-month-day -)");
                ac.Dob = Console.ReadLine();
                Console.WriteLine("Please enter gender: ");
                ac.Gender = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Please enter status: ");
                ac.Status = Int32.Parse(Console.ReadLine());
                return ac;
            }


            return null;
        }
        
        public void DepositBalance(String anumber)
        {
            YYTransactionHistory h = new YYTransactionHistory();
            Console.WriteLine("Please enter input amount to send account.");
            decimal amount = Decimal.Parse(Console.ReadLine());
            _model.DepositBalance(amount, anumber);
            h.AccountNumber = anumber;
            h.Type = 2;
            h.Amount = amount;
            h.TradingAcountNumber = "1";
            _history.HinsertBalance(h);
            Console.WriteLine("You just load your account with the amount " + amount + " (VNĐ)");
        }

        public void WithdrawalBalance(String anumber)
        {
            int limit = 50000;
            var amountTotal = _model.GetByAccountNumber(anumber);
            if (amountTotal.Balance <= limit)
            {
                Console.WriteLine("The amount in the account must be over 50000 (VNĐ)");
                
            }
            else
            {
                YYTransactionHistory h = new YYTransactionHistory();
                Console.WriteLine("Please enter the amount you need to withdraw");
                decimal amount = Decimal.Parse(Console.ReadLine());
                
                if ((amountTotal.Balance - amount) < limit)
                {
                    Console.WriteLine("The amount you can withdraw is currently: " + (amountTotal.Balance - 60000)
                                                                                   + "\nPlease retype");
                }
                else
                {
                    _model.WithdrawalBalance(amount, anumber);
                    h.AccountNumber = anumber;
                    h.Type = 1;
                    h.Amount = amount;
                    h.TradingAcountNumber = "1";
                    _history.HinsertBalance(h);
                    Console.WriteLine("The amount you have withdrawn is " + amount + " (VNĐ)");
                }
            }
        }

        public void ChangeAccount(String anumber)
        {
            var cacount = _model.GetByAccountNumber(anumber);
            while (true)
            {
                Console.WriteLine("---------------Change Information-----------------");
                Console.WriteLine("1, Change username.");
                Console.WriteLine("2, Change password.");
                Console.WriteLine("3, Change identity card.");
                Console.WriteLine("4, Change full name.");
                Console.WriteLine("5, Change email.");
                Console.WriteLine("6, Change phone number.");
                Console.WriteLine("7, Change address.");
                Console.WriteLine("8, Change dob.");
                Console.WriteLine("9, Change gender.");
                Console.WriteLine("10, Lock account");
                Console.WriteLine("11, Exit!");
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Please enter choice: ");
                int choice = Int32.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("You choice change username.");
                        Console.WriteLine("Please enter username.");
                        string cusername = Console.ReadLine();
                        _model.uAUserName(anumber, cusername);
                        string name1 = cacount.UserName;
                        string contentusername = "Change username from " + name1 + " to " + cusername;
                        _change.InsertChange(anumber,contentusername);
                        break;
                    case 2:
                        Console.WriteLine("You choice change password.");
                        Console.WriteLine("Please enter password.");
                        string cpassword = Console.ReadLine();
                        _model.uAPassword(anumber, cpassword);
                        string password1 = cacount.Password;
                        string contentpassword = "Change password from " + password1 + " to " + cpassword;
                        _change.InsertChange(anumber,contentpassword);
                        break;
                    case 3:
                        Console.WriteLine("You choice change identity card.");
                        Console.WriteLine("Please enter identity card.");
                        string cidentitycard = Console.ReadLine();
                        _model.uAIdentityCard(anumber, cidentitycard);
                        string card1 = cacount.IdentityCard;
                        string contentcard = "Change identity card from " + card1 + " to " + cidentitycard;
                        _change.InsertChange(anumber,contentcard);
                        break;
                    case 4:
                        Console.WriteLine("You choice change full name.");
                        Console.WriteLine("Please enter full name.");
                        string cfullname = Console.ReadLine();
                        _model.uAFullName(anumber, cfullname);
                        string fullname1 = cacount.FullName;
                        string contentfullname = "Change full name from " + fullname1 + " to " + cfullname;
                        _change.InsertChange(anumber,contentfullname);
                        break;
                    case 5:
                        Console.WriteLine("You choice change email.");
                        Console.WriteLine("Please enter email.");
                        string cemail = Console.ReadLine();
                        _model.uAEmail(anumber, cemail);
                        string email1 = cacount.Email;
                        string contentemail = "Change email from " + email1 + " to " + cemail;
                        _change.InsertChange(anumber,contentemail);
                        break;
                    case 6:
                        Console.WriteLine("You choice change phone number.");
                        Console.WriteLine("Please enter phone number.");
                        string cphonenumber = Console.ReadLine();
                        _model.uAPhoneNumber(anumber, cphonenumber);
                        string phonenumber = cacount.PhoneNumber;
                        string contentphonenumber = "Change phone number from " + phonenumber + " to " + cphonenumber;
                        _change.InsertChange(anumber,contentphonenumber);
                        break;
                    case 7:
                        Console.WriteLine("You choice change address.");
                        Console.WriteLine("Please enter address.");
                        string caddress = Console.ReadLine();
                        _model.uAAddress(anumber, caddress);
                        string address1 = cacount.Address;
                        string contentaddress = "Change address from " + address1 + " to " + caddress;
                        _change.InsertChange(anumber,contentaddress);
                        break;
                    case 8:
                        Console.WriteLine("You choice change dob.");
                        Console.WriteLine("Please enter dob.");
                        string cdob = Console.ReadLine();
                        _model.uADob(anumber, cdob);
                        string dob1 = cacount.Dob;
                        string contentdob = "Change dob from " + dob1 + " to " + cdob;
                        _change.InsertChange(anumber,contentdob);
                        break;
                    case 9:
                        Console.WriteLine("You choice change gender.");
                        Console.WriteLine("Please enter gender.");
                        string cgender = Console.ReadLine();
                        _model.uAGender(anumber, cgender);
                        int gender1 = cacount.Gender;
                        string contentgender = "Change gender from " + gender1 + " to " + cgender;
                        _change.InsertChange(anumber,contentgender);
                        break;
                    case 10:
                        Console.WriteLine("You choice Lock account.");
                        _model.AccountLock(anumber);
                        break;
                    case 11:
                        Console.WriteLine("Exit! Bye bye");
                        break;
                    default:
                        Console.WriteLine("You enter fails. Please enter again!");
                        break;
                }

                if (choice == 10)
                {
                    DbConnection.Instance().CloseConnection();
                    break;
                }
                if (choice == 11)
                {
                    break;
                }
            }
            
        }
        
        public void Trasaction(String anumber)
        {
            YYTransaction tr = new YYTransaction();
            YYTransactionHistory th1 = new YYTransactionHistory();
            YYTransactionHistory th2 = new YYTransactionHistory();
            Console.WriteLine("Please enter import amount to transfer: ");
            tr.Amount = Decimal.Parse(Console.ReadLine());
            Console.WriteLine("Please enter content: ");
            tr.Content = Console.ReadLine();
            tr.SenderAccountNumber = anumber;
            Console.WriteLine("Please enter receiver account number");
            tr.ReceiverAccountNumber = Console.ReadLine();
            var checkAc = _model.GetByAccountNumber(tr.ReceiverAccountNumber);
            string rac = checkAc.AccountNumber;
            if (rac.Equals(tr.ReceiverAccountNumber))
            {
                Console.WriteLine("Please enter type: ");
                tr.Type = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Please enter status: ");
                tr.Status = Int32.Parse(Console.ReadLine());
                _tras.SaveTransaction(tr);
                _model.WithdrawalBalance(tr.Amount,anumber);
                _model.DepositBalance(tr.Amount,tr.ReceiverAccountNumber);
                th1.AccountNumber = anumber;
                th1.Type = 3;
                th1.Amount = tr.Amount;
                th1.TradingAcountNumber = tr.ReceiverAccountNumber;
                
                th2.AccountNumber = tr.ReceiverAccountNumber;
                th2.Type = 3;
                th2.Amount = tr.Amount;
                th2.TradingAcountNumber = anumber;
                _history.HinsertBalance(th1);
                _history.HinsertBalance(th2);
            }
            else
            {
                Console.WriteLine("This account does not exist.");
            }
            
        }


    }
}