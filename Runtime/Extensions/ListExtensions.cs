using System.Collections.Generic;

namespace IntegratedTimer
{
    public static class ListExtensions
    {
        public static void RefreshWith<T>(this List<T> source, IEnumerable<T> items)
        {
            source.Clear();
            source.AddRange(items);
        }
    }
}