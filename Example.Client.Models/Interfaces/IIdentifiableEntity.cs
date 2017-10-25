using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Client.Models.Interfaces
{
    public interface IIdentifiableEntity
    {
        int EntityId { get; set;  }
    }
}
