using AutoMapper;
using Framework.Db.Base;
using Framework.Db.Mongo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;
using System.Threading.Tasks;
using ToDoListWebAPI.Helpers;
using ToDoListWebAPI.Models.EntityModels;
using ToDoListWebAPI.Services;
using ToDoListWebAPI.Services.Authentication;
using ToDoListWebAPI.Services.ToDo;

namespace ToDoListWebAPI
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors();
      services.AddAutoMapper();
      services.AddSingleton<IGetToDoObjectService, GetToDoObjectService>();
      services.AddSingleton<IAddToDoObjectService, AddToDoObjectService>();
      services.AddSingleton<IDeleteToDoObjectService, DeleteToDoObjectService>();
      services.AddSingleton<IUpdateToDoObjectService, UpdateToDoObjectService>();
      services.AddTransient<IDBInterface, MongoDbActionService>();
      services.AddSingleton<IUserService, UserService>();
      services.AddSingleton<IPasswordHashService, PasswordHashService>();
      services.AddSingleton<IDbOperations<User>, MongoOperations<User>>();
      services.AddSingleton<IDbOperations<ToDoEntity>, MongoOperations<ToDoEntity>>();
      services.Configure<UserConfig>(Configuration.GetSection("MongoDatabaseSettings")
                                                  .GetSection("User"));
      services.Configure<ToDoConfig>(Configuration.GetSection("MongoDatabaseSettings")
                                                  .GetSection("ToDo"));


      // Register the Swagger generator, defining 1 or more Swagger documents
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
      });

      // configure strongly typed settings objects
      var appSettingsSection = Configuration.GetSection("AppSettings");
      services.Configure<AppSettings>(appSettingsSection);

      // configure jwt authentication
      var appSettings = appSettingsSection.Get<AppSettings>();
      var key = Encoding.ASCII.GetBytes(appSettings.Secret);
      services.AddAuthentication(x =>
          {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
          })
          .AddJwtBearer(x =>
          {
            x.Events = new JwtBearerEvents
            {
              OnTokenValidated = context =>
                    {
                    var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                    var userId = context.Principal.Identity.Name;
                    var user = userService.GetById(userId);
                    if (user == null)
                    {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                    }
                    return Task.CompletedTask;
                  }
            };
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
              ValidateIssuerSigningKey = true,
              IssuerSigningKey = new SymmetricSecurityKey(key),
              ValidateIssuer = false,
              ValidateAudience = false
            };
          });
      
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseAuthentication();
      if (env.IsDevelopment())
      {
        // Enable middleware to serve generated Swagger as a JSON endpoint.
        app.UseSwagger();
        app.UseDeveloperExceptionPage();
        app.UseSwaggerUI(c =>
                {
                  c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseStaticFiles();
      app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");

        routes.MapSpaFallbackRoute(
                  name: "spa-fallback",
                  defaults: new { controller = "Home", action = "Index" });
      });
    }
  }
}
