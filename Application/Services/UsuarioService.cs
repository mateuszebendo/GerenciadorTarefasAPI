using Application.Contracts;
using Application.DTOs;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;

namespace Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    }
    
    public async Task<UsuarioDTO> Post(CriarUsuarioDTO criarUsuarioDto)
    {
        try
        {
            var usuario = new Usuario(criarUsuarioDto.Nome, criarUsuarioDto.Status, criarUsuarioDto.Email, criarUsuarioDto.Senha);
            return _mapper.Map<UsuarioDTO>(await _usuarioRepository.Post(usuario));
        }
        catch (SystemException error)
        {
            throw new ApplicationException("Ocorreu um erro com os dados fornecidos." + error.Message);
        }
    }

    public async Task<List<UsuarioDTO>> Get()
    {
        try
        {
            List<UsuarioDTO> usuarioDtos = new List<UsuarioDTO>();
            foreach (var usuario in await _usuarioRepository.Get())
            {
                usuarioDtos.Add(_mapper.Map<UsuarioDTO>(usuario));
            }

            return usuarioDtos;
        } catch (SystemException error)
        {
            throw new ApplicationException("Ocorreu um erro com os dados fornecidos." + error.Message);
        }
    }

    public async Task<UsuarioDTO> GetById(int id)
    {
        try
        {
            return _mapper.Map<UsuarioDTO>(await _usuarioRepository.GetById(id));
        } catch (SystemException error)
        {
            throw new ApplicationException("Ocorreu um erro com os dados fornecidos." + error.Message);
        }
    }

    public async Task<bool> Put(UsuarioDTO usuarioDto, int id)
    {
        try
        {
            return await _usuarioRepository.Put(_mapper.Map<Usuario>(usuarioDto), id);
        } catch (SystemException error)
        {
            throw new ApplicationException("Ocorreu um erro com os dados fornecidos." + error.Message);
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            return await _usuarioRepository.Delete(id);
        } catch (SystemException error)
        {
            throw new ApplicationException("Ocorreu um erro com os dados fornecidos." + error.Message);
        }
    }
}