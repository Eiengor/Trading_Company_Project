using AutoMapper;
using DALEF.Models;
using DTO;

namespace DALEF.MappingProfile
{
    public class BankCardProfile: Profile
    {
        public BankCardProfile()
        {
            CreateMap<TblBankCard, BankCard>();
            CreateMap<BankCard, TblBankCard>();
        }
    }
}
