using ChatBot.Data;
using ChatBot.Data.Infrastructure;
using ChatBot.Data.Respositories;
using ChatBot.Infrastructure;
using ChatBot.Infrastructure.Mappings;
using ChatBot.Model.Models;
using ChatBot.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System;
using System.Globalization;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using ChatBot.Service;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;

namespace ChatBot
{
    using ChatBot.Controllers;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;

    using Newtonsoft.Json;

    public class Startup
    {
        private readonly SecurityKey _securityKey;

        private static string _applicationPath = string.Empty;
        private static string _contentRootPath = string.Empty;

        public Startup(IHostingEnvironment env)
        {
            _applicationPath = env.WebRootPath;
            _contentRootPath = env.ContentRootPath;
            // Setup configuration sources.

            var builder = new ConfigurationBuilder()
                .SetBasePath(_contentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {

                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                //builder.AddUserSecrets();
                builder.AddUserSecrets<Startup>();
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            var cert = new X509Certificate2(Path.Combine(env.ContentRootPath, "people.pfx"));
            _securityKey = new X509SecurityKey(cert);
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string sqlConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
            bool useInMemoryProvider = bool.Parse(Configuration["Data:ChatBotDBConnection:InMemoryProvider"]);
            services.Configure<RequestLocalizationOptions>(opts =>
            {
                var supportedCulture = new[] { new CultureInfo("vi-VN") };
                opts.DefaultRequestCulture = new RequestCulture("vi-VN");
                opts.DefaultRequestCulture.Culture.NumberFormat.NumberDecimalSeparator = ".";
                opts.SupportedCultures = supportedCulture;
            });
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 512 * 1000000;
            });

    
            //  services.AddDbContext<ChatBotDbContext>(options => options.UseSqlServer(@"Data Source=DESKTOP-SD7L5A2\PC;Initial Catalog=ChatBotDB;Integrated Security=False;User Id=sa;Password=123456;MultipleActiveResultSets=True;"));
            //services.AddDbContext<gGMSContext>(options => options.UseSqlServer(@"Server=192.168.1.179\sql2012full;Database=gGMS;User Id=gGMS;Password=gGMS@239;MultipleActiveResultSets=True;"));

            string stringGMSDBConnection = Configuration["ConnectionStrings:gGMSConnection"];

            services.AddDbContext<ChatBotDbContext>(options => options.UseSqlServer(stringGMSDBConnection));
            services.AddDbContext<gGMSContext>(options => options.UseSqlServer(stringGMSDBConnection));

            //services.AddDbContext<ChatBotDbContext>(options => options.UseSqlServer(@"Server=192.168.1.179\sql2012full;Database=gGMS;User Id=gGMS;Password=gGMS@239;MultipleActiveResultSets=True;"));
            //services.AddDbContext<gGMSContext>(options => options.UseSqlServer(@"Server=192.168.1.179\sql2012full;Database=gGMS;User Id=gGMS;Password=gGMS@239;MultipleActiveResultSets=True;"));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                //     options.Password.RequireNonLetterOrDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })

          .AddEntityFrameworkStores<ChatBotDbContext>()
          .AddDefaultTokenProviders();
            services.AddScoped<gGMSContext>();
            // Repositories


            services.AddScoped<IMenuRoleRepository, MenuRoleRepository>();
            services.AddScoped<IApplicationGroupRepository, ApplicationGroupRepository>();
            services.AddScoped<IApplicationUserGroupRepository, ApplicationUserGroupRepository>();
            services.AddScoped<IApplicationRoleGroupRepository, ApplicationRoleGroupRepository>();
            services.AddScoped<IApplicationRoleRepository, ApplicationRoleRepository>();


            services.AddScoped<ILoggingRepository, LoggingRepository>();
            services.AddScoped<IBotDomainRepository, BotDomainRepository>();

            //Services

            services.AddScoped<IMenuRoleService, MenuRoleService>();
            services.AddScoped<IApplicationGroupService, ApplicationGroupService>();
            services.AddScoped<IApplicationRoleService, ApplicationRoleService>();
            //   services.AddAuthentication();
            //    services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            // Polices
            services.AddAuthorization(options =>
            {
                // inline policies
                options.AddPolicy("DeleteUser2", policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, "ViewUserConfig");
                });

            });

            //  services.AddScoped<IMembershipService, MembershipService>();
            //services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<IBotDomainService, BotDomainService>();

            // Add MVC services to the services container.
            services.AddMvc()
            .AddJsonOptions(opt =>
            {
                var resolver = opt.SerializerSettings.ContractResolver;
                if (resolver != null)
                {
                    var res = resolver as DefaultContractResolver;
                    res.NamingStrategy = null;
                }
            });

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {

                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.RsaSha256);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            // this will serve up wwwroot
            app.UseStaticFiles();
            //        app.UseCors("CorsPolicy");
            app.UseCors(builder =>
              builder.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());


            AutoMapperConfiguration.Configure();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            app.UseIdentity();
            // Custom authentication middleware
            // app.UseMiddleware<AuthMiddleware>();

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],
                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _securityKey,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            //Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            
            DbInitializer.Initialize(app.ApplicationServices, _applicationPath);
        }

        // Entry point for the application.
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
              .UseKestrel()
              .UseContentRoot(Directory.GetCurrentDirectory())
              .UseUrls("http://*:9823")
              .UseSetting("detailedErrors", "true")
              .CaptureStartupErrors(true)
              .UseIISIntegration()
              .UseStartup<Startup>()
              .Build();


            host.Run();
        }
    }
}