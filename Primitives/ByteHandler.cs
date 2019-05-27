using System;
using System.Collections.Generic;
using Cat.Calculators;

namespace Cat.Primitives
{
    public class ByteHandler
    {
        public static byte Cast(object value, Dictionary<string,byte> variables)
        {
            try
            {
                return (byte) value;
            }
            catch (Exception e)
            {
                var math = new ByteMath();
                return math.TryCount(value,variables);
            }
        }
    }
}