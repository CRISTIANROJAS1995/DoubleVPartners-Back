using Domain.Dtos;
using Domain.Dtos.Generic;
using Domain.Entities;
using Domain.Input.Task;

namespace Domain.IServices
{
    public interface ITaskService
    {
        Task<ResponseApiDto?> Add(TareaInput request, string identificadorUsuario);
        Task<ResponseApiDto?> Update(TareaActualizarInput request);
        Task<ResponseApiDto?> Delete(string identificador);
        Task<List<TareaDto>?> All();
        Task<Tarea?> ByIdentifier(string identifier);
        Task<ResponseApiDto?> ByName(string name);

    }
}
