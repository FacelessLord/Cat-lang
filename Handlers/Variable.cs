using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cat.AbstractStructure;
using Cat.Primitives;

namespace Cat.Handlers
{
    public class VariableParser : IParser
    {
        public Regex Regex { get; set; } = new Regex(@"^\s*([a-zA-Z_]\w*)");
        public CatStructureObject Process(string expr, List<string> code)
        {
            return null;
        }
    }
}