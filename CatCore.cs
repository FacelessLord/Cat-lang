using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using static Cat.HeapHandler;
using static Cat.Structure.Modifier;
using Cat.AbstractStructure;
using Cat.Handlers.Parsers;
using Cat.Primitives;
using Cat.Primitives.Precise;
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
        public static readonly List<CatStructureObject> Heap = new List<CatStructureObject>();

        /// <summary>
        /// Whether there was removing in Heap
        /// </summary>
        public static bool DoesHeapContainSpaces = false;
        
        /// <summary>
        /// Count of remove operations on Heap
        /// </summary>
        public static int RemoveCount = 0;

        /// <summary>
        /// Link-Zero
        /// </summary>
        public const int L0 = -1000;

        /// <summary>
        /// Value-Zero
        /// </summary>
        public const string V0 = "V\0";

        public static Dictionary<string, CatStructureObject> Variables = new Dictionary<string, CatStructureObject>();
        
        private static void Main(string[] args)
        {
            Variables.Add("_Pi_",CatPrecise.Pi);
            Variables.Add("_Tau_",CatPrecise.Tau);
            Variables.Add("_E_",CatPrecise.E);
            Variables.Add("Pi",CatPrecise.Pi.WithDigits(10));
            Variables.Add("Tau",CatPrecise.Tau.WithDigits(10));
            Variables.Add("E",CatPrecise.E.WithDigits(10));
            
            ObjectExpressionParser.ParseAndExecute("{a,b,c}={Pi,20r,\"asf\"}",Variables);
            ObjectExpressionParser.ParseAndExecute("d={a,b,c}",Variables);
            ObjectExpressionParser.ParseAndExecute("{_,_,_,_}={a,b,c,d}",Variables);
            
            ObjectExpressionParser.ParseAndExecute("{a,b,c,d}|,\\s >> out",Variables);
        }

        //Fieldname = 2 :: field is equal to 2
        //Fieldname = &2 :: field is equal to an object on index 2 :: 2 is a link
        //add an option to try to find exact copies


        /// <summary>
        /// Mapping from type name to index of type in heap
        /// </summary>
        public static readonly Dictionary<string,int> Types = new Dictionary<string, int>();
        /// <summary>
        /// Mapping from type name to index of type in heap
        /// </summary>
        public static readonly Dictionary<string,CatClass> Classes = new Dictionary<string, CatClass>();
        
        /// <summary>
        /// Fully loaded class files
        /// </summary>
        public static readonly Dictionary<string,string[]> ClassFiles = new Dictionary<string,string[]>();
//        
//        /// <summary>
//        /// Fully loaded class files
//        /// </summary>
//        public static readonly Dictionary<string,CatClass> Classes = new Dictionary<string,CatClass>();
        
        public static CatClass GetClassForName(string name)
        {
            return Classes[name];
        }
    }
}