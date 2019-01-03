using System;
using System.Collections.Generic;
using System.Linq;

namespace Cat.Calculators
{

	public abstract class Expression<T>
	{
		public EMath<T> _math;

		public Expression(EMath<T> math)
		{
			_math = math;
		}

		public abstract T Calculate(params (string name, T value)[] args);

		public abstract string AsExpression(params (string name, T value)[] args);
	}

	public abstract class EMath<T>
	{
		public EMath(T zero)
		{
			_zero = zero;
		}

		public T _zero;

		public Expression<T> Get(object arg)
		{
			switch (arg)
			{
				case Expression<T> e:
					return e;
				case T t:
					return new Number<T>(this, t);
				case string s:
					return new Var<T>(this,s);
				default:
					return new Number<T>(this, _zero);
			}
		}

		public Expression<T>[] GetExpressions(params object[] args)
		{
			var expressions = new Expression<T>[args.Length];
			for (var i = 0; i < expressions.Length; i++)
			{
				expressions[i] = Get(args[i]);
			}

			return expressions;
		}

		public Expression<T> ParseExpressionFully(string expstr)
		{
			var lexems = Lexer.SplitSaveDelimiters(("(" + expstr + ")").Replace(" ", ""));
			return ParseExpression(lexems);
		}

		public abstract bool HaveOperation(string operation);
		public abstract int GetPriority(string operation);
		public abstract int GetOperandCount(string operation);

		public abstract Function<T> ForName(string name, params Expression<T>[] args);

		private Expression<T> ParseExpression(IReadOnlyCollection<string> lexedInput)
		{
			if (lexedInput.Count == 0)
				return new Number<T>(this, _zero);

			var stack = new Stack<string>();
			var lexems = new List<string>(); // this will contain RPN

			foreach (var lexeme in lexedInput)
			{
				if (HaveOperation(lexeme))
				{
					if (lexeme == "(" || stack.Count == 0 || GetPriority(lexeme) > GetPriority(stack.Peek()))
					{
						stack.Push(lexeme);
					}
					else if (lexeme == ")")
					{
						while (stack.Peek() != "(" && stack.Count > 0)
						{
							lexems.Add(stack.Pop());
						}

						if (stack.Count > 0)
						{
							stack.Pop(); //poping "(" out
						}
					}
					else if (GetPriority(lexeme) <= GetPriority(stack.Peek()))
					{
						lexems.Add(stack.Pop());
						stack.Push(lexeme);
					}

				}
				else
				{
					lexems.Add(lexeme);
				}
			}

			while (stack.Count > 0)
			{
				lexems.Add(stack.Pop());
			}

			var prepExpr = lexems.Cast<object>().ToList();

			for (var i = 1; i < prepExpr.Count; i++)
			{
				if (prepExpr[i] is string func && HaveOperation(func))
				{
					int operandCount = GetOperandCount(func);
					var last = new object[operandCount];
					for (var k = 0; k < operandCount; k++)
						last[k] = prepExpr[i - k - 1];
					var got = new Expression<T>[operandCount];
					for (var k = 0; k < operandCount; k++)
						got[k] = Get(last[k]);
					prepExpr[i - operandCount] = ForName(func, got);
					for (var k = 0; k < operandCount; k++)
						prepExpr.RemoveAt(i - 1);
					i = Math.Max(i - operandCount, 1);
				}
			}

			return Get(prepExpr[0]);
		}
	}
}