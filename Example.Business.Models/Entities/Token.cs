using Example.Business.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Example.Business.Models.Entities
{
    public class Token : BaseEntity, IIdentifiableEntity
    {
        public int TokenId { get; set; }
        public string Guid { get; set; }
        public string System { get; set; }
        public DateTime LastAccessed { get; set; }
        public int EntityId
        {
            get { return TokenId; }
            set { TokenId = value; }
        }
    }
}