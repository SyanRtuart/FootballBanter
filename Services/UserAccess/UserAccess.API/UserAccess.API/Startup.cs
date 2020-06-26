using System.Collections.Generic;
using Autofac;
using Base.Api.Configuration;
using Base.Api.Configuration.Authorization;
using Base.Api.Configuration.Validation;
using Base.Application.BuildingBlocks;
using Base.Domain.Exceptions;
using Base.Infrastructure.Emails;
using Hellang.Middleware.ProblemDetails;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Compact;
using UserAccess.API.Configuration;
using UserAccess.Application.IdentityServer;
using UserAccess.Infrastructure.Configuration;

namespace UserAccess.API
{
    public class Startup
    {
        private static ILogger _logger;
        public static ILogger ApiLogger;
        private static ILogger _loggerForApi;

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

            services.ConfigureIdentityServer(Configuration);

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
                .AddOptions()
                .AddSwagger(Configuration)
                .AddCustomMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseProblemDetails();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
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
            // services.AddSingleton(ApiLogger);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var emailsConfiguration = new EmailsConfiguration(Configuration["EmailsConfiguration:FromEmail"]);

            UserAccessStartup.Initialize(
                Configuration["ConnectionString"],
                new ExecutionContextAccessor(new HttpContextAccessor()),
                _logger,
                emailsConfiguration,
                Configuration["Security:TextEncryptionKey"],
                null,
                builder);

            //var container = builder.Build();

            //UserAccessCompositionRoot.SetContainer(container);

            //using (var scope = UserAccessCompositionRoot.BeginLifetimeScope())
            //{
            //    var mediator = scope.Resolve<IMediator>();
            //    mediator.Send(new RegisterNewUserCommand("", "", "", "", ""));
            //}
        }
    }

    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            return services;
        }

        public static IServiceCollection ConfigureIdentityServer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddIdentityServer(options => { options.IssuerUri = "http://useraccess.api"; })
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfig.GetApis())
                .AddInMemoryClients(IdentityServerConfig.GetClients())
                .AddInMemoryPersistedGrants()
                .AddProfileService<ProfileService>()
                .AddDeveloperSigningCredential();

            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, x =>
                {
                    x.Authority = "http://useraccess.api";
                    x.ApiName = "userAccessApi";
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo {Title = "User Access API", Version = "v1"});

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