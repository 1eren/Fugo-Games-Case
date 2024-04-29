using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public static class ListExtentions 
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    // Method to sort the elements of a list in descending order
    public static void SortDescending<T>(this IList<T> list) where T : IComparable<T>
    {
        var comparer = Comparer<T>.Default;
        list.Sort((x, y) => comparer.Compare(y, x));
    }

    // Method to sort the elements of a list in ascending order
    public static void SortAscending<T>(this IList<T> list) where T : IComparable<T>
    {
        var comparer = Comparer<T>.Default;
        list.Sort((x, y) => comparer.Compare(x, y));
    }

    // Method to sort the elements of a list based on a specific criterion
    public static void SortBy<T, TKey>(this IList<T> list, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
    {
        list.Sort((x, y) => keySelector(x).CompareTo(keySelector(y)));
    }
}
