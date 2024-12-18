using AutoMapper;
using AutoWorkshopApi.Models;
using AutoWorkshop.Shared.DTOs;
using AutoWorkshop.Shared.Enums;

namespace AutoWorkshopApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map from Client to ClientDTO
            CreateMap<Client, ClientDTO>()
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            // Map from Job to JobDTO
            CreateMap<Job, JobDTO>()
                .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.JobId))
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.LicensePlate, opt => opt.MapFrom(src => src.LicensePlate))
                .ForMember(dest => dest.ManufactureYear, opt => opt.MapFrom(src => src.ManufactureYear))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Severity, opt => opt.MapFrom(src => src.Severity))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status)); 

            CreateMap<Client, ClientWithJobsDTO>()
            .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.ClientId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Jobs, opt => opt.MapFrom(src => src.Jobs));
        }
    }
}
