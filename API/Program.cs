




using System.Collections.Immutable;
using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext<StoreContext>(options => {options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //middleware
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(opt => {
        opt.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
try{
    context.Database.Migrate();
    DbInitializer.Initialize(context);
            
}
catch (Exception ex) 
{
    logger.LogError(ex, "problem migraring data");
    }

app.Run();