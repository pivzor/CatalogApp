using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CatalogApp.Domain.Entities
{
    public class Driver
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName  { get; set; }

        [Required]
        [MaxLength(50)]
        public string LicenseNumber { get; set; }

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }

        public int ExperienceYears { get; set; }

        public ICollection<Crew> Crews { get; set; } = new List<Crew>();
    }
}
