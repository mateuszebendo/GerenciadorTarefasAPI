using System.Security.Cryptography;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;

namespace Domain.Services;

public class UsuarioDomainService : IUsuarioDomainService
{
    public string EncriptografarSenha(string senha)
    {
        var salt = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }

        var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, 10000);
        byte[] hash = pbkdf2.GetBytes(20);

        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        return Convert.ToBase64String(hashBytes);
    }

    public bool VerificarSenha(string senhaOriginal, string senhaInserida)
    {
        byte[] hashBytes = Convert.FromBase64String(senhaOriginal);

        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);

        var pbkdf2 = new Rfc2898DeriveBytes(senhaInserida, salt, 10000);
        byte[] hash = pbkdf2.GetBytes(20);

        for (int i = 0; i < 20; i++)
        {
            if (hashBytes[i + 16] != hash[i])
            {
                throw new EmailouSenhaInvalidoException("Email ou senhas inválidos!");
            }
        }

        return true;
    }

    public Usuario VerificaLogin(List<Usuario> usuarios, string email)
    {
        foreach (var usuario in usuarios)
        {
            if (usuario.Email.Equals(email))
            {
                return usuario;
            }
        }

        throw new EmailouSenhaInvalidoException("Email ou senha inválidos!");
    } 
    
}