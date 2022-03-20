using Aniverse.Business.Implementations;
using Aniverse.Business.Interface;
using Aniverse.Core;
using Aniverse.Core.Entites;
using Aniverse.Data;
using Aniverse.Data.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Aniverse.Business.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Aniverse.Business.Services.Interface;
using Aniverse.Business.Services.Implementaions;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Aniverse.UI.Hubs;
using FluentValidation.AspNetCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Aniverse.Business.Validator.Authentication;
using FluentValidation;
using Aniverse.Business.DTO_s.Authentication;
using Aniverse.Business.DTO_s.Post;
using Aniverse.Business.Validator.Post;
using Aniverse.Business.DTO_s.Page;
using Aniverse.Business.Validator.Page;

namespace Aniverse
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
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:3000")
                                                            .AllowAnyHeader()
                                                            .AllowAnyMethod()
                                                            .AllowCredentials();
                                  });
            });
            services.AddSignalR();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
            services.AddControllers().AddNewtonsoftJson(options =>
             options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddFluentValidation(fl => fl.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddTransient<IValidator<Register>, RegisterValidation>();
            services.AddTransient<IValidator<Login>, LoginValidation>();
            services.AddTransient<IValidator<PostCreateDto>, PostCreateValidation>();
            services.AddTransient<IValidator<PageCreateDto>, PageCreateValidation>();

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = false;

                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = true;
            })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AppDbContext>();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = Configuration.GetSection("Jwt:audience").Value,
                    ValidIssuer = Configuration.GetSection("Jwt:issuer").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Jwt:securityKey").Value)),
                };
            });
            services.AddSingleton<IDictionary<string, UserConnection>>(opts => new Dictionary<string, UserConnection>());
            services.AddMapperService();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtService, JwtService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Images")),
                RequestPath = "/Images"
            });
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapControllers();
            });
        }
    }
}
