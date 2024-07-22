using Domain.Dtos.Generic;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Domain.Input;
using Domain.IRepositories;
using Domain.IServices;

namespace Application.Services
{
    public class GenericService : IGenericService
    {
        private readonly IGenericRepository _genericRepository;

        public GenericService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<List<RolDto>?> AllRole()
        {
            return await _genericRepository.AllRole();
        }

        public async Task<List<EstadoTareaDto>?> AllStateTask()
        {
            return await _genericRepository.AllStateTask();
        }
    }
}
