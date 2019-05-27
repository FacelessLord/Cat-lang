using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cat.AbstractStructure;
using Cat.Structure;

namespace Cat.Handlers.Parsers
{
    public class LinearExpressionHandler
    {
        public static List<IParser> Parsers = new List<IParser>();


        public static void Load()
        {
            Parsers.Add(new InputParser());
            Parsers.Add(new TypeofParser());
            Parsers.Add(new ForLoopParser());
            
            Parsers.Add(new NullParser());
            Parsers.Add(new VariableParser());
            Parsers.Add(new AngleParser());
            Parsers.Add(new NumberParser());
            Parsers.Add(new StringParser());
            Parsers.Add(new RangeParser());
            
            Parsers.Add(new OutputParser());
            
            Parsers.Add(new ClassPropertyParser());
            Parsers.Add(new IncrementParser());
            Parsers.Add(new IndexCreationParser());
            Parsers.Add(new OperatorParser());
            Parsers.Add(new SemicolonParser());
            Parsers.Add(new SetParser());
        }

        public static Scope Scope = new Scope();
        
        public static List<CatStructureObject> ParseLexems(string expr)
        {
            var lexems = new List<string>();
            var code = new List<CatStructureObject>();

            while (expr.Length > 0)
            {
                for (int i = 0; i < Parsers.Count; i++)
                {
                    var parser = Parsers[i];
                    if (parser.Regex.IsMatch(expr))
                    {
                        var match = parser.Regex.Match(expr);
                        string lexem = match.Groups[1].Value;
                        lexems.Add(lexem);
                        expr = expr.Substring(match.Groups[0].Length);
                        break;
                    }
                }
            }

            for (int i = 0; i < lexems.Count; i++)
            {
                for (int j = 0; j < Parsers.Count; j++)
                {
                    var parser = Parsers[j];
                    if (parser.Regex.IsMatch(lexems[i]))
                    {
                        code.Add(parser.Process(lexems[i], lexems));
                        break;
                    }
                }
            }

            return code;
        }

        public static int CurrentAdress = -1;

        public static Dictionary<string, int> Labels = new Dictionary<string, int>();

        public static void Run(string expression)
        {
        }


        public static CatStructureObject ParseAndExecute(string expression)
        {
            return TypeHandler.Null;
        }
    }
}