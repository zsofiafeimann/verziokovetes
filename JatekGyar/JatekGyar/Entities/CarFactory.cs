using JatekGyar.Abstraction;
using JatekGyar.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JatekGyar.Entities
{
    public class IToyFactory : Abstractions.IToyFactory
    {
        public Abstraction.Toy CreateNew()
        {
            return new Toy();
        }


    }
}
