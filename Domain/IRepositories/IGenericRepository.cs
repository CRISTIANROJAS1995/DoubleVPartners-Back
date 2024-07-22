using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IGenericRepository
    {
        Task<List<RolDto>?> AllRole();
        Task<List<EstadoTareaDto>?> AllStateTask();
    }
}
