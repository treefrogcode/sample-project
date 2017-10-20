using System.ComponentModel.DataAnnotations;

namespace Example.Client.Models.Entities
{
    public class Stuff : BaseEntity
    {
        public int StuffId { get; set; }

        [Required]
        public string One { get; set; }

        [Required]
        public string Two { get; set; }

        public string Three { get; set; }
    }
}