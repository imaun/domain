using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace iman.Domain.Messaging.Extensions;

 /// <summary>
    /// from : https://github.com/dasiths/SimpleMediator/blob/9680205111da9f4573a06b38fa2b12949c2ac8bc/SimpleMediator.Extensions.Microsoft.DependencyInjection/ServiceCollectionExtensions.cs
    /// Extensions to scan for Mediator handlers and registers them.
    /// - Scans for any handler interface implementations and registers them as <see cref="ServiceLifetime.Transient"/>
    /// - Scans for any <see cref="IMessagePreProcessor{TMessage}"/> and <see cref="IMessagePostProcessor{TMessage,TResponse}"/> implementations and registers them as scoped instances
    /// Registers <see cref="ServiceFactory"/> and <see cref="IMediator"/> as scoped instances
    /// After calling AddMediator you can use the container to resolve an <see cref="IMediator"/> instance.
    ///
    /// This class and methods has been *heavily* influenced by __jbogard/MediatR.Extensions.Microsoft.DependencyInjection__
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddDomainCore(this IServiceCollection services) {
            services.AddMediator();
            services.AddMediatorMiddleware();
            return services;
        }

        /// <summary>
        /// Registers handlers and the mediator types from <see cref="AppDomain.CurrentDomain"/>.
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddMediator(this IServiceCollection services)
            => services.AddMediator(AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic));

        /// <summary>
        /// Registers handlers and mediator types from the specified assemblies
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="assemblies">Assemblies to scan</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddMediator(this IServiceCollection services, params Assembly[] assemblies)
            => services.AddMediator(assemblies.AsEnumerable());

        /// <summary>
        /// Registers handlers and mediator types from the specified assemblies
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="assemblies">Assemblies to scan</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddMediator(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            ReflectionUtilities.AddRequiredServices(services);

            ReflectionUtilities.AddMediatorClasses(services, assemblies);

            return services;
        }

        /// <summary>
        /// Registers handlers and mediator types from the assemblies that contain the specified types
        /// </summary>
        /// <param name="services"></param>
        /// <param name="handlerAssemblyMarkerTypes"></param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddMediator(this IServiceCollection services, params Type[] handlerAssemblyMarkerTypes)
            => services.AddMediator(handlerAssemblyMarkerTypes.AsEnumerable());

        /// <summary>
        /// Registers handlers and mediator types from the assemblies that contain the specified types
        /// </summary>
        /// <param name="services"></param>
        /// <param name="handlerAssemblyMarkerTypes"></param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddMediator(this IServiceCollection services, IEnumerable<Type> handlerAssemblyMarkerTypes)
        {
            ReflectionUtilities.AddRequiredServices(services);
            ReflectionUtilities.AddMediatorClasses(services, handlerAssemblyMarkerTypes.Select(t => t.GetTypeInfo().Assembly));
            return services;
        }

        public static IServiceCollection AddMediatorMiddleware(this IServiceCollection services) =>
            services.AddMediatorMiddleware(AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic));

        public static IServiceCollection AddMediatorMiddleware(this IServiceCollection services, params Assembly[] assemblies)
            => services.AddMediatorMiddleware(assemblies.AsEnumerable());

        public static IServiceCollection AddMediatorMiddleware(this IServiceCollection services,
            IEnumerable<Assembly> assembliesToScan)
        {
            return ReflectionUtilities.AddMediatorMiddleware(services, assembliesToScan);
        }

    }