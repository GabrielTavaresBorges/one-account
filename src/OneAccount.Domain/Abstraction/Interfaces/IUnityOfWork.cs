namespace OneAccount.Domain.Abstraction.Interfaces;

public interface IUnityOfWork
{
    Task CommitAsync();
}

