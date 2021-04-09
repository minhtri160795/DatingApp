using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Users, UsersForListDto>()
            .ForMember(
                dest => dest.PhotoUrl
                , opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url)
            )
            .ForMember(
                dest => dest.Age
                , opt => opt.ResolveUsing(a => a.DateOfBirth.CalculateAge()));
            CreateMap<Users, UsersForDetailDto>()
            .ForMember(
                dest => dest.PhotoUrl
                , opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url)
            )
            .ForMember(
                dest => dest.Age
                , opt => opt.ResolveUsing(a => a.DateOfBirth.CalculateAge()));

            CreateMap<Photo, PhotosForDetailDto>();
        }

    }
}