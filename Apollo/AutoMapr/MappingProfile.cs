using AutoMapper;
using Entities.Models;
using Entities.Models.Enums;
using Shared.DataTransferObjects;

namespace Apollo.AutoMapr;

public class MappingProfile : Profile
{
   public MappingProfile()
   {
      CreateMap<Company, CompanyDto>()
         .ForMember(c => c.FullAddress,
            option =>
               option.MapFrom(x => string.Join(' ', x.Address, x.Country)));

      CreateMap<CompanyForCreationDto, Company>();
      CreateMap<CompanyForUpdateDto, Company>();
      
      
      CreateMap<Employee, EmployeeDto>();
      CreateMap<EmployeeForCreationDto, Employee>();
      CreateMap<EmployeeForUpdateDto, Employee>();
      CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
      
      CreateMap<UserForRegistrationDto, User>();
      
      CreateMap<Tasks, TasksDto>()
         .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.ToString()))
         .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.ToString()));
      CreateMap<TasksForCreationDto, Tasks>()
         .ForMember(dest => dest.State, opt => opt.MapFrom(src => Enum.Parse<State>(src.State)))
         .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => Enum.Parse<Priority>(src.Priority)));
      CreateMap<TasksForCreationDto, TasksDto>()
         .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
         .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority));
      CreateMap<TasksForUpdateDto, Tasks>()
         .ForMember(dest => dest.State, opt => opt.MapFrom(src => Enum.Parse<State>(src.State)))
         .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => Enum.Parse<Priority>(src.Priority)));
      CreateMap<TasksForUpdateDto, Tasks>().ReverseMap();
   }
}