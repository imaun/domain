using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using iman.Domain;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Domain.Tests;

[TestFixture]
public class QueryHandlerTests
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
    public async Task Query_handler_executes_query_and_return_valid_result()
    {
        var query = new GetByNameQuery("iman");
        var result = await _mediator.PublishAsync(query);

        var expected = new[]
        {
            new Item
            {
                Name = "Iman", Count = 2
            },
            new Item { Name = "Iman", Count = 3 }
        }.ToArray();
        
        // Assert.That(expected, Is.EquivalentTo(result.ToArray()));
        CollectionAssert.AreEqual(expected, result.ToArray());
    }
}

public class Item
{
    public string Name { get; set; }
    public int Count { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        var objToCompare = obj as Item;
        if (this.Name.ToUpper() == objToCompare.Name.ToUpper() &&
            this.Count == objToCompare.Count)
            return true;
        return false;
    }

    public override int GetHashCode()
    {
        return Count * 1000;
    }
}

public static class Items
{
    public static IEnumerable<Item> __source = new[]
    {
        new Item { Name = "Iman", Count = 2 },
        new Item { Name = "Iman", Count = 3 },
        new Item { Name = "Ahmad", Count = 6 },
        new Item { Name = "Filan", Count = 2 }
    };
}

public class GetByNameQuery : IQuery<IReadOnlyCollection<Item>> 
{
    
    public string Name { get; set; }

    public GetByNameQuery(string name) => Name = name;
}

public class QueryHandlers : IQueryHandler<GetByNameQuery, IReadOnlyCollection<Item>>
{
    
    public async Task<IReadOnlyCollection<Item>> HandleAsync(
        GetByNameQuery message, CancellationToken cancellationToken)
    {
        var result = Items.__source
            .Where(_ => _.Name.ToUpper() == message.Name.ToUpper())
            .ToList();
        return result;
    }
}

