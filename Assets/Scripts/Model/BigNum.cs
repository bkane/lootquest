using System;
using UnityEngine;

namespace Assets.Scripts.Model
{
    [Serializable]
    public struct BigNum
    {
        //public Units Type;
        public float value; //maybe double?


        public BigNum(float d)
        {
            value = d;
        }

        public static implicit operator BigNum(float d)
        {
            return new BigNum(d);
        }

        public static implicit operator float(BigNum b)
        {
            return b.value;
        }

        public override string ToString()
        {
            string[] suffixes = { "", "K", "M", "B", "T", "Quad", "Quin", "Sext", "Sept", "Oct", "Non", "Dec", "Undec" };

            if (value == 0)
            {
                return "0";
            }
            else
            {
                int place = Mathf.FloorToInt(Mathf.Log(value, 1000));

                if (place >= 0)
                {
                    double val = Math.Round(value / Mathf.Pow(1000, place), 2);

                    if (place < 2)
                    {
                        return val.ToString("0");
                    }
                    else
                    {
                        return val.ToString("0.00") + suffixes[place];
                    }
                }
                else
                {
                    return value.ToString("0.00");
                }
            }
        }
    }
}
