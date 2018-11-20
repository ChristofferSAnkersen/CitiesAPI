using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CitiesAPI.DTOs;

namespace CitiesAPI.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to DTO
            CreateMap<City, CityDTO>();
            CreateMap<Attraction, AttractionDTO>();

            //DTO to Domain
            CreateMap<CityDTO, City>();
            CreateMap<AttractionDTO, Attraction>();
        }
    }
}
