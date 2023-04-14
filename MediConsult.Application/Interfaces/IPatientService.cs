using MediConsult.Application.UsesCases;
using MediConsult.Application.UsesCases.Patients.Request;
using MediConsult.Domain.Entities;

namespace MediConsult.Application.Interfaces;

public interface IPatientService
{
    Task<ServicesResponse> CreatePatient(CreatePatientRequest createPatient);
    Task<ServicesResponse> UpdatePatient(int id,UpdatePatientRequest updatePatient);
    Task<ServicesResponse<List<Patient>>> GetAll();
    Task<ServicesResponse<Patient>> Get(int id);
    Task<ServicesResponse?> Delete(int id);

}
