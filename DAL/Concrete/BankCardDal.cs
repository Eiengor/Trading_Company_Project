using DAL.Interface;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class BankCardDal : IBankCardDal
    {
        private readonly SqlConnection _connection;
        public BankCardDal(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public List<BankCard> GetAll()
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "select bank_card_id, owner_id, number from bank_card";
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                var bankCards = new List<BankCard>();
                while (reader.Read())
                {
                    bankCards.Add(new BankCard
                    {
                        BankCardId = Convert.ToInt32(reader["bank_card_id"]),
                        OwnerId = Convert.ToInt32(reader["owner_id"]),
                        Number = reader["number"].ToString()
                    });
                }
                _connection.Close();
                return bankCards;
            }
        }

        public BankCard GetById(int id)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT bank_card_id, owner_id, number FROM bank_card WHERE bank_card_id = @bankCardId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("bankCardId", id);
                _connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                BankCard? bankCard = null;
                if(reader.Read())
                {
                    bankCard = new BankCard
                    {
                        BankCardId = Convert.ToInt32(reader["bank_card_id"]),
                        OwnerId = Convert.ToInt32(reader["owner_id"]),
                        Number = reader["number"].ToString()
                    };
                }
                _connection.Close();
                if(bankCard == null) 
                {
                    throw new NullReferenceException("Invalid Bank Card ID");
                }
                return bankCard;
            }
        }
    }
}
