namespace MediConsult.Domain.Entities;

public record Patient
{
    public Patient() { }
    public Patient(
        string firtName,
        string lastName,
        string address,
        string gender,
        string document,
        string phone)
    {
        FistName = firtName;
        LastName = lastName;
        Address = address;
        Gender = gender;
        Document = document;
        Phone = phone;
    }
    public int Id { get; init; }
    public string FistName { get; init; }
    public string LastName { get; init; }
    public string Address { get; init; }
    public string Gender { get; init; }
    public string Document { get; init; }
    public string Phone { get; init; }
    public DateTime CreatedAt { get; init; }
    public bool IsDeleted { get; init; }
}
