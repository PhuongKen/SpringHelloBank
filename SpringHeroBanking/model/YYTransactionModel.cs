using System;
using MySql.Data.MySqlClient;
using SpringHeroBanking.entity;

namespace SpringHeroBanking.model
{
    public class YYTransactionModel
    {
        public Boolean SaveTransaction(YYTransaction tr)
        {
            DbConnection.Instance().OpenConnection();
            string queryTransaction = "insert into `transaction`(amount,content,senderAccountNumber," +
                                      "receiverAccountNumber,type,status) " +
                                      "values(@val1,@val2,@val3,@val4,@val5,@val6)";
            MySqlCommand cmd = new MySqlCommand(queryTransaction, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@val1", tr.Amount);
            cmd.Parameters.AddWithValue("@val2", tr.Content);
            cmd.Parameters.AddWithValue("@val3", tr.SenderAccountNumber);
            cmd.Parameters.AddWithValue("@val4", tr.ReceiverAccountNumber);
            cmd.Parameters.AddWithValue("@val5", tr.Type);
            cmd.Parameters.AddWithValue("@val6", tr.Status);
            cmd.ExecuteNonQuery();
            DbConnection.Instance().CloseConnection();
            return true;
        }
        
    }
}