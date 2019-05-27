using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cat.AbstractStructure;
using Cat.Handlers.Parsers;
using Cat.Primitives;
using Cat.Structure;

namespace Cat.Handlers
{
    public class TypeofParser : IParser
    {
        public Regex Regex { get; set; } = new Regex(@"^\s*(typeof)");
        public CatStructureObject Process(string expr, List<string> code)
        {
            return new CatFunction()
            {
                Func = (lexems, address) => new CatString(lexems[address + 1].Type)
            };
        }
    }
}