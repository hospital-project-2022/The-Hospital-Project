using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalProject.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentDescription { get; set; }

        public virtual Department Departments { get; set; }
    }
    public class DepartmentDto
    {
        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentDescription { get; set; }
    }
}