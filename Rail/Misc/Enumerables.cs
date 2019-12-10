﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Rail.Misc
{
    public static class Enumerables
    {

		
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach(T i in source)
			{
				action(i);
			}
		}

		public static bool One<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source.Count<TSource>(predicate) == 1;
		}

		//public static void ForEach<T>(this ObservableCollection<T> source, Action<T> action)
		//{
		//	foreach (T i in source)
		//	{
		//		action(i);
		//	}
		//}
	}
}