using Domain.Dtos.Generic;
using Domain.Input;

namespace Domain.IRepositories
{
    public interface IJWTManagerRepository
    {
        Task<ResponseApiDto> Authenticate(LoginInput request);
    }
}
