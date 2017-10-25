using Example.Client.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Example.Client.Models.Entities
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