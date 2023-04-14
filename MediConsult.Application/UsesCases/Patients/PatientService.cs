using MediConsult.Application.Interfaces;
using MediConsult.Application.UsesCases.Patients.Constants;
using MediConsult.Application.UsesCases.Patients.Request;
using MediConsult.Domain.Entities;
using MediConsult.Domain.Repositories;
using MediConsult.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Reflection;

namespace MediConsult.Application.UsesCases.Patients;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IDataHelper _dataHelper;
    private readonly ILogger<PatientService> _logger;
    public PatientService(
        IPatientRepository patientRepository, IDataHelper dataHelper, ILogger<PatientService> logger)
    {
        _patientRepository = patientRepository;
        _dataHelper = dataHelper;
        _logger = logger;
    }
    public async Task<ServicesResponse> CreatePatient(CreatePatientRequest createPatient)
    {
        try
        {
            await _patientRepository.Insert(PatientsConstans.CreatePatientSP, _dataHelper.GetSqlParameters(new
            {
                createPatient.FirstName,
                createPatient.LastName,
                createPatient.Address,
                createPatient.Gender,
                createPatient.Document,
                createPatient.Phone
            }));

            return new ServicesResponse(200, "Ok", createPatient);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while inserting the patient {createPatient.FirstName} Ex: {ex.Message}");
            return new ServicesResponse(500, $"An error occurred while inserting the patient {createPatient.FirstName} Ex: {ex.Message}", createPatient);
        }
    }

    public async Task<ServicesResponse> UpdatePatient(int id, UpdatePatientRequest updatePatient)
    {
        try
        {
            var patient = await Get(id);
            if (patient.Code != 200)
            {
                return new ServicesResponse(404, "The specified patient id does not exist", null);
            }
            await _patientRepository.Update(PatientsConstans.UpdatePatientSP, _dataHelper.GetSqlParameters(new
            {
                Id = id,
                updatePatient.FirstName,
                updatePatient.LastName,
                updatePatient.Address,
                updatePatient.Gender,
                updatePatient.Document,
                updatePatient.Phone
            }));
            return new ServicesResponse(200, "Ok", updatePatient);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while updating the patient {updatePatient.FirstName} Ex: {ex.Message}");
            return new ServicesResponse(500, $"An error occurred while updating the patient {updatePatient.FirstName} Ex: {ex.Message}", updatePatient);
        }
    }

    public async Task<ServicesResponse<List<Patient>>> GetAll()
    {
        try
        {
            var results = await _patientRepository.GetAll(PatientsConstans.PatientTableName);
            List<ServicesResponse> response = new List<ServicesResponse>();
            if (results.Any())
            {
                return new ServicesResponse<List<Patient>>(200, "Ok", results);
            }
            return new ServicesResponse<List<Patient>>(404, "No patients found", null);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting the list of patients Ex: {ex.Message}");
            return new ServicesResponse<List<Patient>>(500, $"An error occurred while getting the list of patients Ex: {ex.Message}", null);
        }
    }

    public async Task<ServicesResponse?> Delete(int id)
    {
        try
        {
            var patient = await Get(id);
            if (patient.Code != 200)
            {
                return new ServicesResponse(404, "The specified patient id does not exist", null);
            }
            await _patientRepository.DeleteLogic(PatientsConstans.PatientTableName, id);
            return new ServicesResponse(200, "Successfully delete", id);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while deleting the list of patients Ex: {ex.Message}");
            return new ServicesResponse(500, $"An error occurred while deleting the patient {id} Ex: {ex.Message}", null);
        }
    }

    public async Task<ServicesResponse<Patient>> Get(int id)
    {
        try
        {
            var result = await _patientRepository.Get(PatientsConstans.PatientTableName, id);
            if (result == null)
            {
                return new ServicesResponse<Patient>(404, $"The specified patient id {id} does not exist", null);
            }
            return new ServicesResponse<Patient>(200, "Ok", result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting the patient {id} Ex: {ex.Message}");
            return new ServicesResponse<Patient>(500, $"An error occurred while getting the patient {id} Ex: {ex.Message}", null);
        }
    }


}
