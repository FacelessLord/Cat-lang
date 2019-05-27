using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using static Cat.HeapHandler;
using static Cat.Structure.Modifier;
using Cat.AbstractStructure;
using Cat.Handlers;
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

        private static void Main(string[] args)
        {

            LinearExpressionHandler.Load();
            //doLoadBigConstants
//            Variables.Add("_Pi_",CatPrecise.Pi);
//            Variables.Add("_Tau_",CatPrecise.Tau);
//            Variables.Add("_E_",CatPrecise.E);
            LinearExpressionHandler.Scope.AddVariable("Pi", CatPrecise.Pi.WithDigits(10));
            LinearExpressionHandler.Scope.AddVariable("Tau", CatPrecise.Tau.WithDigits(10));
            LinearExpressionHandler.Scope.AddVariable("E", CatPrecise.E.WithDigits(10));
            //ObjectExpressionParser.ParseAndExecute("(a = \"asf\")",Variables);
//            ObjectExpressionParser.Run("a = {0,1,2,3}",
//                Variables);
            string file = "classes/main.cls";
            var lines = File.ReadLines(file);
            var line = "";
            foreach (var l in lines)
            {
                line += l;
            }

            LinearExpressionHandler.Run(line);


            //ObjectExpressionParser.ParseAndExecute("a >> .out",Variables);
        }

        //Fieldname = 2 :: field is equal to 2
        //Fieldname = &2 :: field is equal to an object on index 2 :: 2 is a link
        //add an option to try to find exact copies


        /// <summary>
        /// Mapping from type name to index of type in heap
        /// </summary>
        public static readonly Dictionary<string, int> Types = new Dictionary<string, int>();

        /// <summary>
        /// Mapping from type name to Class
        /// </summary>
        public static readonly Dictionary<string, CatClass> Classes = new Dictionary<string, CatClass>();

        /// <summary>
        /// Fully loaded class files
        /// </summary>
        public static readonly Dictionary<string, string[]> ClassFiles = new Dictionary<string, string[]>();
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