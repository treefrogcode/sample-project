using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Business.Models.Interfaces
{
    public interface IIdentifiableEntity
    {
        int EntityId { get; set;  }
    }
}
