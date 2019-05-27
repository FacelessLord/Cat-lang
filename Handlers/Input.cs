using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cat.AbstractStructure;
using Cat.Handlers.Parsers;
using Cat.Primitives;
using Cat.Structure;

namespace Cat.Handlers
{
    public class InputParser : IParser
    {
        public Regex Regex { get; set; } = new Regex(@"^\s*(in\s*>>)");

        public CatStructureObject Process(string expr, List<string> code)
        {
            return new CatFunction()
            {
                Func = (lexems, address) =>
                {
                    if (lexems[address+1] is CatVariable cvar)
                    {
                        return cvar.SetValue(new CatString(Console.ReadLine()));
                    }
                    else
                    {
                        throw new ArgumentException("Cannot write to " + lexems[address+1].Type);
                    }
                }
            };
        }
    }
}