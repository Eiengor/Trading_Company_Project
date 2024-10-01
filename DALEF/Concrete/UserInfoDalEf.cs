using AutoMapper;
using DAL.Interface;
using DALEF.Context;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace DALEF.Concrete
{
    public class UserInfoDalEf : IUserInfoDal
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public UserInfoDalEf(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }
        public List<UserInfo> GetAll()
        {
            using (var context = new TradingCompanyContext(_connectionString))
            {
                return _mapper.Map<List<UserInfo>>(context.TblUsers);
            }

        }
        public UserInfo GetByID(int id)
        {

        }
    }
}
