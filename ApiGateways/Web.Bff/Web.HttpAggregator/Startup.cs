using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Hellang.Middleware.ProblemDetails;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Web.HttpAggregator.Config;
using Web.HttpAggregator.Exceptions;
using Web.HttpAggregator.Models;
using Web.HttpAggregator.Services;
using Web.HttpAggregator.Services.Match;
using Web.HttpAggregator.Services.Phrase;
using Web.HttpAggregator.Services.UserAccess;

namespace Web.HttpAggregator
{
    public class Startup
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices();

            services.Configure<UrlsConfig>(Configuration.GetSection("urls"));
            services.Configure<IdentityConfig>(Configuration.GetSection("IdentityConfig"));

            services.AddSwagger();

            services.AddProblemDetails(options =>
            {
                options.Map<BusinessRuleValidationException>((ex) => new BusinessRuleValidationExceptionProblemDetails(ex));
                options.IncludeExceptionDetails = ctx => true;
            });

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });


            services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);

            services
                .ConfigureAuthentication(Configuration, _hostingEnvironment)
                .AddOptions()
                .AddCustomMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseProblemDetails();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            app.UseAuthentication();

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }

    public static class ServiceCollectionExtensions
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
            if (!hostEnvironment.IsDevelopment())
            {
                services.AddMvc(opts =>
                {
                    opts.Filters.Add(new AllowAnonymousFilter());
                });
                services.AddScoped<IExecutionContextAccessor, MockExecutionContextAccessor>();
            }
            else
            {
                services.AddScoped<IExecutionContextAccessor, ExecutionContextAccessor>();
                services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, x =>
                    {
                        x.Authority = "http://useraccess.api";
                        x.ApiName = "web.httpaggregator";
                        x.RequireHttpsMetadata = false;
                        x.SaveToken = true;
                    });
            }

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddHttpClient<IMatchApiClient, MatchApiClient>();
            services.AddHttpClient<IPhraseApiClient, PhraseApiClient>();
            services.AddHttpClient<IUserAccessApiClient, UserAccessApiClient>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Web Http Aggregator API", Version = "v1" });

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

    public class TestingHttpContextAccessor
    {
        public void AccessTokenGetter()
        {

        }
    }
}