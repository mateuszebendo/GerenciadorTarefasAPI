using Application.Contracts;
using Application.DTOs;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;

namespace Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUsuarioDomainService _usuarioDomainService;
    private readonly IMapper _mapper;

    public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, IUsuarioDomainService usuarioDomainService)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
        _usuarioDomainService = usuarioDomainService;
    }
    
    public async Task<bool> RegistrarUsuario(CriarUsuarioDTO criarUsuarioDto)
    {
        try
        {
            criarUsuarioDto.Senha = _usuarioDomainService.EncriptografarSenha(criarUsuarioDto.Senha);
            var usuario = new Usuario(criarUsuarioDto.Nome, criarUsuarioDto.Status, criarUsuarioDto.Email, criarUsuarioDto.Senha);
            return await _usuarioRepository.RegistrarUsuario(usuario);
        }
        catch (SystemException error)
        {
            throw new ApplicationException("Ocorreu um erro com os dados fornecidos." + error.Message);
        }
    }

    public async Task<int> LogarUsuario(LoginUsuarioDTO loginUsuarioDto)
    {
        try
        {
            List<Usuario> usuarios = new List<Usuario>();
            foreach (var usuario in await _usuarioRepository.RecuperarUsuariosAtivos())
            {
                usuarios.Add(usuario);
            }

            var usuarioEncontrado = _usuarioDomainService.VerificaLogin(usuarios, loginUsuarioDto.Email);

            if (_usuarioDomainService.VerificarSenha(usuarioEncontrado.Senha, loginUsuarioDto.SenhaInserida))
            {
                return usuarioEncontrado.UsuarioId;
            }

            return -1;
        } catch (SystemException error)
        {
            throw new ApplicationException("Ocorreu um erro com os dados fornecidos." + error.Message);
        }
    }

    public async Task<UsuarioDTO> RecuperarInformacoesUsuario(int id)
    {
        try
        {
            return _mapper.Map<UsuarioDTO>(await _usuarioRepository.RecuperarUsuarioPorId(id));
        } catch (SystemException error)
        {
            throw new ApplicationException("Ocorreu um erro com os dados fornecidos." + error.Message);
        }
    }

    // public async Task<bool> Put(UsuarioDTO usuarioDto, int id)
    // {
    //     try
    //     {
    //         return await _usuarioRepository.Put(_mapper.Map<Usuario>(usuarioDto), id);
    //     } catch (SystemException error)
    //     {
    //         throw new ApplicationException("Ocorreu um erro com os dados fornecidos." + error.Message);
    //     }
    // }

    public async Task<bool> DesativarUsuario(int id)
    {
        try
        {
            return await _usuarioRepository.DesativarUsuario(id);
        } catch (SystemException error)
        {
            throw new ApplicationException("Ocorreu um erro com os dados fornecidos." + error.Message);
        }
    }
}