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
    }
}
