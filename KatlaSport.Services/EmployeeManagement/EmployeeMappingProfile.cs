namespace KatlaSport.Services.EmployeeManagement
{
    using System;
    using AutoMapper;
    using DataAccessEmployee = KatlaSport.DataAccess.EmployeeInfo.Employee;
    using DataAccessEmployeeCV = KatlaSport.DataAccess.EmployeeInfo.EmployeeCV;
    using DataAccessPosition = KatlaSport.DataAccess.EmployeeInfo.Position;

    public sealed class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<DataAccessEmployee, Employee>();
            CreateMap<DataAccessEmployeeCV, EmployeeCV>();
            CreateMap<DataAccessPosition, Position>();

            CreateMap<Employee, UpdateEmployeeRequest>();
            CreateMap<EmployeeCV, UpdateEmployeeCVRequest>();
            CreateMap<Position, UpdatePositionRequest>();

            CreateMap<UpdateEmployeeRequest, DataAccessEmployee>();
            CreateMap<UpdateEmployeeCVRequest, DataAccessEmployeeCV>()
                .ForMember(r => r.LastUpdate, opt => opt.MapFrom(p => DateTime.UtcNow));
            CreateMap<UpdatePositionRequest, DataAccessPosition>();
        }
    }
}
