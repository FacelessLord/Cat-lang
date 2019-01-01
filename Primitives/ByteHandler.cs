using System;

namespace Cat.Primitives
{
    public class ByteHandler
    {
        public static byte Cast(object value)
        {
            try
            {
                return (byte) value;
            }
            catch (Exception e)
            {
                return ByteCalculator.TryCount(value);
            }
        }
    }
}