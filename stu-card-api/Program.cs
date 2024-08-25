using Microsoft.EntityFrameworkCore;
using stu_card_api.Extentions;
using stu_card_api.interfaces;
using stu_card_api.Services;
using stu_card_api.StuDbContext;
using stu_card_entity_store.Store;

namespace stu_card_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StuCardDbContext>();
            builder.Services.AddMinio(builder.Configuration);
            builder.Services.Configure<MinioOptions>(builder.Configuration.GetSection("MinioOptions"));

            AddScop(builder.Services);

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

        private static void AddScop(IServiceCollection services)
        {
            services.AddScoped<DbContext, StuCardDbContext>();
            services.AddScoped(typeof(IEntityStore<>), typeof(EntityStore<>));
            services.AddScoped<IMinioService, MinioService>();
            services.AddScoped<IFileService, FileService>();
        }
    }
}