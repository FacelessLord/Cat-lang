using System;

namespace Cat.Calculators
{
	public abstract class Function<T> : Expression<T>
	{

		protected readonly Expression<T>[] Arguments;

		protected Function(EMath<T> math, params Expression<T>[] args) : base(math)
		{
			Arguments = args;
			try
			{
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		public override T Calculate(params (string name, T value)[] args)
		{
			var values = new T[Arguments.Length];
			for (var i = 0; i < Arguments.Length; i++) values[i] = Arguments[i].Calculate(args);
			return Call(values);
		}

		protected abstract T Call(params T[] arguments);

		public static Function<T> ForName(EMath<T> math, string name, params Expression<T>[] args)
		{
			return null;
		}
	}

	public class NulFunc<T> : Function<T>
	{
		public NulFunc(EMath<T> math) : base(math)
		{
			Console.WriteLine("Something Wrong!");
		}

		public override string ToString()
		{
			return "";
		}

		public override string AsExpression(params (string name, T value)[] args)
		{
			return "";
		}

		protected override T Call(params T[] arguments)
		{
			return _math._zero;
		}
	}

	public class Number<T> : Expression<T>
	{
		private readonly T _value;

		public Number(EMath<T> math, T value) : base(math)
		{
			this._value = value;
		}

		public override string ToString()
		{
			return "" + this._value;
		}

		public override string AsExpression(params (string name, T value)[] args)
		{
			return ToString();
		}

		public override T Calculate(params (string name, T value)[] args)
		{
			return _value;
		}
	}

	public class Var<T> : Expression<T>
	{
		private readonly string _name;

		public Var(EMath<T> math, string name) : base(math)
		{
			this._name = name;
		}

		public override string ToString()
		{
			return this._name;
		}

		public override string AsExpression(params (string name, T value)[] args)
		{
			return this.Calculate(args) + "";
		}

		public override T Calculate(params (string name, T value)[] args)
		{
			for (var i = 0; i < args.Length; i++)
				if (args[i].name == _name)
				{
					return args[i].value;
				}

			return _math._zero;
		}
	}
}