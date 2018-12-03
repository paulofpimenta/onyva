using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelCompany.Models
{
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


        [Index("UniciteInscription", IsUnique =true)]
        [ForeignKey("Employee")]
        public Guid IdEmploye { get; set; }

        [Index("UniciteVoyage", IsUnique = true)]
        [ForeignKey("Voyage")]
        public Guid IdVoyage { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Voyage Voyage { get; set; }
    }
}