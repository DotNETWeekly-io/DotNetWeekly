namespace DotNETWeekly
{
    using Data;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDataRepository, DataRepository>();
            services.AddMvc();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCors(option => option.AddPolicy("CorsPolicy", builder=>
            builder.AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins(Configuration["Frontend"])));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
