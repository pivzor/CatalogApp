using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CatalogApp.Domain.Entities
{
    public class Machine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(100)]
        public string Model { get; set; }

        [Required]
        [MaxLength(100)]
        public string Number { get; set; }

        public int Year {  get; set; }

        public ICollection<Crew> Crews { get; set; } = new List<Crew>();
    }
}
