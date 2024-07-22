using Domain.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface ITaskRepository
    {
        Task<int?> Add(Tarea model);
        Task<int?> Update(Tarea model);
        Task<int?> Delete(Tarea model);
        Task<List<TareaDto>?> All();
        Task<Tarea?> ByName(string name);
        Task<Tarea?> ByIdentifier(string identifier);
        Task<Usuario?> ByUserIdentifier(string identifier);
        Task<int?> AddAssignTask(AsignacionTarea model);
        Task<AsignacionTarea?> ValidAssignTask(string identifierUser, string identifierTask);
        Task<List<AsignacionTareaDto>?> AllAssignTask();
        Task<List<AsignacionTareaDto>?> ByUserAssignTask(int userId);
    }
}
