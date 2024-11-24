using Microsoft.EntityFrameworkCore;
using bioinsumos_asproc_backend.Models;
using bioinsumos_asproc_backend.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using bioinsumos_asproc_backend.Middlewares;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http.Features;
using Amazon.S3;

var builder = WebApplication.CreateBuilder(args);

// Configuración de Kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = long.MaxValue; // O el valor que necesites
});

// Configuración de los límites para uploads grandes
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue; // 2GB o el tamaño que necesites
});

// Agregar servicios al contenedor
builder.Services.AddControllers();

// Configuración de Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

// Configuración CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .WithExposedHeaders("Content-Range", "Accept-Ranges", "Content-Encoding", "Content-Length","Content-Disposition");
        });
});

// Configuración de la base de datos
var connectionString = builder.Configuration.GetConnectionString("devDbConnectionString");
builder.Services.AddDbContext<BioinsumosContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Configuración de Memcached
builder.Services.AddEnyimMemcached(options =>
{
    builder.Configuration.GetSection("Memcached").Bind(options);
});

// Configurar AWSOptions desde appsettings.json
var awsOptions = builder.Configuration.GetAWSOptions("AWS");
awsOptions.Credentials = new Amazon.Runtime.BasicAWSCredentials(
    builder.Configuration["AWS:AccessKey"], 
    builder.Configuration["AWS:SecretKey"]);

// Registrar el servicio de Amazon S3 con las opciones configuradas
builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddAWSService<IAmazonS3>();

// Configuración de servicios
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IPdfService, PdfService>();
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<IAWSService, AWSVideoService>();
builder.Services.AddScoped<IAWSService, AWSPdfService>();
builder.Services.AddScoped<IAWSService, AWSHandbookService>();
builder.Services.AddScoped<IAWSImageService, AWSImageService>();
builder.Services.AddScoped<IFaqService, FaqService>();
builder.Services.AddScoped<IHandbookService, HandbookService>();
builder.Services.AddScoped<ICertificationInformationService, CertificationInformationService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IWheaterAlertService, WheaterAlertService>();
builder.Services.AddScoped<IPriceService, PriceService>();
builder.Services.AddScoped<IProductService, ProductService>();

// Configuración de autenticación
var key = builder.Configuration.GetValue<string>("JwtSettings:key");
var keyBytes = Encoding.ASCII.GetBytes(key);

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
    };
});

var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP
var env = app.Environment.EnvironmentName;
Console.WriteLine($"Running in environment: {env}");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Middleware de blacklist de tokens
app.UseMiddleware<TokenBlacklistMiddleware>();

app.Run();
