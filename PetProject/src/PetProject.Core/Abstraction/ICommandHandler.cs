using System.Windows.Input;
using CSharpFunctionalExtensions;
using PetProject.Contracts;
using PetProject.SharedKernel;

namespace PetProject.Core.Abstraction;

public interface ICommandHandler<TResponse, in TCommand>
{
    public Task<Result<TResponse, ErrorList>> Handle(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    public Task<UnitResult<ErrorList>> Handle(TCommand command, CancellationToken cancellationToken);
}
