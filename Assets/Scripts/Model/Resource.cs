using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model
{
    public struct Resource
    {
        public Units Type;
        public BigNum Amount;

        public Resource(Units type, BigNum amount)
        {
            Type = type;
            Amount = amount;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Amount, Type);
        }
    }
}
