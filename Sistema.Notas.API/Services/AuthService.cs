using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sistema.Notas.API.Services
{

    public interface IAuthService
    {
        string GenerateJwtToken(string username, string role);
        bool ValidateUser(string username, string password, out string role);
    }

    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Simulación de usuarios en memoria (puedes cambiar esto a una BD)
        private readonly Dictionary<string, (string Password, string Role)> _users =
            new Dictionary<string, (string, string)>
            {
                { "admin", ("admin123", "Administrador") },
                { "profesor1", ("profesor123", "Profesor") },
                { "estudiante1", ("estudiante123", "Estudiante") }
            };

        public bool ValidateUser(string username, string password, out string role)
        {
            role = string.Empty;
            if (_users.TryGetValue(username, out var userInfo) && userInfo.Password == password)
            {
                role = userInfo.Role;
                return true;
            }
            return false;
        }

        public string GenerateJwtToken(string username, string role)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expirationMinutes = int.Parse(jwtSettings["ExpirationMinutes"]!);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
