using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles;

public class TarefaProfile : Profile
{
    public TarefaProfile()
    {
        CreateMap<Tarefa, TarefaDTO>().ReverseMap();
    }
}