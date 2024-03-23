using System.Drawing.Drawing2D;
using System.Drawing;
using AutoMapper;
using angular_crud.Models.Dto;
using angular_crud.Models.Domain;
using anuglar_crud.Models.Dto;

namespace angular_crud.Models.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Users, UserDTOs>().ReverseMap();
            CreateMap<Roles , RoleDTO>().ReverseMap();
           
           
        }
    }
}
