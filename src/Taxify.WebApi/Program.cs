using Microsoft.EntityFrameworkCore;
using Serilog;
using Taxify.DataAccess.Contexts;
using Taxify.WebApi.Extensions;
using Taxify.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
    
builder.Services.AddCustomServices();

builder.Services.AddJwt(builder.Configuration);

builder.Services.AddDbContext<TaxifyDbContext>(options => 
                        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();