using Microsoft.IdentityModel.Tokens;
using PRUEBA_TECNICA.Models;
using PRUEBA_TECNICA.services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public interface IAuthService
{
	Task<string> Authenticate(string email, string password);
}

public class AuthService : IAuthService
{
	private readonly IConfiguration _configuration;
	private readonly IUserDbService _userDbService;  // Usar la interfaz

	public AuthService(IConfiguration configuration, IUserDbService userDbService)
	{
		_configuration = configuration;
		_userDbService = userDbService;  // Inyección de la interfaz
	}

	public async Task<string> Authenticate(string email, string password)
	{
		var user = await _userDbService.GetUserByEmailAndPasswordAsync(email, password);
		if (user == null)
		{
			return null;  // Si el usuario no existe o la contraseña es incorrecta, retorna null
		}

		var claims = new[]
		{
		new Claim(ClaimTypes.Name, user.Name),
		new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
		new Claim(ClaimTypes.Role, user.rol)
	};

		// Crear una clave de seguridad para firmar el token
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
		var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		// Generar el token JWT
		var token = new JwtSecurityToken(
			issuer: _configuration["Jwt:Issuer"],
			audience: _configuration["Jwt:Audience"],
			claims: claims,
			expires: DateTime.Now.AddMinutes(30),
			signingCredentials: credentials
		);

		return new JwtSecurityTokenHandler().WriteToken(token);  // Retorna el token JWT
	}
}
