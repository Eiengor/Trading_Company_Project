using AutoMapper;
using DAL.Interface;
using DALEF.Context;
using DTO;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace DALEF.Concrete
{
    public class BankCardDalEf : IBankCardDal
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public BankCardDalEf(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }
        public List<BankCard> GetAll()
        {
            using (var context = new TradingCompanyContext(_connectionString))
            {
                return _mapper.Map<List<BankCard>>(context.TblBankCards.Include(m => m.UserInfo));
            }

        }
        public BankCard GetById(int id)
        {
        }
    }
}
