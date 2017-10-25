using Example.Client.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Example.Client.Models.Entities
{
    public class Colour : BaseEntity, IIdentifiableEntity
    {
        public int ColourId { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<Stuff> Stuff { get; set; }

        public int EntityId
        {
            get { return ColourId; }
            set { ColourId = value; }
        }
    }
}