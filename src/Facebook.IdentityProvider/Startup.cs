using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Facebook.Domain;
using Facebook.Repository;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Facebook.IdentityProvider
{
    public class IdentityWithAdditionalClaimsProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<User> _claimsFactory;
        private readonly UserManager<User> _userManager;

        public IdentityWithAdditionalClaimsProfileService(UserManager<User> userManager,
            IUserClaimsPrincipalFactory<User> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();

            var user = await _userManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(user);

            var claims = principal.Claims.ToList();

            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            claims.Add(new Claim(JwtClaimTypes.GivenName, user.UserName));


            claims.Add(new Claim(JwtClaimTypes.Role, "dataEventRecords.admin"));
            claims.Add(new Claim(JwtClaimTypes.Role, "dataEventRecords.user"));
            claims.Add(new Claim(JwtClaimTypes.Role, "dataEventRecords"));
            claims.Add(new Claim(JwtClaimTypes.Scope, "dataEventRecords"));

            claims.Add(new Claim(IdentityServerConstants.StandardScopes.Email, user.Email));


            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }

    }



    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
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
            // Add framework services.
            services.AddMvc();
            const string connection = @"Server=localhost;Database=Facebook;Trusted_Connection=True;";
            services.AddDbContext<FacebookDatabaseContext>(options => options.UseSqlServer(connection));

            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<FacebookDatabaseContext>()
                    .AddDefaultTokenProviders();

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


            services.AddTransient<IProfileService, IdentityWithAdditionalClaimsProfileService>();

            // Adds IdentityServer.
            // The AddTemporarySigningCredential extension creates temporary key material for signing tokens on every start.
            // Again this might be useful to get started, but needs to be replaced by some persistent key material for production scenarios.
            // See the cryptography docs for more information: http://docs.identityserver.io/en/release/topics/crypto.html#refcrypto
            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddProfileService<IdentityWithAdditionalClaimsProfileService>()

                .AddAspNetIdentity<User>(); // IdentityServer4.AspNetIdentity.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors("AllowAllOrigins");
            app.UseDeveloperExceptionPage();
            app.UseIdentity();
            app.UseIdentityServer();
            app.UseMvc();
        }
    }
}
