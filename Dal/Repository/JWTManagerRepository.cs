using Domain.Entities;
using Domain.Extension;
using Domain.IRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Input;
using Domain.Dtos.Generic;

namespace Dal.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IUsuarioRepository _userRepository;
        private readonly IConfiguration iconfiguration;

        public JWTManagerRepository(IConfiguration iconfiguration, IUsuarioRepository userRepository)
        {
            this.iconfiguration = iconfiguration;
            _userRepository = userRepository;
        }

        public async Task<ResponseApiDto> Authenticate(LoginInput request)
        {
            var response = new ResponseApiDto();
            var valid = await _userRepository.ValidAuth(request);

            if (valid.Identificador == null)
            {
                response.Result = false;
                response.Message = "Combinación incorrecta de nombre de usuario y contraseña.";
                return response;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
                new Claim(ClaimTypes.NameIdentifier, valid.Identificador),
                new Claim(ClaimTypes.Name, valid.Nombre!),
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Role, valid.Role.Nombre),
              }),
                Expires = DateTime.UtcNow.AddMinutes(1440), //24hours
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            response.Result = true;
            response.Data = new Tokens { Token = tokenHandler.WriteToken(token), RefreshToken = DateTime.UtcNow.AddMinutes(1440).ToString(), UsuarioData = valid };

            return response;

        }
    }
}
