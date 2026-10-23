using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CatalogApp.Domain.Entities
{
    public class Crew
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Machine))]
        public int MachineId { get; set; }
        public Machine Machine { get; set; }

        [Required]
        [ForeignKey(nameof(Driver))]
        public int DriverID { get; set; }
        public Driver Driver { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
