using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cat.AbstractStructure;
using Cat.Primitives;
using Cat.Primitives.Precise;

namespace Cat.Handlers.Parsers
{
    public static class ObjectExpressionParser
    {
        public static void ParseAndExecute(string expression, Dictionary<string, CatStructureObject> variables)
        {
            var expr = expression.Replace(" ", "");
            if (expr.IndexOf("=", StringComparison.Ordinal) != -1)
            {
                var equalS = expr.IndexOf("=", StringComparison.Ordinal);
                string variable = expr.Substring(0, equalS);
                string result = expr.Substring(equalS + 1);
                if (variable.StartsWith("{") && variable.EndsWith("}"))
                {
                    var vars = variable.Substring(1, variable.Length - 2).Split(',');
                    var res = ParseLexem(result, variables);
                    if (res is CatArray arr)
                    {
                        for (int i = 0; i < vars.Length && i < arr.Length; i++)
                        {
                            SetVariable(vars[i], arr[i], variables);
                        }
                    }
                }
                else
                {
                    SetVariable(variable, ParseLexem(result, variables), variables);
                }
            }
            else if (expr.IndexOf("|>>", StringComparison.Ordinal) != -1)
            {
                var equalS = expr.IndexOf("|>>", StringComparison.Ordinal);
                string variable = expr.Substring(0, equalS);
                string result = expr.Substring(equalS + 3);
                if (result == "out")
                {
                    if (variable.StartsWith("{") && variable.EndsWith("}"))
                    {
                        var vars = variable.Substring(1, variable.Length - 2).Split(',');
                        for (int i = 0; i < vars.Length && i < vars.Length; i++)
                        {
                            Console.WriteLine(variables[vars[i]]);
                        }
                    }
                    else
                    {
                        Console.WriteLine(variables[variable]);
                    }
                }
            }
            else if (expr.IndexOf(">>", StringComparison.Ordinal) != -1)
            {
                var equalS = expr.IndexOf(">>", StringComparison.Ordinal);
                string variable = expr.Substring(0, equalS);
                string result = expr.Substring(equalS + 2);
                if (variable == "in")
                {
                    var input = Console.ReadLine();
                    if (result.StartsWith("{") && result.EndsWith("}"))
                    {
                        var vars = result.Substring(1, result.Length - 2).Split(',');
                        var res = ParseLexem("{" + input + "}", variables);
                        if (res is CatArray arr)
                        {
                            for (int i = 0; i < vars.Length && i < arr.Length; i++)
                            {
                                SetVariable(vars[i], arr[i], variables);
                            }
                        }
                    }
                    else
                    {
                        SetVariable(result, ParseLexem(input, variables), variables);
                    }
                }

                if (result == "out")
                {
                    var delim = "";
                    if (variable.IndexOf("|", StringComparison.Ordinal) != -1)
                    {
                        var pipe = variable.IndexOf("|", StringComparison.Ordinal);
                        delim = variable.Substring(pipe + 1);
                        delim = delim.Replace("\\s", " ");
                            
                        variable = variable.Substring(0, pipe);
                    }
                    if (variable.StartsWith("{") && variable.EndsWith("}"))
                    {
                        var vars = variable.Substring(1, variable.Length - 2).Split(',');
                        var output = "";
                        for (int i = 0; i < vars.Length && i < vars.Length; i++)
                        {
                            output += variables[vars[i]]+delim;
                        }

                        if (delim.Length != 0)
                            output = output.Substring(0, output.Length - delim.Length);
                        Console.WriteLine(output);
                    }
                    else
                    {
                        Console.WriteLine(variables[variable]);
                    }
                }
            }
        }

        public static void SetVariable(string name, CatStructureObject value,
            Dictionary<string, CatStructureObject> variables)
        {
            if (name != "_")
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
            if (baseLex.StartsWith("{"))
            {
                if (baseLex.EndsWith("}"))
                {
                    baseLex = baseLex.Substring(1, baseLex.Length - 2);
                }
                else
                {
                    throw new ArgumentException();
                }
            }

            var lexems = SplitCarefully(baseLex,",");
            var objects = new CatStructureObject[lexems.Length];
            for (var i = 0; i < lexems.Length; i++)
            {
                var lexem = lexems[i];
                objects[i] = ParseLexem(lexem,variables);
            }
            return new CatArray(objects);
        }

        public static CatStructureObject ParseLexem(string lexem, Dictionary<string, CatStructureObject> variables)
        {
            var reg = new Regex("^[A-Za-z_]");
            if (reg.IsMatch(lexem)) //if variable
            {
                return variables[lexem];
            }
            if (lexem.StartsWith("#"))
            {
                var obj = variables[lexem];
                for (var j = 0; j < CatCore.Heap.Count; j++)
                {
                    var structureObject = CatCore.Heap[j];
                    if (obj == structureObject)
                    {
                        return new CatInt(j);
                    }
                }
                throw new NullReferenceException();
            }

            if (lexem.StartsWith("{"))
            {
                if (lexem.EndsWith("}"))
                {
                    return ParseArray(lexem, variables);
                }

                throw new ArgumentException();
            }
            
            if(lexem.ToLower() == "true")
                return new CatBool(true);
            if(lexem.ToLower() == "false")
                return new CatBool(false);

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
            if(lexem.EndsWith("rad") || lexem.EndsWith("grad") || lexem.EndsWith("deg"))
                return new CatAngle(lexem);
            
            if (lexem.IndexOf(".", StringComparison.Ordinal) != -1)
            {
                return new CatDouble(lexem);
            }

            return new CatInt(lexem);
        }

        public static string[] SplitCarefully(string text, string delimiter)
        {
            var lexems = new List<string>();
            var dtext = text;
            var index = dtext.IndexOf(delimiter, StringComparison.Ordinal);
            while (index > -1)
            {
                if (dtext[index + 1] != '{')
                {
                    lexems.Add(dtext.Substring(0,index));
                    dtext = dtext.Substring(index + 1);
                }
                else
                {
                    var nest = 0;
                    index += 2;
                    while(nest != 0 && index < dtext.Length)
                    {
                        if (dtext[index] == '{')
                        {
                            nest++;
                        }
                        if (dtext[index] == '}')
                        {
                            nest--;
                        }

                        index++;
                    }

                    if (index >= dtext.Length)
                    {
                        throw new ArgumentException();
                    }
                    lexems.Add(dtext.Substring(0,index));
                    dtext = dtext.Substring(index);
                }
                index = dtext.IndexOf(delimiter, StringComparison.Ordinal);
            }
            
            lexems.Add(dtext);

            return lexems.ToArray();
        }
    }
}