using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace iman.Domain;

// public class EventBus : IEventBus
// {
//     private readonly IServiceProvider _serviceProvider;
//     // private readonly Func<IServiceProvider, IDomainEvent?, TracingScope> createTracingScope;
//
//     public EventBus(IServiceProvider serviceProvider)
//     {
//         _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
//     }
//     
//     public async Task PublishAsync(IDomainEvent @event, CancellationToken cancelToken)
//     {
//         using var scope = _serviceProvider.CreateScope();
//         var handlers = scope.ServiceProvider.GetServices<IEventHandler<IDomainEvent>>();
//         foreach (var handler in handlers)
//         {
//             await handler.HandleAsync(@event, cancelToken);
//         }
//     }
// }