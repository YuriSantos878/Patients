using Azure;
using build_project_2024.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;

namespace build_project_2024.Pages.Patients
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }

    }
}
using BuildProjectSummer2024.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuildProjectSummer2024.Pages.Patient
{
    public class IndexModel : PageModel
    {
        private readonly BuildProject2024Context _context;
        public List<Models.Patient> Patients;

        public IndexModel(BuildProject2024Context context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Patients = _context.Patients.Select(x => new Models.Patient
            {
                Id = x.Id,
                Adress = x.Adress,
                City = x.City,
                ClaimsCount = x.MedicalClaims.Count,
                Gender = x.Gender,
                LastName = x.LastName,
                Mrn = x.Mrn,
                Ssn = x.Ssn,
                HospitalId = x.HospitalId,
                Hospital = new Models.Hospital
                {
                    Id = x.HospitalId,
                    HospitalName = x.Hospital.HospitalName
                },
                Dob = x.Dob,
                FirstName = x.FirstName,
                InsuranceMemberId = x.InsuranceMemberId,
                Phone = x.Phone,
                State = x.State,
                Zip = x.Zip
            }).ToList();
        }
    }
}
public void OnGet(int? hospitalId)
{
    Patients = _context.Patients.Where(x => !hospitalId.HasValue || x.HospitalId == hospitalId).Select(x => new Models.Patient
    {
        Id = x.Id,
        Adress = x.Adress,
        City = x.City,
        ClaimsCount = x.MedicalClaims.Count,
        Gender = x.Gender,
        LastName = x.LastName,
        Mrn = x.Mrn,
        Ssn = x.Ssn,
        HospitalId = x.HospitalId,
        Hospital = new Models.Hospital
        {
            Id = x.HospitalId,
            HospitalName = x.Hospital.HospitalName
        },
        Dob = x.Dob,
        FirstName = x.FirstName,
        InsuranceMemberId = x.InsuranceMemberId,
        Phone = x.Phone,
        State = x.State,
        Zip = x.Zip
    }).ToList();
}
using BuildProjectSummer2024.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BuildProjectSummer2024.Pages.Patient
{
    public class IndexModel : PageModel
    {
        private readonly BuildProject2024Context _context;
        public List<Models.Patient> Patients;
        public int PageIndex { get; set; } //NEW LINE
        public int TotalPages { get; set; } //NEW LINE
        public bool HasPreviousPage => PageIndex > 1; //NEW LINE
        public bool HasNextPage => PageIndex < TotalPages; //NEW LINE

        public IndexModel(BuildProject2024Context context)
        {
            _context = context;
        }

        public void OnGet(int? hospitalId, int? pageIndex)
        {
            const int pageSize = 5;  //NEW LINE
            PageIndex = pageIndex ?? 1; //NEW LINE

            IQueryable<Models.Patient> patientsQuery = _context.Patients; //NEW LINE

            if (hospitalId.HasValue) //NEW LINE
            {
                patientsQuery = patientsQuery.Where(p => p.HospitalId == hospitalId); //NEW LINE
            }

            int totalPatients = patientsQuery.Count(); //NEW LINE
            TotalPages = (int)Math.Ceiling(totalPatients / (double)pageSize); //NEW LINE

            Patients = patientsQuery.Where(x => !hospitalId.HasValue || x.HospitalId == hospitalId).Select(x => new Models.Patient
            {
                Id = x.Id,
                Adress = x.Adress,
                City = x.City,
                ClaimsCount = x.MedicalClaims.Count,
                Gender = x.Gender,
                LastName = x.LastName,
                Mrn = x.Mrn,
                Ssn = x.Ssn,
                HospitalId = x.HospitalId,
                Hospital = new Models.Hospital
                {
                    Id = x.HospitalId,
                    HospitalName = x.Hospital.HospitalName
                },
                Dob = x.Dob,
                FirstName = x.FirstName,
                InsuranceMemberId = x.InsuranceMemberId,
                Phone = x.Phone,
                State = x.State,
                Zip = x.Zip
            })
            .Skip((PageIndex - 1) * pageSize) //NEW LINE
            .Take(pageSize) //NEW LINE
            .ToList();
        }
    }
}

< div class= "pagination" >
    @if(Model.HasPreviousPage)
    {
        < a class= "btn btn-primary m-1" asp - page = "./Index"
           asp - route - hospitalId = "@Request.Query["hospitalId"]"
           asp-route-pageIndex="@(Model.PageIndex - 1)">Previous</a>
    }

    @if(Model.HasNextPage)
    {
        < a class= "btn btn-primary m-1" asp - page = "./Index"
           asp - route - hospitalId = "@Request.Query["hospitalId"]"
           asp-route-pageIndex="@(Model.PageIndex + 1)">Next</a>
    }
</ div >
