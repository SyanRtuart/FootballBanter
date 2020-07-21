using System.Collections.Generic;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Base.Api.Configuration;
using Base.Api.Configuration.Authorization;
using Base.Api.Configuration.Validation;
using Base.Application.BuildingBlocks;
using Base.Domain.Exceptions;
using Base.Infrastructure.Emails;
using Hellang.Middleware.ProblemDetails;
using IdentityServer4.AccessTokenValidation;
using Matches.Domain.Team;
using Matches.Infrastructure.Configuration;
using Matches.Infrastructure.Configuration.Integration;
using Matches.Infrastructure.Configuration.Quartz;
using Matches.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Quartz;
using Serilog;
using Serilog.Formatting.Compact;

namespace Matches.API
{
    public class Startup
    {
        private static ILogger _logger;
        private static ILogger _loggerForApi;
        public ILifetimeScope AutofacContainer { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddLogging(services);

            services.AddControllers();

            //services.ConfigureIdentityServer(Configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();

            services.AddProblemDetails(x =>
            {
                x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(HasPermissionAttribute.HasPermissionPolicyName, policyBuilder =>
                {
                    policyBuilder.Requirements.Add(new HasPermissionAuthorizationRequirement());
                    policyBuilder.AddAuthenticationSchemes(IdentityServerAuthenticationDefaults.AuthenticationScheme);
                });
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthorizationHandler, HasPermissionAuthorizationHandler>();
            services.AddScoped<IExecutionContextAccessor, ExecutionContextAccessor>();
            services.AddHttpContextAccessor();

            services.Configure<AuthMessageSenderOptions>(Configuration);

            services
                .ConfigureAuthentication(Configuration)
                .AddOptions()
                .AddSwagger(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            InitializeQuartz();

            InitializeDbContext();

            //app.UseHttpsRedirection();

            app.UseProblemDetails();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var emailsConfiguration = new EmailsConfiguration(Configuration["EmailsConfiguration:FromEmail"]);

            MatchesStartup.Initialize(
                Configuration["ConnectionString"],
                new ExecutionContextAccessor(new HttpContextAccessor()),
                _logger,
                emailsConfiguration,
                Configuration["Security:TextEncryptionKey"],
                null,
                builder);
        }

        private void AddLogging(IServiceCollection services)
        {
            _logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u3}] [{Module}] [{Context}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.RollingFile(new CompactJsonFormatter(), "logs/logs")
                .CreateLogger();

            _loggerForApi = _logger.ForContext("Module", "API");

            _loggerForApi.Information("Logger configured");
        }
        private void InitializeQuartz()
        {
            var scheduler = AutofacContainer.Resolve<IScheduler>();
            var logger = AutofacContainer.Resolve<ILogger>();

            QuartzStartup.Initialize(logger, scheduler);
        }

        private void InitializeDbContext()
        {
            var context = AutofacContainer.Resolve<MatchContext>();
            var teamRepository = AutofacContainer.Resolve<ITeamRepository>();
            context.Database.Migrate();
            MatchContextInitializer.Initialize(context, teamRepository);
        }

    }

    internal static class ServiceCollectionExtensions
    {
       public static IServiceCollection ConfigureAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthorizationHandler, HasPermissionAuthorizationHandler>();
            services.AddScoped<IExecutionContextAccessor, ExecutionContextAccessor>();
            services.AddHttpContextAccessor();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(HasPermissionAttribute.HasPermissionPolicyName, policyBuilder =>
                {
                    policyBuilder.Requirements.Add(new HasPermissionAuthorizationRequirement());
                    policyBuilder.AddAuthenticationSchemes("Bearer");
                });
            });

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, x =>
                {
                    x.Authority = "http://useraccess.api";
                    x.ApiName = "matchesApi";
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                });

            return services;
        }

       public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
       {
           services.AddSwaggerGen(options =>
           {
               options.SwaggerDoc("v1", new OpenApiInfo { Title = "User Access API", Version = "v1" });

               options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
               {
                   Description =
                       "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                   Name = "Authorization",
                   In = ParameterLocation.Header,
                   Type = SecuritySchemeType.ApiKey
               });

               options.AddSecurityRequirement(new OpenApiSecurityRequirement
               {
                   {
                       new OpenApiSecurityScheme
                       {
                           Reference = new OpenApiReference
                           {
                               Type = ReferenceType.SecurityScheme,
                               Id = "Bearer"
                           },
                           Scheme = "oauth2",
                           Name = "Bearer",
                           In = ParameterLocation.Header
                       },
                       new List<string>()
                   }
               });
           });

           return services;
       }
    }
}