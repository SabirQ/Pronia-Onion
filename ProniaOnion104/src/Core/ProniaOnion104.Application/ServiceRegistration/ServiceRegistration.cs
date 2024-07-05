using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace ProniaOnion104.Application.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection service)
        {
            //service.AddAutoMapper(typeof(CategoryProfile));
            service.AddAutoMapper(Assembly.GetExecutingAssembly());
            //service.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters().AddFluentValidation;
            service.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return service;

        }
    }
}
