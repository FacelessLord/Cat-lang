using System;
using System.Collections.Generic;
using System.Linq;

namespace Cat.Calculators
{
	public class DoubleMath : EMath<double>
	{
		public DoubleMath() : base(0)
		{

		}

		public static readonly EMath<double> EMath = new DoubleMath();

		public override Function<double> ForName(string name, params Expression<double>[] args)
		{
			switch (name)
			{
				case "+": return new DoubleSum(args);
				case "-": return new DoubleSub(args);
				case "*": return new DoubleProd(args);
				case "/": return new DoubleDivide(args);
				case "^": return new DoublePower(args);
				default:
					return new NulFunc<double>(this);
			}
		}

		private readonly Dictionary<string, int> _priorities = new Dictionary<string, int>();
		private readonly Dictionary<string, int> _operands = new Dictionary<string, int>();

		public override int GetPriority(string operation)
		{
			return _priorities[operation];
		}

		public override int GetOperandCount(string operation)
		{
			return _operands[operation];
		}

		public override bool HaveOperation(string operation)
		{
			return _priorities.ContainsKey(operation);
		}

		public void PrepareForMaths()
		{
			_priorities["("] = 0;
			_operands["("] = 0;
			_priorities[")"] = 1;
			_operands[")"] = 0;
			_priorities["+"] = 2;
			_operands["+"] = 2;
			_priorities["-"] = 2;
			_operands["-"] = 2;
			_priorities["*"] = 3;
			_operands["*"] = 2;
			_priorities["/"] = 3;
			_operands["/"] = 2;
			_priorities["^"] = 4;
			_operands["^"] = 2;
		}

		public Expression<double> Sum(params object[] args)
		{
			return ForName("+", GetExpressions(args));
		}

		public Expression<double> Sub(params object[] args)
		{
			return ForName("-", GetExpressions(args));
		}

		public Expression<double> Prod(params object[] args)
		{
			return ForName("*", GetExpressions(args));
		}

		public Expression<double> Div(params object[] args)
		{
			return ForName("/", GetExpressions(args));
		}

		public Expression<double> Pow(params object[] args)
		{
			return ForName("^", GetExpressions(args));
		}
	}

	public class DoubleSum : Function<double>
	{
		public DoubleSum(params Expression<double>[] args) : base(DoubleMath.EMath, args)
		{
		}

		public override string ToString()
		{
			var ret = this.Arguments[0].ToString();
			if (!(Arguments[0] is Number<double>) && !(Arguments[0] is Var<double>))
			{
				ret = "(" + ret + ")";
			}

			for (var i = 1; i < this.Arguments.Length; i++)
			{
				var tmp = Arguments[i].ToString();
				ret += " + " + Arguments[i];
			}

			return ret;
		}

		public override string AsExpression(params (string name, double value)[] args)
		{
			var ret = this.Arguments[0].AsExpression(args);
			for (var i = 1; i < this.Arguments.Length; i++)
			{
				ret += " + " + this.Arguments[i].AsExpression(args);
			}

			return ret;
		}

		protected override double Call(params double[] arguments)
		{
			return arguments.Sum();
		}
	}

	public class DoublePower : Function<double>
	{
		public DoublePower(params Expression<double>[] args) : base(DoubleMath.EMath, args)
		{
		}

		public override string ToString()
		{
			var left = "";
			var right = "";

			if (Arguments[0] is Number<double> || Arguments[0] is Var<double>)
			{
				right = Arguments[0].ToString();
			}
			else
			{
				right = "(" + Arguments[0].ToString() + ")";
			}

			if (Arguments[1] is Number<double> || Arguments[1] is Var<double>)
			{
				left = Arguments[1].ToString();
			}
			else
			{
				left = "(" + Arguments[1].ToString() + ")";
			}

			return left + " ^ " + right;
		}

		public override string AsExpression(params (string name, double value)[] args)
		{
			var left = "";
			var right = "";

			if (Arguments[0] is Number<double> || Arguments[0] is Var<double>)
			{
				right = Arguments[0].AsExpression(args);
			}
			else
			{
				right = "(" + Arguments[0].AsExpression(args) + ")";
			}

			if (Arguments[1] is Number<double> || Arguments[1] is Var<double>)
			{
				left = Arguments[1].AsExpression(args);
			}
			else
			{
				left = "(" + Arguments[1].AsExpression(args) + ") ";
			}

			return left + " ^ " + right;
		}

