using System.Collections.Generic;
using System.Linq;

namespace Cat.Calculators
{
	public static class Lexer
	{
		private static readonly string[] Delimiters = {"(", ")", "^", "*", "/", "+", "-", "%", "|"};

		public static string[] SplitSaveDelimiters(string s)
		{
			var temp = new List<char>();
			var parts = new List<string>();
			foreach (var c in s)
			{
				if (Delimiters.Contains(c.ToString()))
				{
					if (temp.Any())
					{
						parts.Add(new string(temp.ToArray()));
						temp.Clear();
					}

					parts.Add(c.ToString());
				}
				else
				{
					temp.Add(c);
				}
			}

			var prepExpr = new List<string>();
			for (var i = 0; i < parts.Count; i++)
			{
				if (i + 2 < parts.Count)
				{
					if (parts[i] == "(" && parts[i + 2] == ")")
					{
						prepExpr.Add(parts[i + 1]);
						i += 2;
						continue;
					}
				}

				prepExpr.Add(parts[i]);
			}

			return prepExpr.ToArray();
		}
	}
}