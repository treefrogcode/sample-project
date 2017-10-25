using Example.Business.Models.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace Example.Business.Models.Entities
{
    public class Colour : BaseEntity, IIdentifiableEntity
    {
        public int ColourId { get; set; }

        public string Name { get; set; }

        public int EntityId
        {
            get { return ColourId; }
            set { ColourId = value; }
        }
    }
}