using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Cat.AbstractStructure;
using Cat.Primitives;
using Cat.Primitives.Precise;
using Cat.Structure;
using Cat.Utilities;

namespace Cat.Handlers.Parsers
{
    public static class RecursiveObjectExpressionParser
    {
        public static List<IParser> Handlers = new List<IParser>();

        public static void Load()
        {
            Handlers.Add(new NullParser());
            Handlers.Add(new SemicolonParser());
            Handlers.Add(new SetParser());
            Handlers.Add(new IndexCreationParser());
            Handlers.Add(new ForLoopParser());
            Handlers.Add(new InputParser());
            Handlers.Add(new OutputParser());
            Handlers.Add(new IncrementParser());
            Handlers.Add(new OperatorParser());
            Handlers.Add(new ClassPropertyParser());
            Handlers.Add(new TypeofParser());
        }

        public static int CurrentAdress = -1;

        public static Dictionary<string, int> Labels = new Dictionary<string, int>();

        public static void Run(string expression, Dictionary<string, CatStructureObject> variables)
        {
            Labels = new Dictionary<string, int>();
            var lexems = SplitCarefully(expression, ";");

//            Console.WriteLine(MiscUtils.ArrayToString(lexems,delimiter:" | "));
            for (CurrentAdress = 0; CurrentAdress < lexems.Length; CurrentAdress++)
            {
                var lexem = lexems[CurrentAdress];
                var skip = SuperKeyWordHandler.Process(lexem, variables);
                if (skip)
                {
                    continue;
                }


                ParseAndExecute(lexem, variables, expression + " call:" + CurrentAdress + " lexem" + lexem);
            }

            CurrentAdress = -1;
        }

        public static CatStructureObject ParseAndExecute(string expression,
            Dictionary<string, CatStructureObject> variables, string parentExpr)
        {
            expression = CarefullyReplaceSpaces(expression);
            if (expression.StartsWith("("))
            {
                if (expression.EndsWith(")"))
                {
                    expression = expression.Substring(1, expression.Length - 2);
                }
            }

            if (expression.StartsWith("{"))
            {
                if (expression.EndsWith("}"))
                {
                    expression = expression.Substring(1);
                    expression = expression.Substring(0, expression.Length - 1);
                    return ParseArray(expression, variables);
                }
            }

            //Console.WriteLine($"Expr {expression}");
//            foreach (var handler in Handlers)
//            {
//                if (handler.Matches(expression))
//                {
//                    var obj = handler.Process(expression, variables);
//                    if (obj is CatNull)
//                        Console.WriteLine("Found null when parsed " + expression);
//                    return obj;
//                }
//            }

            var lexObj = ParseLexem(expression, variables, parentExpr);
            if (lexObj is CatNull)
                Console.WriteLine("Found null:" + expression);
            return lexObj;
        }

        public static void SetVariable(string name, CatStructureObject value,
            Dictionary<string, CatStructureObject> variables)
        {
            if (name != "_" && name != "null")
            {
                if (variables.ContainsKey(name))
                {
                    variables.Remove(name);
                }
                variables.Add(name, value);
            }
        }

        public static CatArray ParseArray(string baseLex, Dictionary<string, CatStructureObject> variables)
        {
            var lexems = SplitCarefully(baseLex.Substring(1,baseLex.Length-2), ",");
            var objects = new CatStructureObject[lexems.Length];
            for (var i = 0; i < lexems.Length; i++)
            {
                var lexem = lexems[i];
                objects[i] = ParseAndExecute(lexem, variables, baseLex);
            }

            return new CatArray(objects);
        }

        private static CatStructureObject ParseRange(string baseLex, Dictionary<string, CatStructureObject> variables)
        {
            var lexems = SplitCarefully(baseLex.Substring(1, baseLex.Length - 2), ",");
            var objects = new CatNumber[lexems.Length];
            if (lexems.Length != 2)
            {
                throw new ArgumentException($"Wrong arguments for range [{baseLex}]");
            }

            for (var i = 0; i < 2; i++)
            {
                var lexem = lexems[i];
                try
                {
                    objects[i] = (CatNumber) ParseAndExecute(lexem, variables, baseLex);
                }
                catch (Exception e)
                {
                    throw new ArgumentException($"Wrong arguments for range [{baseLex}]");
                }
            }

            return new CatRange(objects[0], objects[1]);
        }

