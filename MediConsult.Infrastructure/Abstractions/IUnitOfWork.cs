using MediConsult.Domain.Repositories;
using System.Data.Common;
namespace MediConsult.Infrastructure.Abstractions;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IPatientRepository PatientRepository { get; }
    Task<DbTransaction> BeginTransactionAsync();
    Task CommitAsync(DbTransaction transaction);
    Task RollbackAsync(DbTransaction transaction);
}
