using FMS_Web_Api.Configuration;
using FMS_Web_Api.DAL;
using FMS_Web_Api.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace FMS_Web_Api
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
                options.AddPolicy(
                  "CorsPolicy",
                  builder => builder.WithOrigins("http://localhost:4200")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials());
            });
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                            .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<EventRepository>();
            services.AddScoped<DashboardRepository>();
            services.AddScoped<FeedbackQuestionRepository>();
            services.AddScoped<FeedbackOptionRepository>();
            services.AddScoped<FeedbackRepository>();
            services.AddScoped<EventParticipatedUsersRepository>();
            services.AddScoped<ParticipantFeedbackRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            // configure strongly typed settings objects	    
            var jwtSection = Configuration.GetSection("JwtBearerTokenSettings");
            services.Configure<JwtBearerTokenSettings>(jwtSection);
            var jwtBearerTokenSettings = jwtSection.Get<JwtBearerTokenSettings>();
            var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options => { options.RequireHttpsMetadata = false; 
                    options.SaveToken = true; options.TokenValidationParameters = new TokenValidationParameters() { 
                        ValidateIssuer = true, 
                        ValidIssuer = jwtBearerTokenSettings.Issuer,
                        ValidateAudience = true, 
                        ValidAudience = jwtBearerTokenSettings.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateLifetime = true, ClockSkew = TimeSpan.Zero }; });
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ApplicationDbContext db, UserManager<IdentityUser> userManager,
RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            db.Database.Migrate();
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();            
            app.UseRouting();
            app.UseAuthentication();

            //Loading roles and admin user to DB for the first time
            MyIdentityDataInitializer.SeedData(userManager, roleManager);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
