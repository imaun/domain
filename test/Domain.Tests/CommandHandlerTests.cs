using System.Threading;
using System.Threading.Tasks;
using iman.Domain;
using iman.Domain.Messaging.Extensions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Domain.Tests;

public class CommandHandlerTests
{
    private IMediator _mediator;
    
    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddDomainCore();
        var provider = services.BuildServiceProvider();

        _mediator = provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task Test1()
    {
        var services = new ServiceCollection();
        services.AddDomainCore();
        var provider = services.BuildServiceProvider();

        _mediator = provider.GetRequiredService<IMediator>();
        
        var cmd = new SampleCommand();
        cmd.Input = "iman";
        await _mediator.PublishAsync(cmd).ConfigureAwait(false);
        var expectedOutput = $"The input was {cmd.Input}";
        Assert.Equals(cmd.Input, cmd.Output);
    }
}

public class SampleCommand : ICommand
{
    public string Input { get; set; }
    public string Output { get; set; }
}

public class SampleCommandHandler : ICommandHandler<SampleCommand>
{

    public Task HandleAsync(SampleCommand message, CancellationToken cancellationToken)
    {
        message.Output = $"The input was {message.Input}";
        return Task.CompletedTask;
    }
}