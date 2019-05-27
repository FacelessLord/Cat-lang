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
        /// <summary>
        /// 'n' is an new line modifier : inserts \n characters at the end : usage is equal to usage of >>
        /// 'd' is a dot modifier : inserts '.' at the end
        /// </summary>
        public Regex Regex { get; set; } = new Regex(@"^\s*(\~\>\>\s*[nd]{0,1}\s*out|\>\>\s*d{0,1}\s*out)");

        public CatStructureObject Process(string expr, List<string> code)
        {
            return new CatFunction()
            {
                Func = (lexems, address) =>
                {
                    var output = lexems[address - 1];

                    string delim = "";
                    string end = "";
                    if (expr.StartsWith("~>>"))
                    {
                        if (expr.Contains("n"))
                        {
                            end = "\n";
                        }

                        if (expr.Contains("d"))
                        {
                            end = ".";
                        }

                        Console.Write(output + end);
                    }
                    else
                    {
                        if (expr.Contains("d"))
                        {
                            end = ".";
                        }

                        Console.Write(output + end);
                    }

                    return new CatString(output.ToString());
                },
                Priority = 30
            };
        }
    }
}