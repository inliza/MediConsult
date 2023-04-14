using MediConsult.Application.Interfaces;
using MediConsult.Application.UsesCases;
using MediConsult.Application.UsesCases.Patients;
using MediConsult.Application.UsesCases.Patients.Constants;
using MediConsult.Application.UsesCases.Patients.Request;
using MediConsult.Domain.Entities;
using MediConsult.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System.Data.SqlClient;

namespace MediConsult.Tests
{
    public class PatientServiceTests
    {
        private readonly Mock<IPatientRepository> _patientRepositoryMock = new();
        private readonly Mock<IDataHelper> _dataHelperMock = new();
        private readonly Mock<ILogger<PatientService>> _loggerMock = new();
        private readonly PatientService _patientService;

        public PatientServiceTests()
        {
            _patientService = new PatientService(_patientRepositoryMock.Object, _dataHelperMock.Object, _loggerMock.Object);
        }
        [Fact]
        public async Task CreatePatient_ShouldReturn200Response_WhenPatientIsCreatedSuccessfully()
        {
            // Arrange
            var createPatientRequest = new CreatePatientRequest
            {
                FirstName = "John",
                LastName = "Doe",
                Address = "123 Main St",
                Gender = "Male",
                Document = "123456789",
                Phone = "555-5555"
            };

            _patientRepositoryMock.Setup(x => x.Insert(It.IsAny<string>(), It.IsAny<List<SqlParameter>>()))
                .Returns(Task.CompletedTask);
            // Act
            var response = await _patientService.CreatePatient(createPatientRequest);

            // Assert
            Assert.Equal(200, response.Code);
            Assert.Equal("Ok", response.Message);
            Assert.Equal(createPatientRequest, response.Data);
        }

        [Fact]
        public async Task CreatePatient_ShouldReturn500Response_WhenPatientInsertionFails()
        {
            // Arrange
            var createPatientRequest = new CreatePatientRequest
            {
                FirstName = "John",
                LastName = "Doe",
                Address = "123 Main St",
                Gender = "Male",
                Document = "123456789",
                Phone = "555-5555"
            };

            _patientRepositoryMock.Setup(x => x.Insert(It.IsAny<string>(), It.IsAny<List<SqlParameter>>()))
                .ThrowsAsync(new Exception("An error occurred while inserting the patient"));

            // Act
            var response = await _patientService.CreatePatient(createPatientRequest);

            // Assert
            Assert.Equal(500, response.Code);
            Assert.Contains("An error occurred while inserting the patient", response.Message);
            Assert.Equal(createPatientRequest, response.Data);
        }

        [Fact]
        public async Task UpdatePatient_ReturnsOkResponse_WhenPatientExists()
        {
            // Arrange
            int id = 1;
            var updatePatient = new UpdatePatientRequest
            {
                FirstName = "John",
                LastName = "Doe",
                Address = "123 Main St",
                Gender = "Male",
                Document = "123456789",
                Phone = "555-5555"
            };
            var patient = new Patient
            {
                Id = id,
                FistName = "Jane",
                LastName = "Doe",
                Address = "456 Main St",
                Gender = "Female",
                Document = "987654321",
                Phone = "555-1234"
            };
            var sqlParams = new List<SqlParameter> { };

            _patientRepositoryMock.Setup(x => x.Get(PatientsConstans.PatientTableName, id)).ReturnsAsync(patient);
            _dataHelperMock.Setup(x => x.GetSqlParameters(It.IsAny<object>())).Returns(sqlParams);
            _patientRepositoryMock.Setup(x => x.Update(PatientsConstans.UpdatePatientSP, sqlParams)).Returns(Task.CompletedTask);

            // Act
            var result = await _patientService.UpdatePatient(id, updatePatient);

            // Assert
            Assert.Equal(200, result.Code);
            Assert.Equal("Ok", result.Message);
            Assert.Equal(updatePatient, result.Data);
            _patientRepositoryMock.Verify(x => x.Update(PatientsConstans.UpdatePatientSP, sqlParams), Times.Once);
        }

        [Fact]
        public async Task GetAll_WithPatientsInDatabase_ReturnsSuccessResponse()
        {
            // Arrange
            var patients = new List<Patient>
        {
            new Patient { Id = 1, FistName = "John", LastName = "Doe" },
            new Patient { Id = 2, FistName = "Jane", LastName = "Doe" }
        };
            _patientRepositoryMock.Setup(x => x.GetAll(PatientsConstans.PatientTableName))
                .ReturnsAsync(patients);
            _dataHelperMock.Setup(x => x.GetSqlParameters(It.IsAny<object>())).Returns(new List<SqlParameter> { });

            // Act
            var result = await _patientService.GetAll();

            // Assert
            Assert.Equal(200, result.Code);
            Assert.Equal("Ok", result.Message);
            Assert.Equal(patients, result.Data);
        }

