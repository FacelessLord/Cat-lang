using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cat.AbstractStructure;
using Cat.Primitives;

namespace Cat.Handlers
{
    public class ScopeOnHandler : IParser
    {
        public Regex Regex { get; set; } = new Regex(@"^\s*(\{)");
        public CatStructureObject Process(string expr, List<string> code)
        {
            return null;
        }
    }
    
    public class ScopeOffHandler : IParser
    {
        public Regex Regex { get; set; } = new Regex(@"^\s*(\})");
        public CatStructureObject Process(string expr, List<string> code)
        {
            return null;
        }
    }
}