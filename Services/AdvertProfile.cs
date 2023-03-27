using WebAdvert.Models;
using AutoMapper;

namespace WebAdvert.Services
{
    public class AdvertProfile : Profile
    {
        public AdvertProfile()
        {
            CreateMap<AdvertDbModel, AdvertModel>().ReverseMap();
        }
    }
}