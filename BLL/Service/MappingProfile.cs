using AutoMapper;
using BlogSF.DAL.Models;

namespace BlogSF.BLL.Service
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UserViewModel>()
                .ConstructUsing(v => new UserViewModel(v));

        }
    }
}
