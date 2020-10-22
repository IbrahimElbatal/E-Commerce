using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace UsingRepository.Core.Models
{
    public class Country
    {
        public Country()
        {
            Cities = new Collection<City>();
        }
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}