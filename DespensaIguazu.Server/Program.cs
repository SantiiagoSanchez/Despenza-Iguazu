using DespensaIguazu.BD.Data;
using DespensaIguazu.Server.Repositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(op => op.UseSqlServer("name=conn1"));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
builder.Services.AddScoped<IMarcaRepositorio, MarcaRepositorio>();
builder.Services.AddScoped<IUnidadRepositorio, UnidadRepositorio>();
builder.Services.AddScoped<IProductoRepositorio, ProductoRepositorio>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
