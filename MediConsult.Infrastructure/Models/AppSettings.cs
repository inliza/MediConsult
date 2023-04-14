using MediConsult.Domain.Primitives;
using Microsoft.Extensions.Configuration;

namespace MediConsult.Infrastructure.Models;

public class AppSettings : IAppSettings
{
    private readonly IConfiguration _configuration;
    public AppSettings(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string ConnectionString
    {
        get
        {
            return _configuration.GetConnectionString("MediConsultDb");
        }
    }
}