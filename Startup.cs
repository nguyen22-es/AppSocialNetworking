
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SocialNetworking.Hubs;
using SocialNetworking.Repository;
using SocialNetworking.Services;
using SocialNetworkingApi.IdentityServer;
using SocialNetworkingApi.Services;


namespace SocialNetworking
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

            services.AddDbContext<ManageAppDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ManagerUser, IdentityRole>() // để cho nó dùng được UserManger và roleManager
                .AddEntityFrameworkStores<ManageAppDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<UserManager<ManagerUser>>();
            services.AddTransient<IEmailSender, EmailSenderService>();
            services.AddTransient<IMessagesService, MessagesService>();
            services.AddTransient<IPostService, PostService>();
            services.AddScoped<IFriendShipService, FriendShipService>();
            services.AddTransient<IFriendShipsDbRepository, FriendShipsRepository>();
            services.AddTransient<ICommentsDbRepository, CommentsRepository>();
            services.AddTransient<IMessageDbRepository, MessageRepository>();
            services.AddTransient<IPostDbRepository, PostRepository>();
            services.AddScoped<RoleManager<IdentityRole>>();
                // other services
            services.AddAutoMapper(typeof(Startup));
            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

            }) 
          .AddInMemoryApiResources(Config.ApiResources)
                                               
          .AddInMemoryClients(Config.Clients) 
          .AddInMemoryIdentityResources(Config.IdentityResources)

          .AddInMemoryApiScopes(Config.ApiScopes)
          .AddAspNetIdentity<ManagerUser>()
          .AddDeveloperSigningCredential();


            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

           /* services.AddAuthentication("Bearer")
      .AddIdentityServerAuthentication("Bearer", options =>
      {
          options.ApiName = "CoffeeAPI";
          options.Authority = "https://localhost:5443";
      });*/
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Bearer"; // Chắc chắn rằng mặc định là Bearer
            })
.AddJwtBearer("Bearer", options =>
{
    options.Authority = "https://localhost:5443"; // Thay thế bằng URL của IdentityServer của bạn
    options.Audience = "CoffeeAPI.read"; // Thay thế bằng tên nguồn tài nguyên của API
});
            /*  services.AddAuthentication("Bearer")
              .AddLocalApi("Bearer", options =>
              {
                  options.ExpectedScope = "CoffeeAPI.read";

              });*/

             services.AddAuthorization(options =>
                {
                    options.AddPolicy("Bearer", policy =>
                    {
                        policy.AddAuthenticationSchemes("Bearer");
                        policy.RequireAuthenticatedUser();
                    });
                });

            services.AddRazorPages(options =>
            {
                options.Conventions.AddAreaFolderRouteModelConvention("Identity", "/Account/", model =>
                {
                    foreach (var selector in model.Selectors)
                    {
                        var attributeRouteModel = selector.AttributeRouteModel;
                        attributeRouteModel.Order = -1;
                        attributeRouteModel.Template = attributeRouteModel.Template.Remove(0, "Identity".Length);
                    }
                });
            });


            services.AddControllersWithViews();
            services.AddSignalR();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApp Space Api", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://localhost:5000/connect/authorize"),
                            Scopes = new Dictionary<string, string> { { "CoffeeAPI.read", "CoffeeAPI.write" } }

                        }
                    }
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new List<string>{ "CoffeeAPI.read" }
        }
    });
            });





        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseIdentityServer();

            app.UseAuthentication();
         
            app.UseRouting();
         
            app.UseAuthorization();
            app.UseCors("AllowOrigin");
            app.UseEndpoints(endpoints =>
            {
           
                endpoints.MapDefaultControllerRoute().RequireCors("AllowOrigin");
                endpoints.MapRazorPages();
                endpoints.MapHub<ChatHub>("/chatHub");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.OAuthClientId("swagger");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApp Space Api V1");
            });

        }
    }
}
