using Example.Business.Models.Interfaces;
using System.Collections.Generic;

namespace Example.Business.Models.Entities
{
    public class Stuff : BaseEntity, IIdentifiableEntity, IOwnedEntity
    {
        public int StuffId { get; set; }

        public string One { get; set; }

        public string Two { get; set; }

        public string Three { get; set; }

        public int? ColourId { get; set; }

        public string Owner { get; set; }

        public virtual Colour Colour { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public int EntityId
        {
            get { return StuffId; }
            set { StuffId = value; }
        }

        public string EntityOwner
        {
            get { return Owner; }
            set { Owner = value; }
        }
    }
}