using HMS.Persistence.Context;
using HMS.Persistence.Extension_Methods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentityServices();
var app = builder.Build();
using(var scope = app.Services.CreateScope())
{
    var instance = scope.ServiceProvider.GetRequiredService<AppDbContextInitialiser>();
    await instance.InitialiseAsync();
    await instance.RoleSeedAsync();
}

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
