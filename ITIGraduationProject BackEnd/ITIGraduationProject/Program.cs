using AspNetCoreRateLimit;
using BugProject;
using ITIGraduationProject.BL;
using ITIGraduationProject.BL.DTO;
using ITIGraduationProject.BL.Manger.SubscriptionManger;
using ITIGraduationProject.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Security.Claims;
using System.Text;
namespace ITIGraduationProject
{
    public class Program
    {
        public static  async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDataAccessServices(builder.Configuration);
            builder.Services.AddBusinessServices();
            builder.Services.Configure<MailSettingsDto>(builder.Configuration.GetSection("MailSettings"));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
            {
                // Validation to be read from configurations
                options.Password.RequiredUniqueChars = 2;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;
            })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
        
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // 2. Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // 3. Configure JWT Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:IssuerIP"],

                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:AudienceIP"],

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecritKey"])),

                    ValidateLifetime = true
                };
            });

            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    Constatnts.Policies.ForAdminOnly,
                    builder => builder
                        .RequireClaim(ClaimTypes.Role, "Manager", "Developer")
                        .RequireClaim(ClaimTypes.NameIdentifier)
                );

                options.AddPolicy(
                    Constatnts.Policies.ForDev,
                    builder => builder
                        .RequireClaim(ClaimTypes.Role, "Developer", "PremiumUser")
                        .RequireClaim(ClaimTypes.NameIdentifier)
                );
                options.AddPolicy(
                  Constatnts.Policies.ForTester,
                  builder => builder
                      .RequireClaim(ClaimTypes.Role, "Developer", "Tester")
                      .RequireClaim(ClaimTypes.NameIdentifier)
              );

            });

            // From here to Handle number of requsets per minute


            // Add MemoryCache (مطلوب للمكتبة)
            builder.Services.AddMemoryCache();

            // إعداد قواعد الـ Rate Limiting
            builder.Services.Configure<IpRateLimitOptions>(options =>
            {
                options.GeneralRules = new List<RateLimitRule>
    {
        new RateLimitRule
        {
            Endpoint = "*",     // كل المسارات
            Limit = 50,          // عدد الريكوستات المسموح بها
            Period = "1m"       // خلال دقيقة واحدة
        }
    };
            });

            // إضافة باقي الخدمات المطلوبة من المكتبة
            builder.Services.AddInMemoryRateLimiting();
            builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // باقي الخدمات بتاعتك
            builder.Services.AddControllers();

            // End here

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await Constatnts.SeedRolesAsync(services); // 👈 Seed roles here
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");

            // 8. Enable Authentication & Authorization
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}