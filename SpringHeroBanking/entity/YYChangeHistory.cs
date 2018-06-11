namespace SpringHeroBanking.entity
{
    public class YYChangeHistory
    {
        private string accountNumber;
        private string content;

        public YYChangeHistory()
        {
        }

        public string AccountNumber
        {
            get => accountNumber;
            set => accountNumber = value;
        }

        public string Content
        {
            get => content;
            set => content = value;
        }
    }
}