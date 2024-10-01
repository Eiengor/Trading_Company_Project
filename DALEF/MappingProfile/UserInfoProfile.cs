using AutoMapper;
using DALEF.Models;
using DTO;
namespace DALEF.MappingProfile
{
    public class UserInfoProfile: Profile
    {
        public UserInfoProfile()
        {
            CreateMap<TblUserInfo, UserInfo>();
            CreateMap<UserInfo, TblUserInfo>();
        }
    }
}
