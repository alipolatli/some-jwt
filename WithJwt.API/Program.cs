using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Configuration;
using System.Reflection;
using WithJwt.Core.Configurations;
using WithJwt.Core.Entites;
using WithJwt.Core.Repositories;
using WithJwt.Core.Services;
using WithJwt.Core.Services.InternalServices;
using WithJwt.Core.UnitOfWork;
using WithJwt.Repository.Contexts;
using WithJwt.Repository.Repositories;
using WithJwt.Repository.UnitOfWork;
using WithJwt.Service.Mappers.AutoMapper;
using WithJwt.Service.Services;
using WithJwt.Service.Services.InternalManagers;

var builder = WebApplication.CreateBuilder(args);

//only internal, best practise olanı autofac kullanıp servis katmanında olusturmaktır.
builder.Services.AddScoped<ITokenService, TokenService>();

//DI REGISTER
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUnitofWork, UnitofWork>();
builder.Services.AddAutoMapper(typeof(DtoProfile));


//DB
builder.Services.AddDbContext<JwtCourseDbContext>();

builder.Services.AddIdentity<AppUser, AppRole>(idenytityOptions =>
{

    //PASSWORD OPTIONS
    idenytityOptions.Password.RequiredLength = 4; //miniumum karakter default=6
    idenytityOptions.Password.RequireNonAlphanumeric = false;//?.! gibi karakter zounlu olmasın. default=true
    idenytityOptions.Password.RequireLowercase = false;//küçük karaker zounlu olmasın. default=true
    idenytityOptions.Password.RequireUppercase = false;//büyük karakter zorunlu olmasın. default=true
    idenytityOptions.Password.RequireDigit = false;//rakam zorunlu değil. default=true
    idenytityOptions.Password.RequiredUniqueChars = 1;//1 adet benzersiz (farklı karakter olsun) default=1
    //PASSWORD OPTIONS

    //USERNAME OPTIONS
    idenytityOptions.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";
    idenytityOptions.User.RequireUniqueEmail = true; //benzersiz emaial default= false
    //USERNAME OPTIONS

    //PasswordContainsValidator ile custom PASSWORD OPTIONS.
    //CustomUserValidator ile custom USER OPTIONS.
    //CustomIdentityErrorDescriber ile errorları türkçeleştirme.
    //AddEntityFrameworkStores ile veri tabanı senkronizasyonu.
}).AddDefaultTokenProviders().AddEntityFrameworkStores<JwtCourseDbContext>();


builder.Services.Configure<CustomTokenOptions>(builder.Configuration.GetSection("TokenOptions"));
builder.Services.Configure<List<ClientNoIdentity>>(builder.Configuration.GetSection("ClientsNoIdentity"));


builder.Services.AddAuthentication(authenticationOptions =>
{
    authenticationOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions =>
{
    CustomTokenOptions tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<CustomTokenOptions>();
    jwtBearerOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Auidence[0],
        IssuerSigningKey = SecurityKeyService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddAuthorization(authOptions =>
{
    authOptions.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
});

//builder.Services.AddAuthorization(options =>
//{
//    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
//        JwtBearerDefaults.AuthenticationScheme);

//    defaultAuthorizationPolicyBuilder =
//        defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

//    options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
//});


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
