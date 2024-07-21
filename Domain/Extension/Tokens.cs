using Domain.Dtos;

namespace Domain.Extension
{
    public class Tokens
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public UsuarioDto UsuarioData { get; set; }
    }
}
