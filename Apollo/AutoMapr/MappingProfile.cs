using AutoMapper;
using Entities.Models;
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

      CreateMap<Tasks, TasksDto>();

      CreateMap<TasksForCreationDto, Tasks>();

      CreateMap<TasksForUpdateDto, Tasks>();

      CreateMap<TasksForUpdateDto, Tasks>().ReverseMap();

   }
}