using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SpringHeroBanking.entity;

namespace SpringHeroBanking.model
{
    public class YYTransactionHistoryModel
    {
        
        public Boolean HinsertBalance(YYTransactionHistory h)
        {
            DbConnection.Instance().OpenConnection();
            var hinsertBalance = "insert into `thistory`(accountNumber,type,amount,tradingAcountNumber) values (" +
                                 "@accountNumber,@type,@amount,@tradingAcountNumber)";
            MySqlCommand cmd = new MySqlCommand(hinsertBalance, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@accountNumber", h.AccountNumber);
            cmd.Parameters.AddWithValue("@type", h.Type);
            cmd.Parameters.AddWithValue("@amount", h.Amount);
            cmd.Parameters.AddWithValue("@tradingAcountNumber", h.TradingAcountNumber);
            cmd.ExecuteNonQuery();
            DbConnection.Instance().CloseConnection();
            return true;
        }

        public List<YYTransactionHistory> queryHistory(String anumber)
        {
            List<YYTransactionHistory> list = new List<YYTransactionHistory>();
            DbConnection.Instance().OpenConnection();
            var query = "select * from `thistory` where accountNumber = @accountNumber";
            MySqlCommand cmd = new MySqlCommand(query, DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@accountNumber", anumber);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                YYTransactionHistory th = new YYTransactionHistory();
                th.Id = reader.GetInt16(reader.GetOrdinal("id"));
                th.AccountNumber = reader.GetString(reader.GetOrdinal("accountNumber"));
                th.Type = reader.GetInt16(reader.GetOrdinal("type"));
                th.Amount = reader.GetDecimal(reader.GetOrdinal("amount"));
                th.TradingAcountNumber = reader.GetString(reader.GetOrdinal("tradingAcountNumber"));
                th.CreatedDate = reader.GetString(reader.GetOrdinal("createdDate"));
                list.Add(th);
            }

            return list;
        }
    }
}