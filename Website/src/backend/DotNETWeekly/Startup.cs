namespace DotNETWeekly
{
    using Data;

    using DbUp;
    using Microsoft.AspNetCore.Authentication.JwtBearer;

    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            EnsureDatabase.For.SqlDatabase(connectionString);
            var upgrader = DeployChanges.To
                .SqlDatabase(connectionString, null)
                .WithScriptsEmbeddedInAssembly(System.Reflection.Assembly.GetExecutingAssembly())
                .WithTransaction()
                .LogToConsole()
                .Build();

            if (upgrader.IsUpgradeRequired())
            {
                upgrader.PerformUpgrade();
            }
            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddAzureAdBearer(options => Configuration.Bind("AzureAd", options));
            services.AddScoped<IDataRepository, DataRepository>();
            services.AddMvc();
            services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen();
            services.AddCors(option => option.AddPolicy("CorsPolicy", builder=>
            builder.AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins(Configuration["Frontend"])));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
           /* if (environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }*/
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
