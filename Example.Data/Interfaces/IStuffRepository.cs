﻿using Example.Business.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Data.Interfaces
{
    public interface IStuffRepository : IDataRepository<Stuff>
    {
        Stuff GetByOne(string one);
    }
}
