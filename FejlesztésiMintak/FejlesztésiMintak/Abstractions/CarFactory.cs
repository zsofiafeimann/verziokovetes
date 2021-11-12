using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FejlesztésiMintak.Abstractions
{
    public interface CarFactory
    {
    public Toy CreateNew()
        {
            return new Ball();
        }
    }
}
}
