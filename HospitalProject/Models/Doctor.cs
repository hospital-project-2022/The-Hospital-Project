using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalProject.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorID { get; set; }

        [ForeignKey("Department")]
        public int DepartmentID { get; set; }

        public string DoctorName { get; set; }

        public string DoctorExpertise { get; set; }

        public string DoctorDesignation { get; set; }

        public int DoctorAge { get; set; }

        public string DoctorContactNumber { get; set; }

        public string DoctorExperience { get; set; }

        public virtual Doctor Doctors { get; set; }
    }
    public class DoctorDto
    {

        public int DoctorID { get; set; }

        public int DepartmentID { get; set; }

        public string DoctorName { get; set; }

        public string DoctorExpertise { get; set; }

        public string DoctorDesignation { get; set; }

        public int DoctorAge { get; set; }

        public string DoctorContactNumber { get; set; }

        public string DoctorExperience { get; set; }

    }
}