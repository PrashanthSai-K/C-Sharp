using System;
using AutoMapper;
using task10.Contarcts;
using task10.Models;

namespace task10.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBooks, Book>();

        CreateMap<UpdateBooks, Book>();
        
        CreateMap<CreateUser, User>();
    }
}
