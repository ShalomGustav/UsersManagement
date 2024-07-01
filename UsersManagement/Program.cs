using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsersManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<UserService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "Users Management API", Version = "v1" });

    
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    x.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(x => x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,//Указывает, нужно ли проверять издателя токена.
        ValidIssuer = AuthOptions.ISSUER,//Издатель токена, который должен быть допустим
        ValidateAudience = true,//Указывает, нужно ли проверять потребителя токена.
        ValidAudience = AuthOptions.AUDIENCE,//Потребитель токена, который должен быть допустим
        ValidateLifetime = true,//Указывает, нужно ли проверять срок действия токена
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),//Ключ для проверки подписи токена.
        ValidateIssuerSigningKey = true,//Указывает, нужно ли проверять ключ подписи токена.
    });

builder.Services.AddAuthorization();//

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllers();

app.Map("/login", (string username) =>
{
    var claims = new List<Claim> {new Claim(ClaimTypes.Name, username)};
    var jwt = new JwtSecurityToken(
        issuer: AuthOptions.ISSUER,
        audience: AuthOptions.AUDIENCE,
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(10),
        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
    return new JwtSecurityTokenHandler().WriteToken(jwt);
});

app.Map("/data", [Authorize] () => new { message = "Token is valid" });//подумать над маршрутом 

app.Run();




