using Domain.Dtos;
using Domain.Entities;
using Domain.Extension;
using Domain.IRepositories;
using Domain.IServices;
using System.Text;
using System.Security.Cryptography;
using System.Reflection;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using Domain.Input;
using System.Numerics;
using System.Net;
using Domain.Dtos.Generic;
using Domain.Enums;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _userRepository;
        private readonly IJWTManagerRepository _jWTManagerRepository;

        public UsuarioService(
            IUsuarioRepository userRepository,
            IJWTManagerRepository jWTManagerRepository)
        {
            _userRepository = userRepository;
            _jWTManagerRepository = jWTManagerRepository;
        }

        public async Task<ResponseApiDto> Authenticate(LoginInput request)
        {
            request.Password = EncryptPassword(request.Password);
            return await _jWTManagerRepository.Authenticate(request);
        }

        public async Task<ResponseApiDto?> Add(UsuarioInput request)
        {
            var response = new ResponseApiDto();

            var user = await _userRepository.ByEmail(request.Email);
            if (user == null)
            {
                Guid obj = Guid.NewGuid();
                Usuario model = new()
                {
                    Identificador = obj.ToString(),
                    RoleId = request.RolId > 0 ? request.RolId : (int)RolEnum.Empleado,
                    Email = request.Email,
                    Password = EncryptPassword(request.Password),
                    Nombre = request.Nombre,
                    NumeroIdentificacion = request.NumeroIdentificacion,
                    Telefono = request.Telefono,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                };

                var responseAdd = await _userRepository.Add(model);
                if (responseAdd == 1)
                {
                    response.Result = true;
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado al guardar el usuario.";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "El usuario ya se encuentra registrado.";
            }

            return response;
        }

        public async Task<ResponseApiDto?> Update(UsuarioActualizarInput request, string identifier)
        {
            var response = new ResponseApiDto();

            var user = await _userRepository.ByIdentifier(identifier);
            if (user == null)
            {
                response.Result = false;
                response.Message = "El usuario no existe.";
            }
            else
            {
                user.Password = request.Password != null ? EncryptPassword(request.Password) : user.Password;
                user.Nombre = request.Nombre != null ? request.Nombre : user.Nombre;
                user.NumeroIdentificacion = request.NumeroIdentificacion != null ? request.NumeroIdentificacion : user.NumeroIdentificacion;
                user.Telefono = request.Telefono != null ? request.Telefono : user.Telefono;
                user.FechaActualizacion = DateTime.Now;

                var responseUpdate = await _userRepository.Update(user);
                if (responseUpdate == 1)
                {
                    response.Result = true;
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado al actualizar el usuario.";
                }
            }

            return response;
        }

        public async Task<ResponseApiDto?> UpdateToAdmin(UsuarioActualizarAdminInput request)
        {
            var response = new ResponseApiDto();

            var user = await _userRepository.ByIdentifier(request.Identificador);
            if (user == null)
            {
                response.Result = false;
                response.Message = "El usuario no existe.";
            }
            else
            {
                user.RoleId = request.RolId > 0 ? request.RolId : user.RoleId;
                user.Password = request.Password != null ? EncryptPassword(request.Password) : user.Password;
                user.Nombre = request.Nombre != null ? request.Nombre : user.Nombre;
                user.NumeroIdentificacion = request.NumeroIdentificacion != null ? request.NumeroIdentificacion : user.NumeroIdentificacion;
                user.Telefono = request.Telefono != null ? request.Telefono : user.Telefono;
                user.FechaActualizacion = DateTime.Now;

                var responseUpdate = await _userRepository.Update(user);
                if (responseUpdate == 1)
                {
                    response.Result = true;
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado al actualizar el usuario.";
                }
            }

            return response;
        }

        public async Task<List<UsuarioDto>?> All()
        {
            return await _userRepository.All();
        }

        public async Task<UsuarioDto> ByIdentifierDto(string identifier)
        {
            return await _userRepository.ByIdentifierDto(identifier);
        }

        public async Task<Usuario?> ByIdentifier(string identifier)
        {
            return await _userRepository.ByIdentifier(identifier);
        }

        public async Task<ResponseApiDto?> ByEmail(string email)
        {
            var response = new ResponseApiDto();

            var user = await _userRepository.ByEmail(email);
            if (user != null)
            {
                response.Result = true;
            }

            return response;
        }

        public async Task<ResponseApiDto?> ByIdentification(string numberIdentification)
        {
            var response = new ResponseApiDto();

            var user = await _userRepository.ByIdentification(numberIdentification);
            if (user != null)
            {
                response.Result = true;
            }

            return response;
        }

        private static string EncryptPassword(string password)
        {
            var md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(Encoding.ASCII.GetBytes(password));

            //get hash result after compute it
            var result = md5.Hash;

            var strBuilder = new StringBuilder();
            foreach (var t in result)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(t.ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
