using System.Net;
using System.Numerics;
using System.Reflection.Metadata;
using System.Reflection;

namespace MediConsult.Domain.Entities;

public record User
{
    public User() { }
    public User(
        int id,
        string username,
        string firtName,
        string lastName,
        string password)
    {
        Id = id;
        UserName = username;
        FirstName = firtName;
        LastName = lastName;
        Password = password;
    }
    public int Id { get; init; }
    public string UserName { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Password { get; init; }
    public DateTime CreatedAt { get; init; }
    public bool IsActive { get; init; }
}