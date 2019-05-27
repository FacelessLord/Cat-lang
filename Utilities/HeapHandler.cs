using Cat.AbstractStructure;

namespace Cat.Utilities
{
	public static class HeapHandler
	{
		/// <summary>
		/// Causes Heap to remove spacings what results in better memory performance
		/// </summary>
		public static void CallHeapDefragmentation()
		{
			var zeroStart = -1;

			for (var i = 0; i < CatCore.Heap.Count; i++)
			{
				if (CatCore.Heap[i] is null)
				{
					if (zeroStart == -1)
						zeroStart = i;
				}
				else
				{
					if (zeroStart != -1)
					{
						CatCore.Heap.RemoveRange(zeroStart, i - zeroStart);
						i = zeroStart;
						zeroStart = -1;
					}
				}
			}

			if (zeroStart != -1)
			{
				CatCore.Heap.RemoveRange(zeroStart, CatCore.Heap.Count - zeroStart);
			}
		}

		public static int LoadObjectToHeap(CatStructureObject obj)
		{
			if (CatCore.DoesHeapContainSpaces)
			{
				var bestZeroInARow = 0;
				var bestZeroIndex = 0;
				var zeroInARow = 0;
				var zeroIndex = 0;
				for (var i = 0; i < CatCore.Heap.Count; i++)
				{
					if (CatCore.Heap[i] is null)
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

				if (bestZeroInARow >= 1)
				{
					CatCore.Heap[bestZeroIndex] = obj;
					return bestZeroIndex;
				}
			}
			var ret = CatCore.Heap.Count;
			CatCore.Heap.Add(obj);
			return ret;
		}
	}
}