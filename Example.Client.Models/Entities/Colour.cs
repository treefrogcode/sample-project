using Example.Client.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Example.Client.Models.Entities
{
    public class Colour : BaseEntity, IIdentifiableEntity, INamedEntity
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

        public string EntityName
        {
            get { return Name; }
            set { Name = value; }
        }
    }
}