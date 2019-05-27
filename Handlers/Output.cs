using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cat.AbstractStructure;
using Cat.Handlers.Parsers;
using Cat.Primitives;
using Cat.Structure;

namespace Cat.Handlers
{
    public class OutputParser : IParser
    {
        public Regex Regex { get; set; } = new Regex(@"^\s*(\~\>\>\s*s{0,1}\s*\.{0,1}\s*out|\>\>\s*s{0,1}\s*out)");
        public CatStructureObject Process(string expr, List<string> code)
        {
            return new CatFunction()
            {
                Func = (lexems, address) =>
                {
                    var output = lexems[address - 1];
                    if (expr.StartsWith("~>>"))
                    {
                        
                    }
                    else
                    {
                        
                    }
                    
                    return new CatString(output.ToString());
                }
            };
        }
    }
}