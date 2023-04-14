using MediConsult.Application.Interfaces;
using MediConsult.Application.UsesCases.Patients.Request;
using MediConsult.Application.Validators;
using MediConsult.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediConsult.API.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientsController(
            IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody, Bind] CreatePatientRequest createPatient)
        {
            var validator = new PatientValidator();
            var result = validator.Validate(createPatient);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => string.Format("Property {0} failed validation. Error was: {1}", e.PropertyName, e.ErrorMessage));
                return BadRequest(errors);
            }
            var patient = await _patientService.CreatePatient(createPatient);
            return StatusCode(patient.Code, patient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient([FromRoute] int id, [FromBody, Bind] UpdatePatientRequest updatePatient)
        {
            var validator = new PatientUpdateValidator();
            var result = validator.Validate(updatePatient);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => string.Format("Property {0} failed validation. Error was: {1}", e.PropertyName, e.ErrorMessage));
                return BadRequest(errors);
            }

            var patient = await _patientService.UpdatePatient(id,updatePatient);
            return StatusCode(patient.Code, patient);
        }

        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
           
            var patient = await _patientService.GetAll();
            return StatusCode(patient.Code, patient);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient([FromRoute]int id)
        {

            var patient = await _patientService.Get(id);
            if (patient == null)
                return NotFound("The specified patient id does not exist");
            return StatusCode(patient.Code, patient);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
                var res = await _patientService.Delete(id);
            return StatusCode(res.Code, res);
        }
    }
}