		protected override double Call(params double[] arguments)
		{
			return Math.Pow(arguments[0], arguments[1]);
		}
	}

	public class DoubleSub : Function<double>
	{
		public DoubleSub(params Expression<double>[] args) : base(DoubleMath.EMath, args)
		{
		}

		public override string ToString()
		{
			var ret = this.Arguments[0].ToString();
			for (var i = 1; i < this.Arguments.Length; i++)
			{
				ret += " - " + this.Arguments[i].ToString();
			}

			return ret;
		}

		public override string AsExpression(params (string name, double value)[] args)
		{
			var ret = this.Arguments[0].AsExpression(args);
			for (var i = 1; i < this.Arguments.Length; i++)
			{
				ret += " - " + this.Arguments[i].AsExpression(args);
			}

			return ret;
		}

		protected override double Call(params double[] arguments)
		{
			var sum = arguments[0];
			for (var i = 1; i < arguments.Length; i++) sum -= arguments[i];

			return sum;
		}
	}

	public class DoubleProd : Function<double>
	{
		public DoubleProd(params Expression<double>[] args) : base(DoubleMath.EMath, args)
		{
		}

		public override string ToString()
		{
			var ret = this.Arguments[0].ToString();
			for (var i = 1; i < this.Arguments.Length; i++)
			{
				ret += " * " + this.Arguments[i].ToString();
			}

			return ret;
		}

		public override string AsExpression(params (string name, double value)[] args)
		{
			var ret = this.Arguments[0].AsExpression(args);
			for (var i = 1; i < this.Arguments.Length; i++)
			{
				ret += " * " + this.Arguments[i].AsExpression(args);
			}

			return ret;
		}

		protected override double Call(params double[] arguments)
		{
			return arguments.Aggregate<double, double>(1, (current, t) => current * t);
		}
	}

	public class DoubleDivide : Function<double>
	{
		public DoubleDivide(params Expression<double>[] args) : base(DoubleMath.EMath, args)
		{
		}

		public override string ToString()
		{
			var ret = this.Arguments[0].ToString();
			for (var i = 1; i < this.Arguments.Length; i++)
			{
				ret += " / " + this.Arguments[i].ToString();
			}

			return ret;
		}

		public override string AsExpression(params (string name, double value)[] args)
		{
			var ret = this.Arguments[0].AsExpression(args);
			for (var i = 1; i < this.Arguments.Length; i++)
			{
				ret += " / " + this.Arguments[i].AsExpression(args);
			}

			return ret;
		}

		protected override double Call(params double[] arguments)
		{
			var div = arguments[0];
			for (var i = 1; i < arguments.Length; i++) div /= arguments[i];

			return div;
		}
	}

	public class DoubleMod : Function<double>
	{
		public DoubleMod(params Expression<double>[] args) : base(DoubleMath.EMath, args)
		{
		}

		public override string ToString()
		{
			var ret = this.Arguments[0].ToString();
			for (var i = 1; i < this.Arguments.Length; i++)
			{
				ret += " % " + this.Arguments[i].ToString();
			}

			return ret;
		}

		public override string AsExpression(params (string name, double value)[] args)
		{
			var ret = this.Arguments[0].AsExpression(args);
			for (var i = 1; i < this.Arguments.Length; i++)
			{
				ret += " % " + this.Arguments[i].AsExpression(args);
			}

			return ret;
		}

		protected override double Call(params double[] arguments)
		{
			var div = arguments[0];
			for (var i = 1; i < arguments.Length; i++) div %= arguments[i];

			return div;
		}
	}

	public class DoubleDiv : Function<double>
	{
		public DoubleDiv(params Expression<double>[] args) : base(DoubleMath.EMath, args)
		{
		}

		public override string ToString()
		{
			var ret = this.Arguments[0].ToString();
			for (var i = 1; i < this.Arguments.Length; i++)
			{
				ret += " // " + this.Arguments[i].ToString();
			}

			return ret;
		}

		public override string AsExpression(params (string name, double value)[] args)
		{
			var ret = this.Arguments[0].AsExpression(args);
			for (var i = 1; i < this.Arguments.Length; i++)
			{
				ret += " // " + this.Arguments[i].AsExpression(args);
			}

			return ret;
		}

		protected override double Call(params double[] arguments)
		{
			var div = arguments[0];
			for (var i = 1; i < arguments.Length; i++) div /= (int) arguments[i];

			return div;
		}
	}
}