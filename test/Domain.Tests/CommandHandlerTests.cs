using System.Threading;
using System.Threading.Tasks;
using iman.Domain;
using iman.Domain.Messaging.Extensions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Domain.Tests;

[TestFixture]
public class CommandHandlerTests
{
    private ICommandBus _commandBus;
    
    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddDomainCore();
        var provider = services.BuildServiceProvider();

        _commandBus = provider.GetRequiredService<ICommandBus>();
    }

    [Test]
    public async Task Command_handler_command_args_effected_when_published()
    {
        var cmd = new SampleCommand();
        cmd.Input = "iman";
        await _commandBus.ExecuteAsync(cmd).ConfigureAwait(false);
        var expectedOutput = $"The input was {cmd.Input}";
        Assert.AreEqual(expectedOutput, cmd.Output);
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