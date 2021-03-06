﻿namespace Assets.Scripts.Model
{
    public class Resource
    {
        public Units Type;
        public BigNum Amount;
        public BigNum MaxValue;

        public Resource()
            : this(Units.Invalid, 0)
        {
        }

        public Resource(Units type, BigNum amount)
            : this(type, amount, 0)
        {
        }

        public Resource(Units type, BigNum amount, BigNum maxValue)
        {
            Type = type;
            Amount = amount;
            MaxValue = maxValue;
        }

        public override string ToString()
        {
            if (Type == Units.Money)
            {
                return string.Format("${0}", Amount);
            }
            else
            {
                return string.Format("{0} {1}", Amount, Type);
            }
        }
    }
}
