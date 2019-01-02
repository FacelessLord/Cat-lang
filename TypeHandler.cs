using System;
using System.Collections.Generic;
using Cat.AbstractStructure;
using Cat.Primitives;
using Cat.Structure;

namespace Cat
{
    public static class TypeHandler
    {
        public static object TryCast(string type, object value,Dictionary<string,byte> variables)
        {
            if (IsPrimitive(type))
            {
                switch (type)
                {
                    case "byte": return ByteHandler.Cast(value, variables);
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

            if (value is CatCompoundObject cco)
            {
                var obj = cco._typeObject;
                while (obj._parent != null && obj._name != type)
                {
                    obj = obj._parent;
                }

                if (obj._name == type)
                {
                    return obj;
                }
            }

            throw new NullReferenceException(); 
        }
        
        public static CatAngle CastToAngle(object o)
        {
            if (o is string s)
            {
                if (s.EndsWith("g"))
                {
                    return new CatAngle((Math.PI/200)*double.Parse(s.Substring(0, s.Length - 1)));
                }
                if (s.EndsWith("grad"))
                {
                    return new CatAngle((Math.PI/200)*double.Parse(s.Substring(0, s.Length - 4)));
                }
                if (s.EndsWith("r"))
                {
                    return new CatAngle(double.Parse(s.Substring(0, s.Length - 1)));
                }
                if (s.EndsWith("rad"))
                {
                    return new CatAngle(double.Parse(s.Substring(0, s.Length - 3)));
                }
                if (s.EndsWith("d"))
                {
                    return new CatAngle((Math.PI/180)*double.Parse(s.Substring(0, s.Length - 1)));
                }
                if (s.EndsWith("deg"))
                {
                    return new CatAngle((Math.PI/180)*double.Parse(s.Substring(0, s.Length - 3)));
                }
            }

            return new CatAngle(o);
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