using SerilLogTest.Infrastructures.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
SerilLogHelper.ConfigureSerilLogger(config);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// SerilLog 
builder.Services.AddSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging(opts => opts.EnrichDiagnosticContext = SerilLogHelper.EnrichMethod); 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
