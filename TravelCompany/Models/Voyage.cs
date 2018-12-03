using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelCompany.Models
{
    [Table("Tbl_Voyage")]
    public class Voyage
    {
        public Voyage()
        {
            this.Id = Guid.NewGuid();
            this.DateOfVoyage = DateTime.Now.AddDays(10);
            this.MaxNumVoyagers = 50;
            this.Reservations = new HashSet<Reservation>();
        }
        [Column("PK_Voyage")]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Destination")]
        //[RegularExpression("[A-Z][a-z]{1,49}")]
        [MaxLength(50)]
        public string Destination { get; set; }

        [Display(Name = "Maximum number of travelers")]
        //[Range(11, 100, ErrorMessage = "{} must be beteween {1} and {2}")]
        public int MaxNumVoyagers { get; set; }

        [Display(Name = "Date of the travel")]
        [Column(TypeName = "datetime2")]
        public DateTime DateOfVoyage { get; set; }

        [Display(Name = "State")]
        public bool IsAvailable { get; set; }

        public bool HasEmployee(Employee e)
        {
            return this.Reservations.Any(c => c.Employee == e);
        }


    public virtual ICollection<Reservation> Reservations { get; set; }


    }
}