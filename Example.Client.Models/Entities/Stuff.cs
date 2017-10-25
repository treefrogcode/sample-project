using Example.Client.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Example.Client.Models.Entities
{
    public class Stuff : BaseEntity, IIdentifiableEntity
    {
        public int StuffId { get; set; }

        [Required]
        public string One { get; set; }

        [Required]
        public string Two { get; set; }

        public string Three { get; set; }

        public int? ColourId { get; set; }

        public Colour Colour { get; set; }

        public ICollection<Category> Categories { get; set; }

        public int EntityId
        {
            get { return StuffId; }
            set { StuffId = value; }
        }
    }
}