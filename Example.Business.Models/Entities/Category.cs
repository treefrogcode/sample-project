using Example.Business.Models.Interfaces;
using System.Collections.Generic;

namespace Example.Business.Models.Entities
{
    public class Category : BaseEntity, IIdentifiableEntity
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public int EntityId
        {
            get { return CategoryId; }
            set { CategoryId = value; }
        }
    }
}