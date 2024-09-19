using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Contracts;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Presentation.Requests;


namespace Presentation.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly IConfiguration _configuration;

    public AuthController(IUsuarioService usuarioService, IConfiguration configuration)
    {
        _usuarioService = usuarioService;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegistrarUsuario([FromBody] CriarUsuarioRequest criarUsuarioRequest)
    {
        var criarUsuarioDTO = new CriarUsuarioDTO(criarUsuarioRequest.Nome, criarUsuarioRequest.Email, criarUsuarioRequest.Senha);
        if (await _usuarioService.RegistrarUsuario(criarUsuarioDTO))
        {
            return Ok("Usuário cadastrado com sucesso!");
        }

        return BadRequest("Houve um erro ao registrar o usuário");
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUsuario([FromBody] LoginUsuarioRequest loginUsuarioRequest)
    {
        var loginUsuarioDto = new LoginUsuarioDTO(loginUsuarioRequest.Email, loginUsuarioRequest.Senha);
        if (await _usuarioService.LogarUsuario(loginUsuarioDto))
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, loginUsuarioDto.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(double.Parse(jwtSettings["ExpireHours"])),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

        return Unauthorized("Login ou senha inválidos!");
    }
}