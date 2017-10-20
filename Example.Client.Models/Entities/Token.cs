using System;

namespace Example.Client.Models.Entities
{
    public class Token : BaseEntity
    {
        public int TokenId { get; set; }
        public string Guid { get; set; }
        public string System { get; set; }
        public DateTime LastAccessed { get; set; }
    }
}