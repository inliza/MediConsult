using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConsult.Application.UsesCases.Patients.Constants
{
    public static class PatientsConstans
    {
        public const string CreatePatientSP = "sp_CreatePatient";
        public const string UpdatePatientSP = "sp_UpdatePatient";
        public const string PatientTableName = "dbo.Patient";
    }
}
