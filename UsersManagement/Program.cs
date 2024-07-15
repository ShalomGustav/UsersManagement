using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsersManagement.Repositories;
using UsersManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<UserService>();

builder.Services.AddDbContext<UserDbContext>((provider, options) =>
{
    options.UseSqlServer("Data Source=(local);Initial Catalog=UserManagement;Persist Security Info=True;User ID=test;Password=test;MultipleActiveResultSets=True;Connect Timeout=30;TrustServerCertificate=True");
});

builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<Func<IUserRepository>>(provider => () => provider.CreateScope().
ServiceProvider.GetRequiredService<IUserRepository>());



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
        ValidateIssuer = true,//���������, ����� �� ��������� �������� ������.
        ValidIssuer = AuthOptions.ISSUER,//�������� ������, ������� ������ ���� ��������
        ValidateAudience = true,//���������, ����� �� ��������� ����������� ������.
        ValidAudience = AuthOptions.AUDIENCE,//����������� ������, ������� ������ ���� ��������
        ValidateLifetime = true,//���������, ����� �� ��������� ���� �������� ������
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),//���� ��� �������� ������� ������.
        ValidateIssuerSigningKey = true,//���������, ����� �� ��������� ���� ������� ������.
    });

builder.Services.AddAuthorization();//

var app = builder.Build();
using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
var dbContext = scope.ServiceProvider.GetService<UserDbContext>();
dbContext.Database.Migrate();

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

app.Map("/data", [Authorize] () => new { message = "Token is valid" });//�������� ��� ��������� 

app.Run();




