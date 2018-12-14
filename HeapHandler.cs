using System;
using static Cat.CatCore;
using System.Collections.Generic;
namespace Cat
{
	public static class HeapHandler
	{
		/// <summary>
		/// Causes Heap to remove spacings what results in better memory performance
		/// </summary>
		public static void CallHeapDefragmentation()
		{
			var zeroStart = -1;

			for (var i = 0; i < Heap.Count; i++)
			{
				if (Heap[i] is string sh && sh == H0)
				{
					if (zeroStart == -1)
						zeroStart = i;
				}
				else
				{
					if (zeroStart != -1)
					{
						Heap.RemoveRange(zeroStart, i - zeroStart);
						i = zeroStart;
						zeroStart = -1;
					}
				}
			}

			if (zeroStart != -1)
			{
				Heap.RemoveRange(zeroStart, Heap.Count - zeroStart);
			}
		}

		public static int LoadListToHeap(List<object> list)
		{
			if (DoesHeapContainSpaces)
			{
				var bestZeroInARow = 0;
				var bestZeroIndex = 0;
				var zeroInARow = 0;
				var zeroIndex = 0;
				for (var i = 0; i < Heap.Count; i++)
				{
					if (Heap[i] is string k && k == H0)
					{
						if (zeroInARow == 0)
							zeroIndex = i;
						zeroInARow += 1;
						if (bestZeroInARow < zeroInARow)
						{
							bestZeroIndex = zeroIndex;
							bestZeroInARow = zeroInARow;
						}
					}
					else
					{
						zeroInARow = 0;
						zeroIndex = 0;
					}
				}

				if (bestZeroInARow >= list.Count)
				{
					for (var i = 0; i < list.Count; i++)
						Heap[bestZeroIndex + i] = list[i];
					return bestZeroIndex;
				}
			}
			var ret = Heap.Count;
			Heap.AddRange(list);
			return ret;
		}
		
		public static int LoadListToHeap(LinkedList<object> list)
		{
			if (DoesHeapContainSpaces)
			{
				var bestZeroInARow = 0;
				var bestZeroIndex = 0;
				var zeroInARow = 0;
				var zeroIndex = 0;
				for (var i = 0; i < Heap.Count; i++)
				{
					if (Heap[i] is string k && k == H0)
					{
						if (zeroInARow == 0)
							zeroIndex = i;
						zeroInARow += 1;
						if (bestZeroInARow < zeroInARow)
						{
							bestZeroIndex = zeroIndex;
							bestZeroInARow = zeroInARow;
						}
					}
					else
					{
						zeroInARow = 0;
						zeroIndex = 0;
					}
				}

				if (bestZeroInARow >= list.Count)
				{
					var i = 0;
					var en = list.GetEnumerator();
					while (en.MoveNext())
					{
						Heap[bestZeroIndex + i] = en.Current;
						i++;
					}

					en.Dispose();
					return bestZeroIndex;
				}
			}
			var ret = Heap.Count;
			Heap.AddRange(list);
			return ret;
		}
	}
}