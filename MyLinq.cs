using System;
using System.Collections.Generic;

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
        ArgumentNullException.ThrowIfNull(list1, nameof(list1));
        ArgumentNullException.ThrowIfNull(list2, nameof(list2));

        HashSet<T> seen = new HashSet<T>();

        foreach (T item in list1)
        {
            if (seen.Add(item))
            {
                yield return item;
            }
        }

        foreach (T item in list2)
        {
            if (seen.Add(item))
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<T> MyIntersect<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
    {
        ArgumentNullException.ThrowIfNull(list1, nameof(list1));
        ArgumentNullException.ThrowIfNull(list2, nameof(list2));

        HashSet<T> set = new HashSet<T>(list2);

        foreach (T item in list1)
        {
            if (set.Remove(item))
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<T> MyDistinct<T>(this IEnumerable<T> source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        HashSet<T> seen = new HashSet<T>();

        foreach (T item in source)
        {
            if (seen.Add(item))
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<T> MySkip<T>(this IEnumerable<T> source, int count)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        int skipped = 0;

        foreach (T item in source)
        {
            if (skipped++ >= count)
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<T> MyTake<T>(this IEnumerable<T> source, int count)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        int taken = 0;

        foreach (T item in source)
        {
            if (taken++ < count)
            {
                yield return item;
            }
            else
            {
                yield break;
            }
        }
    }

    public static IEnumerable<T> MyTakeWhile<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

        foreach (T item in source)
        {
            if (predicate(item))
            {
                yield return item;
            }
            else
            {
                yield break;
            }
        }
    }

    public static IEnumerable<T> MyExcept<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
    {
        ArgumentNullException.ThrowIfNull(list1, nameof(list1));
        ArgumentNullException.ThrowIfNull(list2, nameof(list2));

        HashSet<T> set = new HashSet<T>(list2);

        foreach (T item in list1)
        {
            if (set.Add(item))
            {
                yield return item;
            }
        }
    }
}
