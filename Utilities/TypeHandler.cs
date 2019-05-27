using System;
using System.Collections.Generic;
using Cat.Primitives;
using Cat.Structure;

namespace Cat.Utilities
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
                var obj = cco.TypeClass;
                while (null != obj.Parent && obj.Name != type)
                {
                    obj = obj.Parent;
                }

                if (obj.Name == type)
                {
                    return obj;
                }
            }

            throw new NullReferenceException(); 
        }
        
        public static CatAngle CastToAngle(object o)
        {
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
            {"void","byte", "int", "long", "angle", "string", "bool", "float", "double", "precise","index", "range", "object","null"};
        
        public static CatNull Null= new CatNull();
        
        public static bool IsPrimitive(string type)
        {
            return Primitives.Contains(type);
        }
        
        
        /// <summary>
        /// Method that finds index of given type in heap
        /// </summary>
        /// <param name="type"> name of the type</param>
        /// <returns> index of the type in heap</returns>
        public static int GetTypeIndex(string type)
        {
            if (IsPrimitive(type))
            {
                return (-1-Primitives.IndexOf(type));
            }

            if (CatCore.Types.ContainsKey(type))
            {
                return CatCore.Types[type];
            }
            
            ExceptionHandler.ThrowException("NullPointerException","type \""+ type+"\" is not loaded.");
            return -10;
        }
    }
}