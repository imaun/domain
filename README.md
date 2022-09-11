# Domain
iman.Domain is the library I've built for using in **DDD** and **CQRS** based projects using `.NET` and `C#`.
It consist of primary building blocks for common domain-driven applications such as `AggregateRoot` and `Entity`.

## Features
- Support for `AggregateRoot` and `Entity` with or without identity
- `DomainEvent` for raising business events in Aggregates
- `Repository` and `Service` contracts for Domain models
- `ValueObject` contract for implementing ValueObjects
- Implementation of `Mediator` pattern 
- Support for `Command`, `Query` and `Event` and automatically matched them with their respective handlers
- `DomainException` contract to define business exceptions


### How to use
For using Domain building blocks, you just need to reference this library and inherit from 
the block you need to use in your application. But for using the Mediator, in order to 
implement `CQRS` pattern, you need to register `iman.Domain` services in the default 
.NET container, like this :

```csharp
var services = new ServiceCollection();
services.AddDomainCore();
```
**⚠**️ If you use any other DI container other than `Microsoft.Extensions.DependencyInjection` 
you must register `iman.Domain` services yourself. 

### Examples
For now, you can see the samples of the library in `Tests` project, I will
add some samples very soon ;)

### Contribution
Feel free to fork this project, commit your codes and send pull requests.

