using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using ProniaOnion104.Application.Abstractions.Services;
using a=ProniaOnion104.Infrastructure.Implementations;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace ProniaOnion104.Infrastructure.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SecurityKey"])),
                    LifetimeValidator = (notBefore, expired, token, param) => token!=null? expired>DateTime.UtcNow:false

                    //LifetimeValidator = (notBefore, expired, token, param) => {
                    //    if (token is not null)
                    //    {
                    //        if (expired>DateTime.UtcNow)
                    //        {
                    //            return true;
                    //        }
                    //    }

                    //    return false;
                    
                    //}


                };
            });
            services.AddAuthorization();
            services.AddScoped<ITokenHandler, a.TokenHandler>();
            return services;
        }
    }
}
