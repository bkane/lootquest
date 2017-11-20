﻿using System;
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


        //public BigNum(Units type, float d)
        //{
        //    Type = type;
        //    value = d;
        //}

        //public static BigNum operator +(BigNum a, BigNum b)
        //{
        //    if (a.Type == b.Type)
        //    {
        //        return new BigNum(a.Type, a.value + b.value);
        //    }
        //    else
        //    {
        //        throw new InvalidOperationException();
        //    }
        //}

        //public static BigNum operator *(BigNum a, BigNum b)
        //{
        //    if (a.Type == b.Type)
        //    {
        //        return new BigNum(a.Type, a.value * b.value);
        //    }
        //    else
        //    {
        //        throw new InvalidOperationException();
        //    }
        //}

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
        }
    }
}
