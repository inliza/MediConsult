using MediConsult.Domain.Entities;
using MediConsult.Domain.Primitives;
using MediConsult.Domain.Repositories;
using MediConsult.Infrastructure.Abstractions;

namespace MediConsult.Infrastructure.Repositories;

public class PatientRepository : BaseRepository<Patient>, IPatientRepository
{
    public PatientRepository(IAppSettings appSettings) : base(appSettings)
    {
    }
}
