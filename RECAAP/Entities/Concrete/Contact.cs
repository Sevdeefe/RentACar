using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Contact :IEntitiy
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string EPosta { get; set; }
        public string Konu { get; set; }
        public string Mesaj { get; set; }
    }
    
}
