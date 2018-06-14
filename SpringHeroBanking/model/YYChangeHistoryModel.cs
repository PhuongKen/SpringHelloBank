using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SpringHeroBanking.entity;

namespace SpringHeroBanking.model
{
    public class YYChangeHistoryModel
    {
        List<YYChangeHistory> _list = new List<YYChangeHistory>();
        
        // Thêm vào lịch sử thay đổi.//////
        public void InsertChange(String accountNumber, String content)
        {
            DbConnection.Instance().OpenConnection();
            string insertChange = "insert into `chistory`(accountNumber,content) values (@accountNumber,@content)";
            MySqlCommand cmd = new MySqlCommand(insertChange,DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@accountNumber",accountNumber);
            cmd.Parameters.AddWithValue("@content", content);
            cmd.ExecuteNonQuery();
            DbConnection.Instance().CloseConnection();
        }

        // Lấy về lịch sử thay đổi.
        public List<YYChangeHistory> queryChange(String accountNumber)
        {
            DbConnection.Instance().OpenConnection();
            string queryChange = "select * from `chistory` where accountNumber = @accountNumber";
            MySqlCommand cmd = new MySqlCommand(queryChange,DbConnection.Instance().Connection);
            cmd.Parameters.AddWithValue("@accountNumber",accountNumber);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                YYChangeHistory ch = new YYChangeHistory();
                ch.AccountNumber = reader.GetString(reader.GetOrdinal("accountNumber"));
                ch.Content = reader.GetString(reader.GetOrdinal("content"));
                _list.Add(ch);
            }

            return _list;
        }
    }
}