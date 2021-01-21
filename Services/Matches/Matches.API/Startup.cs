using System.Collections.Generic;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Base.Api.Configuration;
using Base.Api.Configuration.Authorization;
using Base.Api.Configuration.Validation;
using Base.Application.BuildingBlocks;
using Base.Domain.Exceptions;
using Base.EventBusRabbitMQ;
using Base.Infrastructure.Emails;
using Hellang.Middleware.ProblemDetails;
using IdentityServer4.AccessTokenValidation;
using Matches.Domain.Team;
using Matches.Infrastructure.Configuration;
using Matches.Infrastructure.Configuration.Match;
using Matches.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Compact;

namespace Matches.API
{
    public class Startup
    {
        public ILifetimeScope AutofacContainer { get; private set; }

        private readonly IWebHostEnvironment _hostingEnvironment;

        private static ILogger _logger;
        private static ILogger _loggerForApi;

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddLogging(services);

            services.AddControllers();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();

            services.AddProblemDetails(x =>
            {
                x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
            });

           services
                .ConfigureAuthentication(Configuration, _hostingEnvironment)
                .AddOptions()
                .AddSwagger(Configuration)
                .AddCustomMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            AutofacContainer = app.ApplicationServices.GetAutofacRoot();


            //app.UseHttpsRedirection();

            InitializeModule(AutofacContainer);

            InitializeDbContext();

            app.UseProblemDetails();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new MatchAutofacModule());
        }

        private void InitializeModule(ILifetimeScope autofacContainer)
        {
            var emailsConfiguration = new EmailsConfiguration(Configuration["EmailsConfiguration:FromEmail"], Configuration["EmailsConfiguration:SendGridUser"], Configuration["EmailsConfiguration:SendGridKey"]);

            var httpContextAccessor = autofacContainer.Resolve<IHttpContextAccessor>();
            var executionContextAccessor = new ExecutionContextAccessor(httpContextAccessor);

            var eventBus = new EventBusRabbitMQ(_logger, GetRabbitMQConnection(), "football-banter", "Matches");

            MatchesStartup.Initialize(
                Configuration["ConnectionString"],
                executionContextAccessor,
                _logger,
                emailsConfiguration,
                Configuration["Security:TextEncryptionKey"],
                null,
                eventBus
            );
        }

        private DefaultRabbitMQPersistentConnection GetRabbitMQConnection()
        {
            var eventBusConnectionDetails = new ConnectionDetails();
            Configuration.GetSection("EventBusConnectionDetails").Bind(eventBusConnectionDetails);

            return new DefaultRabbitMQPersistentConnection(eventBusConnectionDetails, _logger);
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
   
        private void InitializeDbContext()
        {
            var context = MatchesCompositionRoot.BeginLifetimeScope().Resolve<MatchContext>();
            var teamRepository = MatchesCompositionRoot.BeginLifetimeScope().Resolve<ITeamRepository>();
            context.Database.Migrate();
            MatchContextInitializer.Initialize(context, teamRepository);
        }

    }

    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddMvc(config =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    config.Filters.Add(new AuthorizeFilter(policy));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            return services;
        }

        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthorizationHandler, HasPermissionAuthorizationHandler>();
            services.AddScoped<IExecutionContextAccessor, ExecutionContextAccessor>();
            services.AddHttpContextAccessor();

            if (hostEnvironment.IsDevelopment())
            {
                services.AddMvc(opts =>
                {
                    opts.Filters.Add(new AllowAnonymousFilter());
                });
            }
            else
            {
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

            }

            return services;
        }

       public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
       {
           services.AddSwaggerGen(options =>
           {
               options.SwaggerDoc("v1", new OpenApiInfo { Title = "Match API", Version = "v1" });

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