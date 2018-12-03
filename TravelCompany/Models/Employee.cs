﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelCompany.Models
{
    [Table("Tbl_Employee")]
    public class Employee
    {
            public Employee()
            {
                this.Id = Guid.NewGuid();
                this.DateOfHire = DateTime.Now;
                this.Reservations = new HashSet<Reservation>();
            }
            [Column("PK_Employee")]
            [Key]
            public Guid Id { get; set; }

            [Required]
            [Display(Name = "Employee's name")]
            [RegularExpression("[A-Z][a-z]{1,49}")]
            [MaxLength(50)]
            public string Nom { get; set; }


            [Required]
            [Display(Name = "Employee's surname")]
            [RegularExpression("[A-Z][a-z]{1,49}")]
            [MaxLength(50)]
            public string Surname { get; set; }

            [ColumnAttribute("Salary")]
            [Display(Name = "Employee's Salary")]
            //[Range(0, 10, ErrorMessage = "The {0} salary must be higher than {1}")]
            public int Salary { get; set; }

            [Display(Name = "Date of hire")]
            public DateTime DateOfHire { get; set; }

            [NotMapped]
            public int YearsInCompany {
            get {
                return DateTime.Now.Year - this.DateOfHire.Year;
            }
            }

        public virtual ICollection<Reservation> Reservations { get; set; }

        }
}