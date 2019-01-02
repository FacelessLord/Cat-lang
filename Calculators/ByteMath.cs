using System;
using System.Collections.Generic;
using System.Linq;
using CalculatorConsole;
using static Cat.Calculators.ByteMath;

namespace Cat.Calculators
{
    public class ByteMath : EMath<Byte>
    {
        public ByteMath() : base(0)
        {
        }
        
        public static readonly EMath<byte> EMath = new ByteMath();

        public override bool HaveOperation(string operation)
        {
            switch (operation)
            {
                case "+":
                case "-":
                case "*":
                case "/":
                case "(":
                case ")":
                    return true;
            }

            return false;
        }

        public override int GetPriority(string operation)
        {
            switch (operation)
            {
                case "+":
                case "-":
                    return 4;
                case "*":
                case "/":
                    return 3;
                case "^":
	                return 2;
                case "(":
                    return 0;
                case ")":
                    return 1;
            }

            return -1;
        }

        public override int GetOperandCount(string operation)
        {
            return 2;
        }

        public override Function<byte> ForName(string name, params Expression<byte>[] args)
        {
            switch (name)
            {
                case "+": return new Sum(args);
                case "-": return new Sub(args);
                case "*": return new Prod(args);
                case "/": return new Divide(args);
                case "^": return new Power(args);
                default:
                    return new NulFunc<byte>(this);
            }
        }

        public byte TryCount(object value,Dictionary<string,byte> variables)
        {
	        var expr = ParseExpressionFully(""+value);
	        var vars = new (string name,byte value)[variables.Count];
	        var i = 0;
	        foreach (var key in variables.Keys)
	        {
		        vars[i]=(key,variables[key]);
		        i++;
	        }

	        return expr.Calculate(vars);
        }
    }
    public class Sum : Function<byte>
	{
		public Sum(params Expression<byte>[] args) : base(EMath, args)
		{
		}

		public override string ToString()
		{
			var ret = this.Arguments[0].ToString();
			if (!(Arguments[0] is Number<byte>) && !(Arguments[0] is Var<byte>))
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

		public override string AsExpression(params (string name, byte value)[] args)
		{
			var ret = this.Arguments[0].AsExpression(args);
			for (var i = 1; i < this.Arguments.Length; i++)
			{
				ret += " + " + this.Arguments[i].AsExpression(args);
			}

			return ret;
		}

		protected override byte Call(params byte[] arguments)
		{
			return (byte) arguments.Sum(a => a);
		}
	}

	public class Power : Function<byte>
	{
		public Power(params Expression<byte>[] args) : base(EMath, args)
		{
		}

		public override string ToString()
		{
			var left = "";
			var right = "";

			if (Arguments[0] is Number<byte> || Arguments[0] is Var<byte>)
			{
				right = Arguments[0].ToString();
			}
			else
			{
				right = "(" + Arguments[0].ToString() + ")";
			}

			if (Arguments[1] is Number<byte> || Arguments[1] is Var<byte>)
			{
				left = Arguments[1].ToString();
			}
			else
			{
				left = "(" + Arguments[1].ToString() + ")";
			}

			return left + " ^ " + right;
		}

		public override string AsExpression(params (string name, byte value)[] args)
		{
			var left = "";
			var right = "";

			if (Arguments[0] is Number<byte> || Arguments[0] is Var<byte>)
			{
				right = Arguments[0].AsExpression(args);
			}
			else
			{
				right = "(" + Arguments[0].AsExpression(args) + ")";
			}

			if (Arguments[1] is Number<byte> || Arguments[1] is Var<byte>)
			{
				left = Arguments[1].AsExpression(args);
			}
			else
			{
				left = "(" + Arguments[1].AsExpression(args) + ") ";
			}

			return left + " ^ " + right;
		}

		protected override byte Call(params byte[] arguments)
		{
			return (byte) Math.Pow(arguments[0], arguments[1]);
		}
	}

	public class Sub : Function<byte>
	{
		public Sub(params Expression<byte>[] args) : base(EMath, args)
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

		public override string AsExpression(params (string name, byte value)[] args)
		{
			var ret = this.Arguments[0].AsExpression(args);
			for (var i = 1; i < this.Arguments.Length; i++)
			{
				ret += " - " + this.Arguments[i].AsExpression(args);
			}

			return ret;
		}

		protected override byte Call(params byte[] arguments)
		{
			var sum = arguments[0];
			for (var i = 1; i < arguments.Length; i++) sum -= arguments[i];

			return sum;
		}
	}

	public class Prod : Function<byte>
	{
		public Prod(params Expression<byte>[] args) : base(EMath, args)
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

		public override string AsExpression(params (string name, byte value)[] args)
		{
			var ret = this.Arguments[0].AsExpression(args);
			for (var i = 1; i < this.Arguments.Length; i++)
			{
				ret += " * " + this.Arguments[i].AsExpression(args);
			}

			return ret;
		}

		protected override byte Call(params byte[] arguments)
		{
			return arguments.Aggregate<byte, byte>(1, (current, t) => (byte)(current * t));
		}
	}

	public class Divide : Function<byte>
	{
		public Divide(params Expression<byte>[] args) : base(EMath, args)
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

		public override string AsExpression(params (string name, byte value)[] args)
		{
			var ret = this.Arguments[0].AsExpression(args);
			for (var i = 1; i < this.Arguments.Length; i++)
			{
				ret += " / " + this.Arguments[i].AsExpression(args);
			}

			return ret;
		}

		protected override byte Call(params byte[] arguments)
		{
			var div = arguments[0];
			for (var i = 1; i < arguments.Length; i++) div /= arguments[i];

			return div;
		}
	}

	public class Mod : Function<byte>
	{
		public Mod(params Expression<byte>[] args) : base(EMath, args)
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

		public override string AsExpression(params (string name, byte value)[] args)
		{
			var ret = this.Arguments[0].AsExpression(args);
			for (var i = 1; i < this.Arguments.Length; i++)
			{
				ret += " % " + this.Arguments[i].AsExpression(args);
			}

			return ret;
		}

		protected override byte Call(params byte[] arguments)
		{
			var div = arguments[0];
			for (var i = 1; i < arguments.Length; i++) div %= arguments[i];

			return div;
		}
	}

	public class Div : Function<byte>
	{
		public Div(params Expression<byte>[] args) : base(EMath, args)
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

		public override string AsExpression(params (string name, byte value)[] args)
		{
			var ret = this.Arguments[0].AsExpression(args);
			for (var i = 1; i < this.Arguments.Length; i++)
			{
				ret += " // " + this.Arguments[i].AsExpression(args);
			}

			return ret;
		}

		protected override byte Call(params byte[] arguments)
		{
			var div = (byte) arguments[0];
			for (var i = 1; i < arguments.Length; i++) div /= (byte) arguments[i];

			return div;
		}
	}
}