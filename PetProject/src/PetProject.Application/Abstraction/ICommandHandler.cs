using CSharpFunctionalExtensions;
using PetProject.Domain.Shared.ValueObjects;

namespace PetProject.Application.Abstraction;

public interface ICommandHandler<TResponse, in TCommand> where TCommand : ICommand
{
    public Task<Result<TResponse, ErrorList>> Handle(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    public Task<UnitResult<ErrorList>> Handle(TCommand command, CancellationToken cancellationToken);
}
