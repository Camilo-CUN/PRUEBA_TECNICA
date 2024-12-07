using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PRUEBA_TECNICA.db_context;
using PRUEBA_TECNICA.services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios de controladores
builder.Services.AddControllers();

// Configuración de Swagger para JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter JWT with Bearer into field",
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] { }
		}
	});
});

// Configuración de autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
		};
	});

// Agregar DbContexts
builder.Services.AddDbContext<ProductsContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("PRUEBADB"))
);
builder.Services.AddDbContext<UsersContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("PRUEBADB"))
);

// Agregar servicios para UserDbService y ProductDbService
builder.Services.AddScoped<IProductDbService, ProductDbService>();
builder.Services.AddScoped<IUserDbService, UserDbService>();
builder.Services.AddScoped<IAuthService, AuthService>();



// Crear la aplicación
var app = builder.Build();

// Usar autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Habilitar Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
