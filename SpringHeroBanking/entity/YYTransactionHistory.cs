using Google.Protobuf.WellKnownTypes;

namespace SpringHeroBanking.entity
{
    public class YYTransactionHistory
    {
        private int _id;
        private string _accountNumber;
        private int _type;
        private decimal _amount;
        private string _tradingAcountNumber;
        private string _createdDate;

        public YYTransactionHistory()
        {
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }


        public string AccountNumber
        {
            get => _accountNumber;
            set => _accountNumber = value;
        }

        public int Type
        {
            get => _type;
            set => _type = value;
        }

        public decimal Amount
        {
            get => _amount;
            set => _amount = value;
        }

        public string TradingAcountNumber
        {
            get => _tradingAcountNumber;
            set => _tradingAcountNumber = value;
        }

        public string CreatedDate
        {
            get => _createdDate;
            set => _createdDate = value;
        }

        public string Type1()
        {
            string result = "";
            if (Type == 1)
            {
                result = "WithDraw";
            } else if (Type == 2)
            {
                result = "Depoin";
            } else if (Type == 3)
            {
                result = "Transfer";
            }

            return result;
        }

       
        public string TradingAcountNumber1()
        {
            string result = "";
            if (TradingAcountNumber.Equals("1"))
            {
                result = "Spring Hero Banking";
            }
            else
            {
                result = TradingAcountNumber;
            }
            return result;
        }
    }
}