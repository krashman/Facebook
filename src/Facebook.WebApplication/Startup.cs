using System;
using System.Linq;
using Facebook.Domain;
using Facebook.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Facebook.WebApplication
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddPolicy("AllowAllOrigins",
                  builder =>
                  {
                builder
                          .AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
              });
      });

      services.AddMvc();

      // Add framework services. 
      const string connection = @"Server=localhost;Database=Facebook;Trusted_Connection=True;";
      services.AddDbContext<FacebookDatabaseContext>(options => options.UseSqlServer(connection));

      services.AddIdentity<IdentityUser, IdentityRole>()
              .AddEntityFrameworkStores<FacebookDatabaseContext>()
              .AddDefaultTokenProviders();


      // Identity options.
      services.Configure<IdentityOptions>(options =>
      {
              // Password settings.
              options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = false;
      });

      // Claims-Based Authorization: role claims.
      services.AddAuthorization(options =>
      {
              // Policy for dashboard: only administrator role.
              options.AddPolicy("Manage Accounts", policy => policy.RequireClaim("role", "administrator"));
              // Policy for resources: user or administrator role. 
              options.AddPolicy("Access Resources", policyBuilder => policyBuilder.RequireAssertion(
                      context => context.User.HasClaim(claim => (claim.Type == "role" && claim.Value == "user")
                         || (claim.Type == "role" && claim.Value == "administrator"))
                  )
              );
      });

      services.AddSingleton<IUserRepository, UserRepository>();
      services.AddTransient<IDbService, DbService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      var angularRoutes = new[]
      {
                "/home",
                "/about",
                "/register"
            };

      app.Use(async (context, next) =>
      {
        if (context.Request.Path.HasValue && null != angularRoutes.FirstOrDefault(
                      (ar) => context.Request.Path.Value.StartsWith(ar, StringComparison.OrdinalIgnoreCase)))
        {
          context.Request.Path = new PathString("/");
        }

        await next();
      });

      app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
      {
        //TODO: Get from config for different environments
        Authority = "http://localhost:5001/",
        ApiName = "WebAPI",

        RequireHttpsMetadata = false
      });

      app.UseCors("AllowAllOrigins");
      app.UseDefaultFiles();
      app.UseStaticFiles();
      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
      });
      app.PopulateDb();

    }
  }

  public static class DbExtensions
  {
    // Adds the extension method.
    public static async void PopulateDb(this IApplicationBuilder app)
    {
      // Uses app.ApplicationServices to access to the DI container(the IServiceProvider),
      // and gets the instance of the DbService to populate db.
      var dbService = app.ApplicationServices.GetRequiredService<IDbService>();
      await dbService.populate();
    }
  }
}