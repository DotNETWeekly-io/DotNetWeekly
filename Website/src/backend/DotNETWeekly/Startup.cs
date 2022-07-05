namespace DotNETWeekly
{
    using Data;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    using Options;

    using Services;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddAzureAdBearer(options => Configuration.Bind("AzureAd", options));
            services.AddMvc();
            services.AddEndpointsApiExplorer();
            services.AddCors(option => option.AddPolicy("CorsPolicy", builder=>
            builder.AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins(Configuration["Frontend"])));
            services.AddMemoryCache();
            services.AddHttpClient();
            services.Configure<EpisodeSyncOption>(Configuration.GetSection("EpisodeSync"));
            services.Configure<CosmosDbOptions>(Configuration.GetSection("CosmosDb"));
            services.AddSingleton<IEpisodeService, CosmosDbEpisodeService>();
            services.AddOptions();
            services.AddSingleton(
                typeof(IOptionsSnapshot<>),
                typeof(OptionsManager<>)
            );
            services.AddHostedService<UpdateEpisodeHostedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
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
