using ApiConciertosMySql.Data;
using ApiConciertosMySql.Helpers;
using ApiConciertosMySql.Models;
using ApiConciertosMySql.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace ApiConciertosMySql;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        string miSecreto = HelperSecretManager.GetSecretAsync().Result;
        KeysModel model = JsonConvert.DeserializeObject<KeysModel>(miSecreto);
        services.AddSingleton<KeysModel>(model);
        services.AddTransient<RepositoryConciertos>();
        services.AddDbContext<ConciertosContext>
            (options => options.UseMySql(model.MySql,
            ServerVersion.AutoDetect(model.MySql)));

        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin", x => x.AllowAnyOrigin());
        });
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Api Conciertos MySql 2023",
                Version = "v1"
            });
        });
        services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCors(options => options.AllowAnyOrigin());

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint(
                url: "swagger/v1/swagger.json",
                name: "Api ExtraSlice");
            options.RoutePrefix = "";
        });

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}