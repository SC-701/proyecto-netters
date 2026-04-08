using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using DA;
using DA.Repositorio;
using Flujo;
using Microsoft.AspNetCore.Authentication.JwtBearer;  
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Autorizacion.Middleware;                     

var builder = WebApplication.CreateBuilder(args);

var tokenConfig = builder.Configuration.GetSection("Token").Get<TokenConfiguracion>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenConfig.Issuer,
            ValidAudience = tokenConfig.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                                           Encoding.UTF8.GetBytes(tokenConfig.key))
        };
    });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepositorioDapper, RepositorioDapper>();

//PARA CARRERA
builder.Services.AddScoped<ICarreraDA, CarreraDA>();
builder.Services.AddScoped<ICarreraFlujo, CarreraFlujo>();

//PARA CURSO
builder.Services.AddScoped<ICursoDA, CursoDA>();
builder.Services.AddScoped<ICursoFlujo, CursoFlujo>();

//PARA CARRERACURSO
builder.Services.AddScoped<ICarreraCursoDA, CarreraCursoDA>();
builder.Services.AddScoped<ICarreraCursoFlujo, CarreraCursoFlujo>();

//PARA ESCUELA
builder.Services.AddScoped<IEscuelaDA, EscuelaDA>();
builder.Services.AddScoped<IEscuelaFlujo, EscuelaFlujo>();

//PARA PERFIL
builder.Services.AddScoped<IPerfilDA, PerfilDA>();
builder.Services.AddScoped<IPerfilFlujo, PerfilFlujo>();

//PARA PERFIL X USUARIO
builder.Services.AddScoped<IPerfilxUsuarioDA, PerfilxUsuarioDA>();
builder.Services.AddScoped<IPerfilxUsuarioFlujo, PerfilxUsuarioFlujo>();

//PARA REQUISITOS
builder.Services.AddScoped<IRequisitosDA, RequisitosDA>();
builder.Services.AddScoped<IRequisitosFlujo, RequisitosFlujo>();

//PARA PLANIFICACION
builder.Services.AddScoped<IPlanificacionDA, PlanificacionDA>();
builder.Services.AddScoped<IPlanificacionFlujo, PlanificacionFlujo>();

// PARA CURSO-PLANIFICACION
builder.Services.AddScoped<ICursoPlanificacionDA, CursoPlanificacionDA>();
builder.Services.AddScoped<ICursoPlanificacionFlujo, CursoPlanificacionFlujo>();

//Para SEGURIDAD
builder.Services.AddTransient<Autorizacion.Abstracciones.Flujo.IAutorizacionFlujo,
                               Autorizacion.Flujo.AutorizacionFlujo>();
builder.Services.AddTransient<Autorizacion.Abstracciones.DA.ISeguridadDA,
                               Autorizacion.DA.SeguridadDA>();
builder.Services.AddTransient<Autorizacion.Abstracciones.DA.IRepositorioDapper,
                               Autorizacion.DA.Repositorios.RepositorioDapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.AutorizacionClaims();

app.UseAuthorization();

app.MapControllers();

app.Run();
