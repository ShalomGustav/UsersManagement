# UsersManagement

**UsersManagement** — это ASP.NET Core приложение для управления пользователями, включающее функции аутентификации и работы с профилями, а также дополнительный функционал.

## Структура проекта

- [**Controllers**](#controllers): Контроллеры для обработки HTTP-запросов и взаимодействия с клиентом.
- [**Migrations**](#migrations): Миграции базы данных для создания и обновления схемы.
- [**Models**](#models): Модели данных, используемые в приложении.
- [**Repositories**](#repositories): Репозитории для управления доступом к данным и абстракции работы с базой данных.
- [**Services**](#services): Бизнес-логика приложения и настройки для аутентификации.
- [**Program.cs**](#programcs): Точка входа в приложение, настройка сервисов и конфигурация аутентификации и авторизации.

## Подключение к базе данных 
- [**Подключение к базе данных**](#подключение-к-базе-данных)
- [**Строка подключения**](#строка-подключения)

## Конфигурация Swagger и JWT аутентификации
- [**Конфигурация и аутентификация**](#конфигурация-swagger-и-JWT-аутентификации)
- [**Настройка Swagger**](#настройка-swagger)
- [**Настройка аутентификации и авторизации**](#настройка-аутентификации-и-авторизации)
- [**Применение миграций**](#применение-миграций)
- [**Использование Swagger и включение HTTPS**](#использование-swagger-и-включение-https)
- [**Маршрутизация и проверка токена**](#маршрутизация-и-проверка-токена)
- 
## Controllers

Контроллеры, обрабатывающие запросы API и связывающие клиентскую часть с бизнес-логикой.

Содержит следующие классы:

- [**AuthController**](#authcontroller): Обеспечивает аутентификацию пользователей и генерацию JWT токенов.
- [**UserController**](#usercontroller): Отвечает за операции с данными пользователей (получение, создание, обновление, удаление).
- [**WeatherForecastController**](#weatherforecastcontroller): Пример контроллера, используемый для демонстрации базового API.

### AuthController

`AuthController` — контроллер, отвечающий за аутентификацию пользователей. Предоставляет маршруты для входа в систему, проверки учетных данных пользователя и выдачи токенов.

### UserController

`UserController` — контроллер, отвечающий за операции с данными пользователей. Поддерживает получение данных пользователя по логину или ID, создание, обновление и удаление пользователей. Использует `UserService` для выполнения бизнес-логики.

### WeatherForecastController

`WeatherForecastController` — демонстрационный контроллер, предоставляющий базовые данные о погоде. Используется как пример API и не связан с основной функциональностью приложения.

## Migrations

Миграции базы данных для создания и обновления схемы, управляемые с помощью Entity Framework Core. Помогают поддерживать структуру базы данных в актуальном состоянии при добавлении новых моделей или изменении существующих.

Содержит следующие классы:

- **Initial Migration**: Начальная миграция, создающая основные таблицы в базе данных.
- **UserDbContextModelSnapshot**: Снимок модели базы данных, используемый для отслеживания изменений в модели и синхронизации с базой данных.

## Models

Модели данных, описывающие структуру информации, с которой работает приложение.

Содержит следующие классы:

- [**User**](#user): Модель, представляющая данные пользователя, такие как логин, пароль, имя, email и статус администратора.
- [**UserEntity**](#userentity): Сущность пользователя для работы с базой данных, соответствует модели `User` и служит для ORM.

### User

`User` — модель, представляющая данные пользователя в приложении. Содержит основные свойства, такие как логин, пароль, имя, email и статус администратора.

### UserEntity

`UserEntity` — сущность пользователя, используемая для хранения данных в базе данных. Отражает модель `User` и используется для операций с ORM.

## Repositories

Репозитории для управления доступом к данным и абстракции работы с базой данных. Позволяют выполнять CRUD операции и обеспечивают уровень абстракции между бизнес-логикой и данными.

Содержит следующие компоненты:

- [**Common**](#common): Общие утилиты и абстракции для работы с репозиториями.
- [**UserRepository**](#userrepository): Реализация репозитория для работы с данными пользователей.

### Common

Общие утилиты и абстракции для работы с репозиториями:

- **AbstractTypeFactory**: Фабрика для создания экземпляров абстрактных типов.
- **CrudService**: Базовый сервис для выполнения CRUD операций.
- **DbContextRepositoryBase**: Базовый класс для репозиториев, использующих DbContext.
- **DbContextUnitOfWork**: Реализация паттерна Unit of Work для управления транзакциями.
- **Entity**: Базовый класс сущности с общими свойствами.
- **IDateEntity** и **IEntity**: Интерфейсы для сущностей.
- **IRepository**: Общий интерфейс для всех репозиториев.
- **IUnitOfWork**: Интерфейс для паттерна Unit of Work.
- **ReflectionUtility**: Утилита для работы с рефлексией.

### UserRepository

`UserRepository` — репозиторий, реализующий доступ к данным пользователей в базе данных. Определяет методы для получения данных пользователей по различным критериям.

## Services

Бизнес-логика приложения и настройки для аутентификации. Сервисы управляют основными операциями и предоставляют функции для работы с данными пользователей.

Содержит следующие классы:

- [**AuthOptions**](#authoptions): Настройки для конфигурации JWT токенов, включая ключ шифрования, издателя и аудиторию.
- [**UserService**](#userservice): Сервис для выполнения операций, связанных с пользователями, включая аутентификацию и CRUD операции.

### AuthOptions

`AuthOptions` — класс, содержащий настройки для конфигурации JWT токенов

### UserService

`UserService` — сервис, предоставляющий бизнес-логику для управления пользователями, включая аутентификацию и CRUD операции. Взаимодействует с `UserRepository` для выполнения операций с данными.

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
  ```csharp
  builder.Services.AddDbContext<UserDbContext>((provider, options) =>
  {
      options.UseSqlServer("Data Source=(local);Initial Catalog=UserManagement;Persist Security Info=True;User ID=test;Password=test;MultipleActiveResultSets=True;Connect Timeout=30;TrustServerCertificate=True");
  });
  ```
`ID` и `Password` указаны `test` по умолчанию.

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
`builder.Services.AddSwaggerGen` — добавляет и настраивает `Swagger` для генерации документации по `API`.

`x.SwaggerDoc("v1", new OpenApiInfo { Title = "Users Management API", Version = "v1" });` — Создаёт новую спецификацию `API` с версией `"v1"`.

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
