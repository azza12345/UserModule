using Business.Repositories;
using Business.Services;
using Core.Interfaces;
using Core.IRepositories;
using Core.IServices;

namespace UserModule
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            
            services.AddScoped<ISystemModuleService, SystemModuleService>();
          services.AddScoped<ISystemModuleRepository, SystemModuleRepository>();

            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IGroupService, GroupService>();

            services.AddScoped<IActionRepository, ActionRepository>();
            services.AddScoped<IActionService, ActionService>();

            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPermissionService, PermissionService>();

            services.AddScoped<IGroupPermissionRepository, GroupPermissionRepository>();
            services.AddScoped<IGroupPermissionService, GroupPermissionService>();






            return services;
        }
    }
}
