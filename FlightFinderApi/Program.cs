
using FlightFinderApi.Services;

namespace FlightFinderApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSubdomains", policy =>
                {
                    policy.WithOrigins("https://patrikpercinic.com")
                          .SetIsOriginAllowedToAllowWildcardSubdomains()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();

            builder.Services.AddMemoryCache();

            builder.Services.AddScoped<IFlightService, FlightService>();
            builder.Services.AddScoped<IIataService, IataService>();
            builder.Services.AddHttpClient<TokenService>();
            builder.Services.AddSingleton<TokenService>();
            builder.Services.AddHttpClient<IataService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowSubdomains");

            app.UseRouting();


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.Run();
        }
    }
}
