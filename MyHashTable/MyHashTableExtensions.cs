using ChallengeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHashTable
{
    public delegate bool FilterDelegate<T>(T element);
    public delegate TResult AggregateDelegate<T, TResult>(TResult accumulator, T element);
    public delegate int CompareDelegate<T>(T x, T y);

    public static class MyHashTableExtensions
    {
        public static IEnumerable<T> Where<T>(this MyHashTable<T> hashTable, FilterDelegate<T> filter) where T : class, IComparable<T>, IComparer<T>, IInit, ICloneable, new()
        {
            foreach (var element in hashTable)
            {
                if (filter(element))
                {
                    yield return element;
                }
            }
        }

        public static TResult Aggregate<T, TResult>(this MyHashTable<T> hashTable, TResult seed, AggregateDelegate<T, TResult> func) where T : class, IComparable<T>, IComparer<T>, IInit, ICloneable, new()
        {
            TResult result = seed;
            foreach (var element in hashTable)
            {
                result = func(result, element);
            }
            return result;
        }

        public static IEnumerable<T> OrderBy<T>(this MyHashTable<T> hashTable, CompareDelegate<T> comparer) where T : class, IComparable<T>, IComparer<T>, IInit, ICloneable, new()
        {
            var sortedList = hashTable.ToList();
            sortedList.Sort(new Comparison<T>(comparer));
            return sortedList;
        }
    }
}
