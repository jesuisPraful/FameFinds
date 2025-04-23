using FameFindsDAL;
using FameFindsDAL.Models;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

<<<<<<< HEAD
        builder.Services.AddScoped<FameFindsRepository>();
        builder.Services.AddScoped<FameFindsContext>();
=======
        builder.Services.AddScoped<FameFindsRepository>(/*new FameFindsRepository(new FameFindsContext(new DbContextOptions<FameFindsContext>()))*/);
        builder.Services.AddScoped<FameFindsContext>();
        //builder.Services.AddDbContext<FameFindsContext>(options =>
        //    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionName")));
>>>>>>> 79edbd7b9726d525bb991b08f7add7aef6d0f4e1

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
    }
}