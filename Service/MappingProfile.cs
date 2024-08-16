using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {

            CreateMap<Company, CompanyDto>()
                .ForMember(c => c.fullAddress, opt => opt.MapFrom(src => string.Join(' ', src.Address, src.Country)))
                .ForMember(c=>c.guid, opt=> opt.MapFrom(x=>x.Id));


            //*-*-*-*-*-*-*-*-*-*-*-*-*-* ЕСЛИ record CompanyDto С ПАРАМЕТРАМИ
            //CreateMap<Company, CompanyDto>()
            //    .ForCtorParam("Name", opt => opt.MapFrom(src => src.Name))
            //    .ForCtorParam("guid", opt => opt.MapFrom(src => src.Id))
            //    .ForCtorParam("fullAddress", opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

            CreateMap<Employee, EmployeeDto>()
                .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
                .ForCtorParam("Name", opt => opt.MapFrom(src => src.Name))
                .ForCtorParam("Age", opt => opt.MapFrom(src => src.Age))
                .ForCtorParam("Position", opt => opt.MapFrom(src => src.Position));
        }
    }
}
