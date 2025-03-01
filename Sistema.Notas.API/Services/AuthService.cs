using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Sistema.Notas.API.Services
{

    public interface IAuthService
    {
        string GenerateJwtToken(string username, string role, string name);
        bool ValidateUser(string username, string password, out string role, out string name);
    }

    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Simulación de usuarios en memoria (puedes cambiar esto a una BD)
        private readonly Dictionary<string, (string Password, string Role, string Name)> _users =
            new Dictionary<string, (string, string, string)>
            {
                { "admin", ("admin123", "Administrador", "Angelica Sanchez") },
                { "profesor1", ("profesor123", "Profesor", "Pedro Lozano") },
                { "estudiante1", ("estudiante123", "Estudiante", "Maria Rios") }
            };

        public bool ValidateUser(string username, string password, out string role, out string name)
        {
            role = string.Empty;
            name = string.Empty;

            if (_users.TryGetValue(username, out var userInfo) && userInfo.Password == password)
            {
                role = userInfo.Role;
                name = userInfo.Name;
                return true;
            }
            return false;
        }

        public string GenerateJwtToken(string username, string role, string name)
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
                new Claim("username", username),
                new Claim("role", role),
                new Claim("name", name)
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
