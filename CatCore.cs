using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Cat.HeapHandler;
using static Cat.Structure.Modifier;
using CalculatorConsole;
using Cat.AbstractStructure;
using Cat.Structure;

namespace Cat
{
    internal static class CatCore
    {
        /// <summary>
        /// Stack that contain all data of "system" calls
        /// </summary>
        public static Stack CallStack = new Stack();

        /// <summary>
        /// An ArrayList that contain all program data, such as links, values, arrays, etc...
        /// </summary>
        public static readonly ArrayList Heap = new ArrayList();

        /// <summary>
        /// Whether there was removing in Heap
        /// </summary>
        public static bool DoesHeapContainSpaces = false;
        
        /// <summary>
        /// Count of remove operations on Heap
        /// </summary>
        public static int RemoveCount = 0;

        /// <summary>
        /// Null-character used to occupy empty space in Heap (Heap-Zero)
        /// </summary>
        public const string H0 = "\0";

        /// <summary>
        /// Link-Zero
        /// </summary>
        public const int L0 = -1;

        /// <summary>
        /// Value-Zero
        /// </summary>
        public const string V0 = "V\0";
        
        private static void Main(string[] args)
        {
            var f1 = new CatField("f1","int",2,Static,Final,Field);
            var f2 = new CatField("f2","angle",2,Final,Field);
            var m1 = new CatMethod("m1(int k, string l)","int","Test",4,Static,Method);
            var m2 = new CatMethod("m2(angle k, double l)","string","Test",8,Final,Method);
            var met = new CatCompoundObject("int",f1,f2,m1,m2);
            var met2 = new CatPrimitiveObject("float",2.0f);
            Heap.AddRange(met.ToMemoryBlock());
            Heap.AddRange(met2.ToMemoryBlock());
            Console.WriteLine(EMath.ArrayToString(Heap.ToArray()));
        }

        //Fieldname = 2 :: field is equal to 2
        //Fieldname& = 2 :: field is equal to an object on index 2 :: 2 is a link
        //add an option to try to find exact copies

        /// <summary>
        /// List that contains primitive type codewords
        /// </summary>
        ///
        /// <remarks>
        /// Primitive types have code equal to (-1-IndexOf(primitive))
        /// </remarks>
        public static List<string> Primitives { get; } = new List<string>()
            {"byte", "int", "long", "angle", "string", "bool", "float", "double", "precise"};

        /// <summary>
        /// Mapping from type name to index of type in heap
        /// </summary>
        public static readonly Dictionary<string,int> Types = new Dictionary<string, int>();
        
        /// <summary>
        /// Fully loaded class files
        /// </summary>
        public static readonly Dictionary<string,string[]> Classes = new Dictionary<string,string[]>();
        
        /// <summary>
        /// Method that finds index of given type in heap
        /// </summary>
        /// <param name="type"> name of the type</param>
        /// <returns> index of the type in heap</returns>
        public static int GetTypeIndex(string type)
        {
            if (Primitives.Contains(type))
            {
                return (-1-Primitives.IndexOf(type));
            }

            if (Types.ContainsKey(type))
            {
                return Types[type];
            }
            
            ExceptionHandler.ThrowException("NullPointerException","type \""+ type+"\" is not loaded.");
            return -10;
        }
    }
}