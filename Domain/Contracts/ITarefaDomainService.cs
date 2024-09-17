using Domain.Entities;

namespace Domain.Contracts;

public interface ITarefaDomainService
{
    void VerificaDuplicidadeTitulo(List<string> listaTarefas, string tituloTarefa);
}