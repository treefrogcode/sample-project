using Example.Business.Models.Interfaces;
using System;

namespace Example.Business.Models.Entities
{
    public class Token : BaseEntity, IIdentifiableEntity
    {
        public int TokenId { get; set; }
        public string Guid { get; set; }
        public bool IsPublic { get; set; }
        public string System { get; set; }
        public DateTime LastAccessed { get; set; }
        public int EntityId
        {
            get { return TokenId; }
            set { TokenId = value; }
        }
    }
}