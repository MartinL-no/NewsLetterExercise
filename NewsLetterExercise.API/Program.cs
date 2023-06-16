using NewsLetterExercise.Core.ApplicationServices;
using NewsLetterExercise.Core.DomainModel;
using NewsLetterExercise.Core.DomainServices;
using NewsLetterExercise.Infrastructure.DataAccess.Repository;
using NewsLetterExercise.Infrastructure.EmailSender;

namespace NewsLetterExercise.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var value = builder.Configuration.GetConnectionString("NewsLetterExerciseDb");
            var connectionString = new ConnectionString(value);
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ISubscriptionModelRespository, SubscriptionModelRepository>();
            builder.Services.AddScoped<SubscriptionService>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();

            builder.Services.AddSingleton<ConnectionString>(connectionString);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}