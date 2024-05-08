using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net_ef_videogame
{
    internal class ValueNotFoundException : Exception
    {
        public ValueNotFoundException(string message)
       : base(message)
        {
        }
    }
}
