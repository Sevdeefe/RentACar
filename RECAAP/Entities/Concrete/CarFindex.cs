﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class CarFindeks :IEntitiy
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int FindeksScore { get; set; }
    }
}