        [Fact]
        public async Task GetAll_WithNoPatientsInDatabase_ReturnsNotFoundResponse()
        {
            // Arrange
            var patients = new List<Patient>();
            _patientRepositoryMock.Setup(x => x.GetAll(PatientsConstans.PatientTableName))
                .ReturnsAsync(patients);
            _dataHelperMock.Setup(x => x.GetSqlParameters(It.IsAny<object>())).Returns(new List<SqlParameter> { });

            // Act
            var result = await _patientService.GetAll();

            // Assert
            Assert.Equal(404, result.Code);
            Assert.Equal("No patients found", result.Message);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GetAll_WithDatabaseError_ReturnsErrorResponse()
        {
            // Arrange
            _patientRepositoryMock.Setup(x => x.GetAll(PatientsConstans.PatientTableName))
                .Throws(new Exception("An error occurred"));
            _dataHelperMock.Setup(x => x.GetSqlParameters(It.IsAny<object>())).Returns(new List<SqlParameter> { });

            // Act
            var result = await _patientService.GetAll();

            // Assert
            Assert.Equal(500, result.Code);
            Assert.StartsWith("An error occurred while getting the list of patients Ex:", result.Message);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task Delete_WhenPatientExists_ShouldReturn200()
        {
            // Arrange
            int patientId = 1;
            _patientRepositoryMock.Setup(x => x.Get(PatientsConstans.PatientTableName, patientId))
                .ReturnsAsync(new Patient { Id = patientId, FistName = "John", LastName = "Doe" });
            _patientRepositoryMock.Setup(x => x.DeleteLogic(PatientsConstans.PatientTableName, patientId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _patientService.Delete(patientId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.Code);
            Assert.Equal("Successfully delete", result.Message);
            Assert.Equal(patientId, result.Data);
        }

        [Fact]
        public async Task Delete_WhenPatientDoesNotExist_ShouldReturn404()
        {
            // Arrange
            int patientId = 1;
            _patientRepositoryMock.Setup(x => x.Get(PatientsConstans.PatientTableName, patientId))
                .ReturnsAsync((Patient)null);

            // Act
            var result = await _patientService.Delete(patientId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.Code);
            Assert.Equal("The specified patient id does not exist", result.Message);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task Get_ValidId_ReturnsPatient()
        {
            // Arrange
            int id = 1;
            var patient = new Patient { Id = id, FistName = "John", LastName = "Doe" };
            _patientRepositoryMock.Setup(repo => repo.Get(PatientsConstans.PatientTableName, id))
                .ReturnsAsync(patient);

            // Act
            var result = await _patientService.Get(id);

            // Assert
            _patientRepositoryMock.Verify(repo => repo.Get(PatientsConstans.PatientTableName, id), Times.Once);
            Assert.Equal(200, result.Code);
            Assert.Equal(patient, result.Data);
        }

        [Fact]
        public async Task Get_InvalidId_ReturnsNotFound()
        {
            // Arrange
            int id = 1;
            _patientRepositoryMock.Setup(repo => repo.Get(PatientsConstans.PatientTableName, id))
                .ReturnsAsync((Patient)null);

            // Act
            var result = await _patientService.Get(id);

            // Assert
            _patientRepositoryMock.Verify(repo => repo.Get(PatientsConstans.PatientTableName, id), Times.Once);
            Assert.Equal(404, result.Code);
            Assert.Null(result.Data);
            Assert.Equal($"The specified patient id {id} does not exist", result.Message);
        }

        [Fact]
        public async Task Get_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            int id = 1;
            _patientRepositoryMock.Setup(repo => repo.Get(PatientsConstans.PatientTableName, id))
                .ThrowsAsync(new Exception("Something went wrong"));

            // Act
            var result = await _patientService.Get(id);

            // Assert
            _patientRepositoryMock.Verify(repo => repo.Get(PatientsConstans.PatientTableName, id), Times.Once);
            Assert.Equal(500, result.Code);
            Assert.Null(result.Data);
            Assert.Contains($"An error occurred while getting the patient {id}", result.Message);
        }

    }
}