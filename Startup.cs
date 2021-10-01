using FluentValidation.AspNetCore;
using MerkleKitchenApp_V2.Data;
using MerkleKitchenApp_V2.Mapper;
using MerkleKitchenApp_V2.Repository;
using MerkleKitchenApp_V2.Repository.IRepository;
using MerkleKitchenApp_V2.Services;
using MerkleKitchenApp_V2.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2
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
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:8080", "https://kitchenappfrontend.merkleinc.agency").AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<ITvRepository, TvRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<ITvService, TvService>();
            services.AddAutoMapper(typeof(Mappings));

            string[] docs = {"MerkleKitchenAPIOrder", "MerkleKitchenAPIOrderItem", "MerkleKitchenAPIProduct", "MerkleKitchenAPIUser", "MerkleKitchenAPIEmail", "MerkleKitchenAPIMenu", "MerkleKitchenAPITv" };
            foreach (var doc in docs)
            {
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc(doc,
                        new Microsoft.OpenApi.Models.OpenApiInfo()
                        {
                            Title = doc,
                        });
                    var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                    options.IncludeXmlComments(cmlCommentsFullPath);
                });
            }            
               
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
               "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
               "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
               "Example: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

           //TOKEN CONFIGURATION
           var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x => {
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



            services.AddControllers()
                .AddFluentValidation(x =>
                {
                    x.RegisterValidatorsFromAssemblyContaining<Startup>();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("ApiCorsPolicy");

            if (env.IsDevelopment())
            {
               
            }
            app.UseDeveloperExceptionPage();

            app.UseSwagger();

            app.UseSwaggerUI(options=> {
                options.SwaggerEndpoint("/swagger/MerkleKitchenAPIUser/swagger.json", "Merkle Kitchen API User");
                options.SwaggerEndpoint("/swagger/MerkleKitchenAPIOrder/swagger.json", "Merkle Kitchen API Order");
                options.SwaggerEndpoint("/swagger/MerkleKitchenAPIOrderItem/swagger.json", "Merkle Kitchen API Order Item");
                options.SwaggerEndpoint("/swagger/MerkleKitchenAPIProduct/swagger.json", "Merkle Kitchen API Product");
                options.SwaggerEndpoint("/swagger/MerkleKitchenAPIEmail/swagger.json", "Merkle Kitchen API Email");
                options.SwaggerEndpoint("/swagger/MerkleKitchenAPIMenu/swagger.json", "Merkle Kitchen API Menu");
                options.SwaggerEndpoint("/swagger/MerkleKitchenAPITv/swagger.json", "Merkle Kitchen API TV");
                options.RoutePrefix = "";
            });
            
            app.UseRouting();
            
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/Health");
            });
        }
    }
}
