using System;
using System.Collections.Generic;

namespace Cat
{
    public class PrimitiveHandler
    {
        public static object TryCast(string type, object value)
        {
            if (IsPrimitive(type))
            {
                try
                {
                    switch (type)
                    {
                        case "byte": return (byte) value;
                        case "int": return (int) value;
                        case "long": return (long) value;
                        case "angle": return CastToAngle(value);
                        case "string": return (string) value;
                        case "bool": return (bool) value;
                        case "float": return (float) value;
                        case "double": return (double) value;
                        case "precise": return CastToPrecise(value);
                    }
                }
                catch (Exception e)
                {

                }
            }
        }
        
        public static object CastToAngle(object o)
        {
            return 0;
        }
        
        public static object CastToPrecise(object o)
        {
            return 0;
        }

        /// <summary>
        /// List that contains primitive type codewords
        /// </summary>
        ///
        /// <remarks>
        /// Primitive types have code equal to (-1-IndexOf(primitive))
        /// </remarks>
        public static List<string> Primitives { get; } = new List<string>()
            {"byte", "int", "long", "angle", "string", "bool", "float", "double", "precise"};
        
        public static bool IsPrimitive(string type)
        {
            return Primitives.Contains(type);
        }
    }
}