using System;
using AutoMapper;
using TaskManagement_RESTAPI.Entities.Models;
using TaskManagement_RESTAPI.Shared.DTO;

namespace TaskManagement_RESTAPI.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateTask, TaskItem>();
        CreateMap<UpdateTask, TaskItem>();
    }
}
