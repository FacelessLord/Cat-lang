using System;

namespace Cat.Utilities
{
	public static class ExceptionHandler
	{
		
		/// <summary>
		/// Method will create internal Cat exception
		/// </summary>
		/// <param name="exception"> Exception name</param>
		/// <param name="message"> Exception Message</param>
		public static void ThrowException(string exception,string message)
		{
			//todo
			Console.WriteLine(exception + " occured when "+message);
		}
	}
}