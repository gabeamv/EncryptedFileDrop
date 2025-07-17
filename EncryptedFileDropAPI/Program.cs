
using Microsoft.EntityFrameworkCore;
using EncryptedFileDropAPI.Data;
using System.Text.Json;

namespace EncryptedFileDropAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Configure json serializer to automatically use camel case.
                    //options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.;
                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Register the database service to the container.
            builder.Services.AddDbContext<EncryptedFileDropContext>
                // Configure the options to use an in-memory database named 'FileDropDB'.
                (opt => opt.UseInMemoryDatabase("EncryptedFileDropDB"));

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
}
