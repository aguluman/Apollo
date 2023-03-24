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

      CreateMap<Employee, EmployeeDto>();

      CreateMap<CompanyForCreationDto, Company>();

   }
}