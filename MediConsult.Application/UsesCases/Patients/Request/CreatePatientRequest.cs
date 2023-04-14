namespace MediConsult.Application.UsesCases.Patients.Request;

public class CreatePatientRequest
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Address { get; init; }
    public string Gender { get; init; }
    public string Document { get; init; }
    public string Phone { get; init; }
}
