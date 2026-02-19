using OneAccount.Domain.Abstraction.Interfaces;

namespace OneAccount.Domain.Repositories;

public interface IRepository<T> where T : IAggregateRoot { }