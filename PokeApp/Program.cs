using PokeApp.AutoMapperProfile;
using PokeApp.Services;
using PokeApp.Services.Interfaces;
using StackExchange.Redis;

namespace sciences_nation_back
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            // Register services singleton
            builder.Services.AddScoped<IPokeService, PokeService>();
            builder.Services.AddScoped<IRedisService, RedisService>();

            // Configure AutoMapper
            builder.Services.AddAutoMapper(typeof(PokemonProfile));

            // Configure Redis connection
            var redisConnectionString = builder.Configuration.GetConnectionString("RedisConnection") ?? "localhost:6379";
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect(redisConnectionString);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Use CORS middleware
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}