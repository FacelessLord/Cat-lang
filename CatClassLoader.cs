using System;
using System.Collections.Generic;
using System.IO;
using static Cat.CatCore;

namespace Cat
{
    public static class CatClassLoader
    {
        /// <summary>
        /// Loads class internal fields to use them in program to create objects
        /// </summary>
        /// <param name="className"> Name of class to load</param>
        /// <returns></returns>
        public static int LoadClassFile(string className)
        {
            var lines = File.ReadAllLines(className);

            var search = false;
            var ClassName = "";
            (int i, int j) index = (0, 0);
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var words = line.Split();
                for (var j = 0; j < words.Length; j++)
                {
                    if (words[j] == "#")
                    {
                        j = words.Length;
                        continue;
                    }

                    //Console.WriteLine(words[j]);
                    if (!search)
                    {
                        if (words[j] == "class")
                        {
                            //Console.WriteLine("class found");
                            search = true;
                        }
                    }
                    else
                    {
                        ClassName = words[j];
                        index = (i, j);
                        goto search;
                    }
                }
            }

            search:
//            Console.WriteLine("Class: "+ClassName);
            if (ClassName != "")
            {
                var bracesNesting = 0;
                var nonStaticFields =
                    new List<(int modifiers, string type, string name, object value)
                    >(); // field : fieldName ~ fieldType : ;
                var staticFields =
                    new List<(int modifiers, string type, string name, object value)
                    >(); // static field : fieldName ~ fieldType : = value;
                var nonStaticMethods =
                    new List<(int modifiers, string signature, string link)
                    >(); // method : methodName(int a, string b) ~ returnType :
                var staticMethods =
                    new List<(int modifiers, string signature, string link)
                    >(); // static method : methodName(int a, string b) ~ returnType :

                int modifiers = 0;
                for (var i = index.i; i < lines.Length; i++)
                {
                    var line = lines[i];
                    var firstTime = true;
                    var words = line.Split();
                    for (var j = index.j; j < words.Length; j++)
                    {
                        if (words[j] == "#")
                            continue;
                        if (words[j] == ";")
                        {
                            modifiers = 0;
                        }

                        if (words[j - 1] == ":") //Static method : methodName ( int a , string b ) ~ returnType
                        {
                            if ((modifiers >> 1) % 2 == 1) //Method
                            {

                                int k = j;
                                string signature = "";
                                while (k < words.Length && words[k] != ":")
                                {
                                    signature += words[k] + " ";
                                    k++;
                                }

                                signature = signature.Trim();
                                string link = className + ":" + i; //i-th line of file:className.cls
                                if ((modifiers >> 2) % 2 == 1) //Static
                                {
                                    staticMethods.Add((modifiers, signature, link));
                                }
                                else
                                {
                                    nonStaticMethods.Add((modifiers, signature, link));
                                }

                                j = k;
                                continue;
                            }
                            else
                            if ((modifiers % 2) == 1) // field
                            {

                                var name = words[j];
                                var type = words[j + 2];

                                if ((modifiers >> 2) % 2 == 1) //Static
                                {
                                    //words[i+3] == ":"
                                    //words[i+4] == "="
                                    //words[i+5] == "2"//value
                                    var k = j + 4;
                                    var expr = "";
                                    while (k < words.Length && words[k] != ";")
                                    {
                                        expr += words[k] + " ";
                                        k++;
                                    }

                                    if (expr == "")
                                        expr = V0;

                                    staticFields.Add((modifiers, type, name, expr.Trim()));
                                    j += 4;
                                    continue;
                                }
                                else
                                {
                                    var k = j + 4;
                                    var expr = "";
                                    try
                                    {
                                        while (k < words.Length && words[k] != ";")
                                        {
                                            expr += words[k] + " ";
                                            k++;
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("Exception when getting non-static field");
                                    }

                                    if (expr == "")
                                        expr = V0;

                                    nonStaticFields.Add((modifiers, type, name, expr));
                                    j += 4;
                                    continue;
                                }

                            }
                        }

                        if (words[j] == "static")
                        {
                            modifiers += 4;
                        }

                        if (words[j] == "constructor")
                        {
                            modifiers += 6; //static method
                        }

                        if (words[j] == "field")
                        {
                            modifiers += 1;
                        }

                        if (words[j] == "method")
                        {
                            modifiers += 2;
                        }

                        if (words[j] == "{")
                        {
                            bracesNesting++;
                            firstTime = false;
                            continue;
                        }

                        if (words[j] == "}")
                        {
                            bracesNesting--;
                            continue;
                        }

                        if (bracesNesting <= 0 && !firstTime)
                        {
                            goto after;
                        }
                    }
                }

                after: //All fields and methods are read

                var ret = Heap.Count;
                var classHeap = new List<object>
                {
                    "|C|" + ClassName,
                    /*"nfc:" + */ nonStaticFields.Count,
                    /*"sfc:" + */ staticFields.Count,
                    /*"nmc:" + */ nonStaticMethods.Count,
                    /*"smc:" + */ staticMethods.Count
                }; //x5
                foreach (var f in nonStaticFields) //x3
                {
                    classHeap.Add(f.name);
                    classHeap.Add(f.type);
                    classHeap.Add(f.value);
                }

                foreach (var f in staticFields) //x3
                {
                    classHeap.Add(f.name);
                    classHeap.Add(f.type);
                    classHeap.Add(f.value);
                }

                foreach (var f in nonStaticMethods) //x2
                {
                    classHeap.Add(f.signature);
                    classHeap.Add(f.link);
                }

                foreach (var f in staticMethods) //x2
                {
                    classHeap.Add(f.signature);
                    classHeap.Add(f.link);
                }

                //var size = 5 + nonStaticFields.Count * 2 + staticFields.Count * 3 + nonStaticMethods.Count + staticMethods.Count * 2;

                HeapHandler.LoadListToHeap(classHeap);
                
                Types.Add(ClassName,ret);
                return ret;
            }

            return L0;
        }

        /// <summary>
        /// Method that will remove all data about provided class
        /// </summary>
        /// <param name="className"> Class to unload</param>
        public static void UnloadClass(string className)
        {
            var k = 0;
            while (k < Heap.Count - 1 && (!(Heap[k] is string sk) || sk != "|C|" + className)) k++;
            if (Heap[k] is string kcn && kcn == "|C|" + className)
            {
                var nfc = (int) Heap[k + 1];
                var sfc = (int) Heap[k + 2];
                var nmc = (int) Heap[k + 3];
                var smc = (int) Heap[k + 4];
                var size = 5 + nfc * 3 + sfc * 3 + 2 * nmc + smc * 2;
                for (var i = k; i < k + size; i++)
                    Heap[i] = H0;
            }

            DoesHeapContainSpaces = true;
            RemoveCount++;
            Types.Remove(className);
//	        CatCore.Heap.RemoveRange(k,size);
        }
    }
}