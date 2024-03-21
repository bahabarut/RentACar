using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
           
            CreateMap<Brand, CreatedBrandDto>().ReverseMap();
            //reverse map reverse class types for mapping process
            //CreateMap<CreatedBrandDto, Brand>();
            CreateMap<Brand, CreateBrandCommand>().ReverseMap();
        }
    }
}
