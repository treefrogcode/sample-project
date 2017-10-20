using Example.Business.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Example.Business.Models.Entities
{
    public class Stuff : BaseEntity, IIdentifiableEntity
    {
        public int StuffId { get; set; }
        public string One { get; set; }
        public string Two { get; set; }
        public string Three { get; set; }
        public int EntityId
        {
            get { return StuffId; }
            set { StuffId = value; }
        }
    }
}