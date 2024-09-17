using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;

namespace Domain.Services;

public class TarefaDomainService : ITarefaDomainService
{
    public void VerificaDuplicidadeTitulo(List<string> listaTarefas,  string tituloTarefa)
    {
        if (listaTarefas.BinarySearch(tituloTarefa) >= 0)
        {
            throw new TituloDuplicadoException("Título duplicado");
        }
    }
}