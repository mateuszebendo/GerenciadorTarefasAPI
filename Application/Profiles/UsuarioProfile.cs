using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<UsuarioDTO, Usuario>().ReverseMap();
    }
}