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
                .ForCtorParam("Name", opt => opt.MapFrom(src => src.Name))
                .ForCtorParam("guid", opt => opt.MapFrom(src => src.Id))
                .ForCtorParam("fullAddress", opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
        }
    }
}
