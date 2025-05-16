using System;

namespace G15_LinqProject;

static class MyLinq
{
    public static IEnumerable<T> MyWhere<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

        foreach (T item in source)
        {
            if (predicate(item))
            {
                yield return item;
            }
        }
    }

    public static bool MyAny<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

        foreach (T item in source)
        {
            if (predicate(item))
            {
                return true;
            }
        }

        return false;
    }

    public static bool MyAll<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

        foreach (T item in source)
        {
            if (!predicate(item))
            {
                return false;
            }
        }

        return true;
    }

    public static IEnumerable<T> MyConcat<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
    {
        ArgumentNullException.ThrowIfNull(list1, nameof(list1));
        ArgumentNullException.ThrowIfNull(list2, nameof(list2));

        foreach (T item in list1)
        {
            yield return item;
        }

        foreach (T item in list2)
        {
            yield return item;
        }
    }

    public static IEnumerable<T> MyUnion<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
    {
        return list1.MyConcat(list2).MyDistinct();
    }

    public static IEnumerable<T> MyExcept<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
    {
        ArgumentNullException.ThrowIfNull(list1, nameof(list1));
        ArgumentNullException.ThrowIfNull(list2, nameof(list2));

        foreach (T item1 in list1)
        {
            if (list2.MyAll(x => !x!.Equals(item1))) yield return item1;
        }
    }

    public static IEnumerable<T> MyIntersect<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
    {
        ArgumentNullException.ThrowIfNull(list1, nameof(list1));
        ArgumentNullException.ThrowIfNull(list2, nameof(list2));

        foreach (T item1 in list1)
        {
            if (list2.MyAny(x => x!.Equals(item1))) yield return item1;
        }
    }

    public static IEnumerable<T> MyDistinct<T>(this IEnumerable<T> source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        return new HashSet<T>(source);
    }

    public static IEnumerable<T> MySkip<T>(this IEnumerable<T> source, int count)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        if (count <= 0)
        {
            foreach (var item in source) yield return item;
            yield break;
        }

        int i = 0;
        foreach (var item in source)
        {
            if (count <= i) yield return item;
            i++;
        }
    }

    public static IEnumerable<T> MyTake<T>(this IEnumerable<T> source, int count)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        if (count <= 0) yield break;

        int i = 0;
        foreach (var item in source)
        {
            if (count > i) yield return item;
            else break;
            i++;
        }
    }

    public static IEnumerable<T> MyTakeWhile<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

        foreach (var item in source) if (predicate(item)) yield return item; 
            else break;
    }
}