using MediConsult.Domain.Primitives;
using MediConsult.Domain.Repositories;
using MediConsult.Infrastructure.Abstractions;
using System.Data.Common;
using System.Data.SqlClient;

namespace MediConsult.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IUserRepository _userRepository;
    private IPatientRepository _patientRepository;
    private readonly IAppSettings _appSettings;
    public UnitOfWork(
        IAppSettings appSettings)
    {
        _appSettings = appSettings;
    }
    public IUserRepository UserRepository 
    {
        get { return _userRepository; }
        set { _userRepository = value ?? new UserRepository(_appSettings); }
    }
    public IPatientRepository PatientRepository
    {
        get { return _patientRepository; }
        set { _patientRepository = value ?? new PatientRepository(_appSettings); }
    }

    public async Task<DbTransaction> BeginTransactionAsync()
    {
        using var connection = new SqlConnection(_appSettings.ConnectionString);
        return await connection.BeginTransactionAsync();
    }

    public async Task CommitAsync(DbTransaction transaction)
    {
        await transaction.CommitAsync();
    }

    public async Task RollbackAsync(DbTransaction transaction)
    {
        await transaction.RollbackAsync();
    }
}
