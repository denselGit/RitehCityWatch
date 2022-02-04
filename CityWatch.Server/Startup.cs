using CityWatch.Server.Services;
using CityWatch.Server.Services.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CityWatch.Server
{
    public class Startup
    {
        internal static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        // postavke za JWT
        private string TokenSecurityKey = "";
        private string TokenIssuer = "";
        private string TokenAudience = "";

        public Startup(IConfiguration configuration)
        {
            log.Info("CityWatch Server starting, version: " + Common.Info.GetVersion());

            Configuration = configuration;

            TokenSecurityKey = Configuration.GetValue<string>("TokenSecurityKey");
            TokenIssuer = Configuration.GetValue<string>("TokenIssuer");
            TokenAudience = Configuration.GetValue<string>("TokenAudience");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // cors
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));


            services.AddControllers();

            // postavi JWT i API key autorizaciju
            var key = Encoding.ASCII.GetBytes(TokenSecurityKey);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddApiKeyInHeader(ApiKeyDefaults.AuthenticationScheme, options =>
            //{
            //    options.KeyName = "X-API-Key";
            //    options.SuppressWWWAuthenticateHeader = true;
            //    options.Events = new ApiKeyEvents
            //    {
            //        //delegate assigned to this property will be invoked just before validating the api key.
            //        OnValidateKey = async (context) =>
            //        {
            //            //custom code to handle the api key, create principal and call Success method on context.
            //            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
            //            var securityGuardService = context.HttpContext.RequestServices.GetRequiredService<ISecurityGuardService>();
            //            var user = (await userService.GetAll()).FirstOrDefault(x => x.ApiKey.ToLower() == context.ApiKey.ToLower());
            //            if (user != null)
            //            {
            //                var claims = new Claim[]
            //                {
            //                    new Claim(ClaimTypes.Name, user.IdUser.ToString()),
            //                    new Claim(ClaimTypes.Role, user.Role),
            //                    new Claim(ClaimTypes.AuthorizationDecision, "citywatch.com"),
            //                    new Claim(ClaimTypes.AuthenticationMethod, "apikey"),
            //                    new Claim("Username", user.Username),
            //                    new Claim("Email", user.Email)
            //                };

            //                context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims, context.Scheme.Name));
            //                context.Success();
            //            }
            //            else
            //            {
            //                context.NoResult();
            //            }
            //        }
            //    };
            //})
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
            {
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

            services.AddSignalR();

            services.AddHttpContextAccessor();

            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<IDatabaseService, DatabaseService>();

            services.AddSingleton<MqttService>();
            services.AddHostedService(provider => provider.GetService<MqttService>());

            services.AddSingleton<RabbitMqService>();
            services.AddHostedService(provider => provider.GetService<RabbitMqService>());

            services.AddSingleton<ServiceManager>();
            services.AddHostedService(provider => provider.GetService<ServiceManager>());

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<ITechnicalDeviceRepository, TechnicalDeviceRepository>();

            // obrana od bruteforceanja lozinki
            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            });

            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CityWatch Server",
                    Version = "v1",
                    Description = "CityWatch API specification"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Reference = new OpenApiReference()
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.UseAllOfToExtendReferenceSchemas();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CityWatch.Server v1"));
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("../swagger/v1/swagger.json", "CityWatch.Server v1"));
            }

            if (!env.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseCors("MyPolicy");

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<DeviceHub>("/deviceHub");
            });
        }
    }
}
