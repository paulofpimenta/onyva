using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelCompany.Models
{
    [Table("Tbl_Reservation")]
    public class Reservation
    {

        public Reservation()
        {
            this.Id = Guid.NewGuid();
        }
        [Column("PK_Reservation")]
        [Key]
        public Guid Id { get; set; }

        public ReservationStateEnum? ValidationState { get; set; }


        //[Index("UniciteInscription", IsUnique =true)]
        // [ForeignKey("Employee")]
        //public Guid IdEmploye { get; set; }

        //[Index("UniciteVoyage", IsUnique = true)]
        //[ForeignKey("Voyage")]
        //public Guid IdVoyage { get; set; }
        [Display(Name = "Employee")]
        public virtual Employee Employee { get; set; }
        [Display(Name = "Destination")]
        public virtual Voyage Voyage { get; set; }
    }
}