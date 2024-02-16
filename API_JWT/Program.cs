using API_JWT;
using API_JWT.Identity;
using API_JWT.Servicecontract;
using API_JWT.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string cs = builder.Configuration.GetConnectionString("conStr");
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationDbContext>
 (option => option.UseSqlServer(cs,
 b => b.MigrationsAssembly("API_JWT")));

builder.Services.AddTransient<IRoleStore<ApplicationRole>, ApplicationRoleStore>();
builder.Services.AddTransient<UserManager<Applicationuser>, ApplicationUserManager>();
builder.Services.AddTransient<SignInManager<Applicationuser>, ApplicationSignInManager>();
builder.Services.AddTransient<RoleManager<ApplicationRole>, ApplicationRoleManager>();
builder.Services.AddTransient<IUserStore<Applicationuser>, ApplicationUserStore>();
builder.Services.AddTransient<IUserService, UserServices>();
builder.Services.AddIdentity<Applicationuser, ApplicationRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddUserStore<ApplicationUserStore>()
.AddUserManager<ApplicationUserManager>()
.AddRoleManager<ApplicationRoleManager>()
.AddSignInManager<ApplicationSignInManager>()
.AddRoleStore<ApplicationRoleStore>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<ApplicationRoleStore>();
builder.Services.AddScoped<ApplicationUserStore>();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

//Add JWT Authentication
var appsettingsection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<Appsettings>(appsettingsection);
var appsetting = appsettingsection.Get<Appsettings>();
var key = Encoding.ASCII.GetBytes(appsetting.secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",

      builder =>
      {
          builder.WithOrigins("http://localhost:4200/")
               .AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyPolicy");
app.UseAuthentication();
app.UseAuthorization();
//data

//IServiceScopeFactory serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
//using (IServiceScope scope = serviceScopeFactory.CreateScope())
//{
//    var rolemanager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
//    var usermanager = scope.ServiceProvider.GetRequiredService<UserManager<Applicationuser>>();
//    //Create Role Admins
//    if (!await rolemanager.RoleExistsAsync("Admin"))
//    {
//        var role = new ApplicationRole();
//        role.Name = "Admin";
//        await rolemanager.CreateAsync(role); 0[]
//    }
//    //Create Role Employee
//    if (!await rolemanager.RoleExistsAsync("employee"))
//    {
//        var role = new ApplicationRole();
//        role.Name = "employee";
//        await rolemanager.CreateAsync(role);
//    }
//    //Create Admin User
//    if (await usermanager.FindByNameAsync("Admin") == null)
//    {
//        var user = new Applicationuser();
//        user.UserName = "Admin";
//        user.Email = "admin@gmail.com";
//        var userpassword = "Admin@123";
//        var chkuser = await usermanager.CreateAsync(user, userpassword);
//        if (chkuser.Succeeded)
//        {
//            await usermanager.AddToRoleAsync(user, "Admin");
//        }
//    }
//    //  //Create Employee User
//    if (await usermanager.FindByNameAsync("employee") == null)
//    {
//        var user = new Applicationuser();
//        user.UserName = "employee";
//        user.Email = "employee@gmail.com";
//        var userPassword = "Admin@123";
//        var chkUser = await usermanager.CreateAsync(user, userPassword);
//        if (chkUser.Succeeded)
//        {
//            await usermanager.AddToRoleAsync(user, "Employee");
//        }

//    }
//}
app.MapControllers();

app.Run();
