using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions
{
    public static class AuthTokenBuild
    {
        public static IServiceCollection AddCustomTokenAuth(this IServiceCollection services, CustomTokenOptions tokenOptions)
        {
            services.AddAuthentication(authenticationOptions =>
             {
                 authenticationOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                 authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
             }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions =>
             {
                //CustomTokenOptions tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<CustomTokenOptions>();
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


            services.AddAuthorization(authOptions =>
            {
                authOptions.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
            });

            return services;
        }
    }
}
