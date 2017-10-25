using System;
using Example.Client.Models.Interfaces;

namespace Example.Client.Models.Entities
{
    public class Category : BaseEntity, IIdentifiableEntity, INamedEntity
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public int EntityId
        {
            get { return CategoryId; }
            set { CategoryId = value; }
        }

        public string EntityName
        {
            get { return Name; }
            set { Name = value; }
        }
    }
}