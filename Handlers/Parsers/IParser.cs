using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cat.AbstractStructure;

namespace Cat.Handlers.Parsers
{
    public interface IParser
    {
        Regex Regex
        {
            get;
            set;
        }

        CatStructureObject Process(string expr, List<string> code);
    }
}