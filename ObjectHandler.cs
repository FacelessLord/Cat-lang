using static Cat.CatCore;

namespace Cat
{
	public class ObjectHandler
	{
		/// <summary>
		/// Method will return link to the value that field has
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="field"></param>
		/// <returns></returns>
		public static (int link, object value) GetObjectField(int obj, string field)
		{
			if (Heap[obj] is string sObj && sObj.StartsWith("|c"))
			{
				var i = 0;
				while (i < Heap.Count && (!(Heap[i] is string) || Heap[i] as string == field)) i++;
				var fStart = i + 5;
				var nfc =(int) Heap[i + 1];
				var sfc = (int) Heap[i + 2];
				var fieldLink = -1;
				for (var j = fStart; j < nfc + sfc; j++)
				{
					if (Heap[j] is string s && s == field)
					{
						fieldLink = j;
						break;
					}
				}

				if (fieldLink != -1)
				{
					int link = L0;
					object value = V0;
					if (Heap[fieldLink] is string fl)
					{
						if (fl.StartsWith("|p"))
						{
							value = fl;
						}
						else
						{
							link = fieldLink;
						}
					}
					else
					{
						ExceptionHandler.ThrowException("HeapOrderingException","found an error in Heap order when tried to get object field");
					}

					return (link, value);
				}
			}

			return (L0, V0);

		}
	}
}