        public static CatStructureObject ParseLexem(string lexem, Dictionary<string, CatStructureObject> variables,
            string parentExpr)
        {
            if (lexem.StartsWith("{"))
            {
                if (lexem.EndsWith("}"))
                {
                    return ParseArray(lexem, variables);
                }

                throw new ArgumentException();
            }
            
            if (lexem.StartsWith("["))
            {
                if (lexem.EndsWith("]"))
                {
                    return ParseRange(lexem, variables);
                }

                throw new ArgumentException();
            }

            if (lexem == "")
            {
                Console.WriteLine("You missed a symbol. Counting as null. Please check your code: " + parentExpr);
                return CatNull.Null;
            }
            if (lexem == "null" || lexem == "_")
            {
                return CatNull.Null;
            }
            
            if (lexem.ToLower() == "true")
                return new CatBool(true);
            if (lexem.ToLower() == "false")
                return new CatBool(false);

            //Console.WriteLine(lexem);
            var reg = new Regex("^[A-Za-z_]");
            if (reg.IsMatch(lexem) && lexem != "null" && lexem != "_") //if variable
            {
                return variables[lexem];
            }


            if (lexem.StartsWith("&"))
            {
                var obj = ParseAndExecute(lexem.Substring(1), variables, lexem);
                for (var j = 0; j < CatCore.Heap.Count; j++)
                {
                    var structureObject = CatCore.Heap[j];
                    if (obj == structureObject)
                    {
                        return new CatLink(j);
                    }
                }

                throw new NullReferenceException(
                    $"Object {obj} isn't contained in Heap. May be you broken you data security");
            }

            if (lexem.StartsWith("!"))
            {
                var obj = ParseAndExecute(lexem.Substring(1), variables, lexem);

                return obj.ToInt();
            }

            if (lexem.StartsWith("#"))
            {
                var obj = ParseAndExecute(lexem.Substring(1), variables, lexem);

                if (obj is CatIndex index)
                {
                    return index.Evaluate();
                }
                return obj.ToDouble();
            }

            if (lexem.StartsWith("\""))
            {
                if (lexem.EndsWith("\""))
                {
                    return new CatString(lexem.Substring(1, lexem.Length - 2));
                }

                throw new ArgumentException();
            }

            switch (lexem.ToLower()[lexem.Length - 1])
            {
                case 'p': return new CatPrecise(lexem);
                case 'b':
                    return new CatByte(lexem);
                case 'r':
                case 'g':
                    return new CatAngle(lexem);
                case 'l':
                    return new CatLong(lexem);
                case 'f':
                    return new CatFloat(lexem);
                case 'd':
                    return new CatDouble(lexem);
            }

            if (lexem.EndsWith("rad") || lexem.EndsWith("grad") || lexem.EndsWith("deg"))
                return new CatAngle(lexem);

            if (lexem.IndexOf(".", StringComparison.Ordinal) != -1)
            {
                return new CatDouble(lexem);
            }

            return new CatInt(lexem);
        }

        public static int Calls = 0;

        public static string[] SplitCarefully(string text, string delimiter)
        {
            List<string> lexems = new List<string>();
            var dtext = text;
            if (text == "")
                return new string[0];
            for (var i = CarefullIndexOf(dtext, delimiter);
                i != -1 && dtext.Length > 0;
                i = CarefullIndexOf(dtext, delimiter))
            {
                var lex = dtext.Substring(0, i);
                if (lex != "")
                    lexems.Add(lex);
                dtext = dtext.Substring(i + delimiter.Length);
            }

            if (dtext != "")
                lexems.Add(dtext);
            return lexems.ToArray();
        }

        public static string FormatString(string output)
        {
            output = output.Replace("\\s", " ");
            output = output.Replace("\\n", "\n");
            return output;
        }

        public static string CarefullyReplaceSpaces(string s)
        {
            var index = 0;
            while (index < s.Length)
            {
                if (s[index] == ' ')
                {
                    s = s.Remove(index, 1);
                    index--;
                }

                if (s[index] == '"')
                {
                    index++;
                    while (index < s.Length && s[index] != '"')
                    {
                        index++;
                    }
                }

                index++;
            }

            return s;
        }

        public static int CarefullIndexOf(string s, string search)
        {
//            Console.WriteLine($"Search {search} in { s }");
            var index = 0;
            while (s.Length > 0)
            {
                if (s.Length == 0)
                    return -1;
                if (s.StartsWith(search))
                {
                    //Console.WriteLine("returned "+s);
                    return index;
                }

                if (s[0] == '"' && s.Length > 1)
                {
                    index++;
                    while (index < s.Length && s[1] != '"')
                    {
                        index++;
                        s = s.Substring(1);
                    }

                    s = s.Substring(1);
                }
                if (s[0] == '(' && s.Length > 1)
                {
                    var nest = 1;
                    index++;
                    while (index < s.Length && nest != 0)
                    {
                        if (s[1] == '(')
                            nest++;
                        if (s[1] == ')')
                            nest--;
                        index++;
                        s = s.Substring(1);
                    }

                    if (index < s.Length)
                    {
                        if (s.StartsWith(search))
                        {
                            return index;
                        }
                    }

                    s = s.Substring(1);
                }

                if (s[0] == '{' && s.Length > 1)
                {
                    var nest = 1;
                    index++;
                    while (index < s.Length && nest != 0)
                    {
                        if (s[1] == '{')
                            nest++;
                        if (s[1] == '}')
                            nest--;
                        index++;
                        s = s.Substring(1);
                    }

                    if (index < s.Length)
                    {
                        if (s.StartsWith(search))
                        {
                            return index;
                        }
                    }

                    s = s.Substring(1);
                }

                index++;
                s = s.Substring(1);
//                Console.WriteLine($"itres {s}");
            }

            return -1;
        }
    }
}