# UsersManagement
**Автор:** Фатихов Максим Сергеевич

## Описание
**UsersManagement** — это ASP.NET Core приложение для управления пользователями, включающее функции аутентификации и работы с профилями, а также дополнительный функционал.

- [**Требования**](#требования)
- [**Структура проекта**](#структура-проекта)
  - [**Controllers**](#controllers)
  - [**Migrations**](#migrations)
  - [**Models**](#models)
  - [**Repositories**](#repositories)
  - [**Common**](#common)
  - [**Services**](#services)
  - [**Program.cs**](#programcs)
- [**Подключение к базе данных**](#подключение-к-базе-данных)
  - [**Строка подключения**](#строка-подключения)
- [**Конфигурация и аутентификация**](#конфигурация-и-аутентификация)
  - [**Настройка Swagger**](#настройка-swagger)
  - [**Настройка аутентификации и авторизации**](#настройка-аутентификации-и-авторизации)
  - [**Применение миграций**](#применение-миграций)
  - [**Использование Swagger и включение HTTPS**](#использование-swagger-и-включение-https)
  - [**Маршрутизация и проверка токена**](#маршрутизация-и-проверка-токена)

## Требования

- .NET 6.0
- SQL Server

Проект использует следующие библиотеки и пакеты:

- **`Microsoft.AspNetCore.Authentication.JwtBearer`** (версия 8.0.6) — для поддержки аутентификации через JWT.
- **`Swashbuckle.AspNetCore`** (версия 6.4.0) — для интеграции Swagger в приложение ASP.NET Core.
- **`Microsoft.EntityFrameworkCore.Design`** (версия 6.0.18) — для создания и работы с миграциями.
- **`Microsoft.EntityFrameworkCore.SqlServer`** (версия 6.0.18) — провайдер Entity Framework Core для работы с SQL Server.
- **`Microsoft.EntityFrameworkCore.Tools`** (версия 6.0.18) — инструменты для работы с Entity Framework Core, включая создание миграций.

## Структура проекта

Проект `UsersManagement` состоит из нескольких ключевых компонентов, каждый из которых отвечает за отдельные аспекты работы приложения.

### Controllers
- `AuthController.cs` — контроллер для аутентификации пользователей. Обрабатывает запросы на вход и выдачу JWT-токенов.
- `UserController.cs` — контроллер для управления пользователями. Обеспечивает CRUD операции, такие как создание, получение, обновление и удаление пользователей.
- `WeatherForecastController.cs` — демонстрационный контроллер, предоставляющий пример простого API для получения данных о погоде. Используется только в учебных целях и не связан с основной функциональностью приложения.

### Migrations
- **Миграции** используются для управления схемой базы данных с помощью Entity Framework Core. Они позволяют автоматически применять изменения структуры базы данных при обновлении моделей.
  - `20240703134713_Initial.cs` — начальная миграция, создающая основные таблицы и структуры базы данных.
  - `UserDbContextModelSnapshot.cs` — снимок модели базы данных, используемый для отслеживания изменений и синхронизации с базой данных.

### Models
- `User.cs` — модель, представляющая данные пользователя в приложении. Содержит такие свойства, как логин, пароль, имя, email и статус администратора.
- `UserEntity.cs` — сущность, отражающая модель `User` и используемая для взаимодействия с базой данных. Включает аналогичные свойства и методы для преобразования в `User`.

### Repositories
Каталог **Repositories** отвечает за управление данными и взаимодействие с базой данных, обеспечивая абстракцию для CRUD операций и поддержку паттернов Repository и Unit of Work. Он содержит следующие компоненты:

### Common 

Подкаталог, содержащий общие классы и интерфейсы, используемые для создания репозиториев:
- **Repositories**
  - **Common** 
    - `AbstractTypeFactory.cs` — Фабрика для создания экземпляров абстрактных типов. Предоставляет возможность регистрации типов и их создание по имени.
    - `CrudService.cs` — Базовый класс для сервисов, поддерживающих CRUD операции. Обеспечивает стандартные методы для создания, чтения, обновления и удаления сущностей.
    - `DbContextRepositoryBase.cs` — Базовый класс для репозиториев, работающих с `DbContext`. Упрощает создание специфичных репозиториев для Entity Framework.
    - `DbContextUnitOfWork.cs` — Реализация паттерна Unit of Work для работы с транзакциями, используемая для обеспечения целостности данных при выполнении нескольких операций.
    - `Entity.cs` — Базовый класс сущностей, содержащий общее свойство `Id` для всех объектов, хранимых в базе данных.
    - `IDateEntity.cs` — Интерфейс, определяющий методы для преобразования сущностей, связанных с датами, между различными моделями.
    - `IEntity.cs` — Интерфейс, определяющий обязательное свойство `Id` для всех сущностей.
    - `IRepository.cs` — Интерфейс, определяющий основные методы для всех репозиториев, включая добавление, обновление, удаление и получение сущностей.
    - `IUnitOfWork.cs` — Интерфейс для реализации паттерна Unit of Work, отвечающий за фиксирование изменений в базе данных.
    - `ReflectionUtility.cs` — Утилита для работы с рефлексией, предоставляющая методы для работы с атрибутами и наследованием типов. Помогает находить свойства и методы в классах.

- `DesignTimeDbContextFactory.cs` — Фабрика, необходимая для создания экземпляра `UserDbContext` в режиме разработки и для применения миграций.

- `IUserRepository.cs` — Интерфейс, определяющий методы, специфичные для работы с данными пользователей, такие как получение пользователя по логину и ID.

- `UserDbContext.cs` — Класс контекста базы данных для Entity Framework, представляющий соединение с базой данных и настройку сущностей, используемых в проекте.

- `UserRepository.cs` — Реализация `IUserRepository`, предоставляющая методы для работы с данными пользователей, такие как поиск пользователей по логину или ID. Использует `UserDbContext` для доступа к данным в базе.

### Services
- `AuthOptions.cs` — класс для настройки параметров JWT аутентификации, таких как издатель, потребитель и ключ шифрования.
- `UserService.cs` — сервис, содержащий бизнес-логику для работы с пользователями, включая аутентификацию и операции CRUD. Использует `UserRepository` для доступа к данным пользователей.

### Конфигурационные файлы
- `appsettings.json` — файл конфигурации приложения, содержащий настройки, такие как строка подключения к базе данных и другие параметры.

### Program.cs
- Основная точка входа приложения, в которой происходит настройка сервисов, добавление контроллеров, настройка аутентификации и авторизации, подключение базы данных и Swagger для документирования API.

### Дополнительные файлы
- `UsersManagement.http` — файл для тестирования HTTP-запросов к API.
- `WeatherForecast.cs` — демонстрационный класс для работы с `WeatherForecastController`, используемый в учебных целях.

## Program.cs

`Program.cs` — основной файл, отвечающий за настройку и запуск приложения. Содержит следующие настройки:

- **Добавление сервисов**: Подключение контроллеров, `UserService`, базы данных `UserDbContext`, а также репозитория `UserRepository`.
- **Swagger**: Настраивает Swagger для документирования API, включая схему авторизации JWT для удобства тестирования.
- **Аутентификация и авторизация**: Настраивает JWT аутентификацию с проверкой издателя и потребителя токена, а также сроком его действия.
- **Конфигурация приложений**: Добавляет маршруты для контроллеров, а также дополнительные маршруты `/login` и `/data`, демонстрирующие использование JWT токенов для авторизации.
 
### Подключение к базе данных 

Приложение использует **SQL Server** для хранения данных пользователей. Подключение к базе данных настраивается в файле `Program.cs`:

## Строка подключения

- В `Program.cs` используется `UseSqlServer`, где строка подключения содержит параметры подключения к вашей базе данных:
- 
  ```csharp
  builder.Services.AddDbContext<UserDbContext>((provider, options) =>
  {
      options.UseSqlServer("Data Source=(local);Initial Catalog=UserManagement;Persist Security Info=True;User ID=test;Password=test;MultipleActiveResultSets=True;Connect Timeout=30;TrustServerCertificate=True");
  });
  ```
> **Обратите внимание:** В строке подключения по умолчанию используются `User ID` и `Password`, заданные как `test`.
> Эти данные предназначены исключительно для тестирования и должны быть заменены на реальные учетные данные перед использованием приложения в рабочей среде.

## Конфигурация и аутентификация

Приложение использует `Swagger` для документирования `API` и `JWT` для аутентификации. Ниже описаны основные шаги настройки.

## Настройка Swagger
`Swagger` используется для документирования и тестирования `API`. Конфигурация включает определение схемы авторизации с использованием токенов `JWT`:

```csharp
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

```
- `builder.Services.AddSwaggerGen` — добавляет и настраивает `Swagger` для генерации документации по `API`.

- `x.SwaggerDoc("v1", new OpenApiInfo { Title = "Users Management API", Version = "v1" });` — Создаёт новую спецификацию `API` с версией `"v1"`.

 - `Title` указывает заголовок документации `API`, здесь — `"Users Management API"`.

 - `Version` задаёт версию документации, которая здесь равна `"v1"`.


```csharp
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

```
`"Bearer"` — Уникальное имя для этой схемы безопасности.

`new OpenApiSecurityScheme {...}` — Определяет параметры для использования `Bearer` токенов в API.

 - `Description` — Описание, которое будет показано в `Swagger` для пояснения, как использовать токен (в этом случае — вставка токена в заголовок `Authorization`).

 - `Name = "Authorization"` — Имя заголовка, который будет использоваться для передачи токена.

 - `In = ParameterLocation.Header` — Указывает, что токен должен быть передан в заголовке запроса.

 - `Type = SecuritySchemeType.ApiKey` — Указывает тип схемы безопасности, где ApiKey означает, что токен будет передан в заголовке запроса.

 - `Scheme = "Bearer"` — Указывает, что тип схемы — `Bearer`, который обычно используется для JWT токенов.

```csharp
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
```

 - `x.AddSecurityRequirement(new OpenApiSecurityRequirement {...});` — Определяет требование безопасности, которое будет применяться к защищённым эндпоинтам.

## Настройка аутентификации и авторизации

JWT аутентификация и авторизация настраиваются в `Program.cs`. Здесь указываются параметры проверки токена: издатель, потребитель, срок действия и ключ для подписи.

```scharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x => x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = AuthOptions.ISSUER,
        ValidateAudience = true,
        ValidAudience = AuthOptions.AUDIENCE,
        ValidateLifetime = true,
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true,
    });

builder.Services.AddAuthorization();
```
### Параметры проверки токена:

- `ValidateIssuer = true` — указывает, что необходимо проверять издателя токена. Издатель помогает убедиться, что токен выпущен доверенным сервером.

- `ValidIssuer = AuthOptions.ISSUER` — задаёт допустимого издателя, который ожидается в токене. Значение берётся из `AuthOptions.ISSUER` — это строка, представляющая имя или идентификатор доверенного издателя (сервер, который выпустил токен).

- `ValidateAudience = true` — указывает, что необходимо проверять потребителя токена (аудиторию). Это гарантирует, что токен предназначен для данного приложения.

- `ValidAudience = AuthOptions.AUDIENCE` — задаёт допустимую аудиторию для токена. Значение берётся из `AuthOptions.AUDIENCE` — это строка, представляющая идентификатор приложения, которому предназначен токен.

- `ValidateLifetime = true` — указывает, что необходимо проверять срок действия токена. Это позволяет отвергнуть истёкшие токены.

- `IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()` — задаёт ключ для проверки подписи токена. Этот ключ позволяет приложению убедиться, что токен не был изменён. `AuthOptions.GetSymmetricSecurityKey()` — это метод, который возвращает симметричный ключ для подписи токенов (обычно это секретный ключ, закодированный в base64).

- `ValidateIssuerSigningKey = true` — указывает, что необходимо проверять ключ подписи токена. Это позволяет удостовериться, что токен действительно подписан доверенным сервером, а не кем-то подделан.

### Авторизация

```csharp
builder.Services.AddAuthorization();
```

`builder.Services.AddAuthorization()` — добавляет и настраивает службу авторизации в приложении. 

## Применение миграций

Код ниже автоматически применяет миграции при запуске приложения, обновляя структуру базы данных в соответствии с последними изменениями моделей:

```csharp
var app = builder.Build();
using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
var dbContext = scope.ServiceProvider.GetService<UserDbContext>();
dbContext.Database.Migrate();
```
#### Описание:

- Создание приложения: `var app = builder.Build();` — Создаёт экземпляр приложения.

- Создание области (scope): `CreateScope()` — Создаёт область (scope) для службы, чтобы использовать UserDbContext с ограниченным временем жизни (Scoped).

- Получение контекста базы данных: `scope.ServiceProvider.GetService<UserDbContext>();` — Извлекает UserDbContext из службы для взаимодействия с базой данных.

- Применение миграций: `dbContext.Database.Migrate();` — Применяет все неприменённые миграции к базе данных, синхронизируя её структуру с текущими моделями.

> ⚠️ **Важно**: Этот код подходит для разработки и тестирования. В продакшн-среде автоматическое применение миграций может быть рискованным.  
> В таких случаях миграции обычно применяются вручную для более тщательного контроля.

### Использование Swagger и включение HTTPS

Swagger используется для документирования и тестирования API, и он включается только в режиме разработки. В этом же блоке настраивается HTTPS редирект, чтобы повысить безопасность приложения.

```csharp
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
```
### Описание:

- **Swagger**:
  - `app.UseSwagger()` — Включает генерацию документации Swagger.
  - `app.UseSwaggerUI()` — Включает интерфейс Swagger UI, который предоставляет удобный способ тестирования API.
  - Эти вызовы ограничены средой разработки, чтобы избежать их использования в продакшн-среде.

- **HTTPS редирект**:
  - `app.UseHttpsRedirection()` — Перенаправляет все HTTP-запросы на HTTPS, добавляя дополнительный уровень безопасности.

### Маршрутизация и проверка токена

Приложение поддерживает маршруты для генерации и проверки JWT токенов, что позволяет управлять доступом к защищённым ресурсам.

```csharp
app.Map("/login", (string username) =>
{
    var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
    var jwt = new JwtSecurityToken(
        issuer: AuthOptions.ISSUER,
        audience: AuthOptions.AUDIENCE,
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(10),
        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
    return new JwtSecurityTokenHandler().WriteToken(jwt);
});

app.Map("/data", [Authorize] () => new { message = "Token is valid" });
```
#### Описание:

- **Маршрут `/login`** — Генерирует JWT токен на основе имени пользователя.
  - Включает `claims`, содержащие имя пользователя (`username`).
  - Токен подписывается с использованием `SigningCredentials`, что гарантирует его подлинность.
  - Токен истекает через 10 минут, обеспечивая ограниченный срок действия.

- **Маршрут `/data`** — Защищённый маршрут, доступный только при наличии действующего JWT токена.
  - `[Authorize]` — Требует наличия действительного токена для доступа к этому маршруту.
  - Возвращает сообщение `{ message = "Token is valid" }` при успешной аутентификации.

> ⚠️ **Примечание**: Эти маршруты демонстрируют работу с JWT токенами и могут быть расширены для более сложных сценариев проверки и управления доступом.


