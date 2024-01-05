using FluentValidation;
using FluentValidation.AspNetCore;
using Localization.Test.API;
using Localization.Test.API.Helpers;
using Localization.Test.API.Middlewares;
using Localization.Test.API.Validators;
using Localization.Test.Common;
using Localization.Test.Common.Constants;
using Localization.Test.Common.Helpers;
using Localization.Test.Common.Models;
using Localization.Test.Common.RequestModels;
using Localization.Test.Repository.DbContexts;
using Localization.Test.Repository.UnitOfWork;
using Localization.Test.Repository.UnitOfWork.Interfaces;
using Localization.Test.Service;
using Localization.Test.Service.Extensions;
using Localization.Test.Service.Interfaces;
using Localization.Test.Service.Managers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Globalization;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

IJwtSettings jwtSettings = builder
    .Configuration
    .GetSection("JwtSettings").Get<JwtSettings>() ?? new JwtSettings();

JwtTokenManager
    .Configure(new JwtSettings
    {
        Audience = jwtSettings.Audience,
        Issuer = jwtSettings.Issuer,
        ExpiryInDays = jwtSettings.ExpiryInDays,
        SecurityKey = jwtSettings.SecurityKey,
    });

NotificationConfig notificationConfig = builder
    .Configuration
    .GetSection("NotificationConfig").Get<NotificationConfig>() ?? new NotificationConfig();

builder.Services.AddSingleton(notificationConfig);

EmailSettings emailSettings = builder
    .Configuration
    .GetSection("EmailSettings").Get<EmailSettings>() ?? new EmailSettings();

AppSettings appSettings = builder
    .Configuration
    .GetSection("AppSettings").Get<AppSettings>() ?? new AppSettings();

EmailManager
    .Configure(new EmailSettings
    {
        FromAddress = emailSettings.FromAddress,
        Host = emailSettings.Host,
        Password = emailSettings.Password,
        Port = emailSettings.Port,
        Username = emailSettings.Username
    });



// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<LocalizationDbContext>(item => item.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDatabase")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = jwtSettings.Issuer,
                       ValidAudience = jwtSettings.Audience,
                       IssuerSigningKey = new SymmetricSecurityKey(
                           Encoding.UTF8.GetBytes(
                               jwtSettings.SecurityKey ?? string.Empty)
                           )
                   };
               });

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("dashboard.read", p => p.RequireClaim("scope", "dashboard.read"));

    o.AddPolicy("users.read", p => p.RequireClaim("scope", "users.read"));
    o.AddPolicy("users.write", p => p.RequireClaim("scope", "users.write"));
    o.AddPolicy("users.delete", p => p.RequireClaim("scope", "users.delete"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddSingleton<LanguageService>();

builder.Services.AddScoped<IValidator<RegistrationRequest>, RegistrationValidator>();
builder.Services.AddScoped<IValidator<UpdateUserRequest>, UpdateUserValidator>();

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IdentityResolver>();

#region Localization
builder.Services.AddLocalization(option => option.ResourcesPath = Keys.ResourcesString);
builder.Services.Configure<RequestLocalizationOptions>(
    options =>
    {
        var supportedCultures = new List<CultureInfo>
        {
            new CultureInfo(Keys.DefaultCulture),
            new CultureInfo(Keys.GermanCulture)
        };

        options.DefaultRequestCulture = new RequestCulture(culture: Keys.DefaultCulture, uiCulture: Keys.DefaultCulture);
        options.SupportedCultures=supportedCultures;
        options.SupportedUICultures=supportedCultures;
        //options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
    });
#endregion


builder.Services.AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = c =>
        {
            var errors = string.Join('\n', c.ModelState.Values.Where(v => v.Errors.Count > 0)
              .SelectMany(v => v.Errors)
              .Select(v => v.ErrorMessage));

            return new BadRequestObjectResult(ResponseObject<object>.Build().setIsError(true).setMessage(errors));
        };
    });


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(Keys.V1, new OpenApiInfo
    {
        Title = Keys.LocalizationAPI,
        Version = Keys.V1
    });
    c.AddSecurityDefinition(Keys.TOKEN, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = Keys.JWTTokenRequired,
        Name = Keys.TOKEN,
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = Keys.TOKEN
                                }
                            },
                            new string[] { }
                        }
        });
    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
    var xmlFile = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
    var xmlPath = Path.Combine(baseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();
app.ApplyMigration();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", $"{Keys.LocalizationAPI} {Keys.V1}"));
}

app.UseMiddleware<RequestResponseMiddleware>();

app.UseHttpsRedirection();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<CultureMiddleware>();

app.MapControllers();
app.Run();
