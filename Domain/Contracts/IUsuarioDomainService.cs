using Domain.Entities;

namespace Domain.Contracts;

public interface IUsuarioDomainService
{
    string EncriptografarSenha(string senha);
    bool VerificarSenha(string senhaOriginal, string senhaInserida);
    Usuario VerificaLogin(List<Usuario> usuarios, string emailInserido);
}