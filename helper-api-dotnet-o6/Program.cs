using helper_api_dotnet_o6.Configure;
using helper_api_dotnet_o6_investimento.Domain.Interfaces;
using helper_api_dotnet_o6_investimento.Repositories;
using helper_api_dotnet_o6_investimento.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomServices();


//builder.Services.AddMemoryCache();
//builder.Services.AddScoped<IInvestimentoService, InvestimentoService>();
//builder.Services.AddScoped<IBancoCentralApiRepository, BancoCentralApiRepository>();
//builder.Services.AddScoped<IGerenciamentoCacheRepository, GerenciamentoCacheRepository>();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowInvestimento",
//        builder =>
//        {
//            builder.AllowAnyOrigin()
//                   .AllowAnyMethod()
//                   .AllowAnyHeader();
//        });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowInvestimento");

app.UseAuthorization();

app.MapControllers();

app.Run();
