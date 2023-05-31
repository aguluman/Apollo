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
      
      
      CreateMap<Attendance, AttendanceDto>();
      
      CreateMap<AttendanceForClockInDto, Attendance>()
         .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
         .ForMember(dest => dest.ClockIn, opt => opt.MapFrom(src => DateTimeOffset.Now)).ReverseMap();
            
      //Add a mapping from AttendanceForClockInDto to  AttendanceDto
      CreateMap<AttendanceForClockInDto, AttendanceDto>()
         .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
         .ForMember(dest => dest.ClockIn, opt => opt.MapFrom(src => DateTimeOffset.Now)).ReverseMap();
      
      CreateMap<AttendanceForClockOutDto, Attendance>()
         .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
         .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AttendanceId))
         .ForMember(dest => dest.ClockOut, opt => opt.MapFrom(src => DateTimeOffset.Now)).ReverseMap();
      
      //Add a mapping from AttendanceForClockOutDto to  AttendanceDto
      CreateMap<AttendanceForClockOutDto, AttendanceDto>()
         .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
         .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AttendanceId))
         .ForMember(dest => dest.ClockOut, opt => opt.MapFrom(src => DateTimeOffset.Now)).ReverseMap();

      CreateMap<AttendanceForBtClockInDto, Attendance>()
         .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
         .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AttendanceId))
         .ForMember(dest => dest.BreakTimeStart, opt => opt.MapFrom(src => DateTimeOffset.Now)).ReverseMap();
            
      //Add a mapping from AttendanceForBtClockInDto to AttendanceDto
      CreateMap<AttendanceForBtClockInDto, AttendanceDto>()
         .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
         .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AttendanceId))
         .ForMember(dest => dest.BreakTimeStart, opt => opt.MapFrom(src => DateTimeOffset.Now)).ReverseMap();

      CreateMap<AttendanceForBtClockOutDto, Attendance>()
         .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
         .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AttendanceId))
         .ForMember(dest => dest.BreakTimeEnd, opt => opt.MapFrom(src => DateTimeOffset.Now)).ReverseMap();
      
      //Add a mapping from AttendanceForBtClockOutDto to AttendanceDto
      CreateMap<AttendanceForBtClockOutDto, AttendanceDto>()
         .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
         .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AttendanceId))
         .ForMember(dest => dest.BreakTimeEnd, opt => opt.MapFrom(src => DateTimeOffset.Now)).ReverseMap();


      //Todo: Add Comeback here, in case any mapping logic breaks
   }
}