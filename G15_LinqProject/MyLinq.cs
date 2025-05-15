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
            if (predicate(item))
            {
                continue;
            }
            return false;
        }

        return true;
    }

    public static IEnumerable<T> MyConcat<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
    {
        ArgumentNullException.ThrowIfNull(list1, nameof(list1));
        ArgumentNullException.ThrowIfNull(list2, nameof(list2));

        List<T> result = new List<T>();

        foreach (T item in list1)
        {
            result.Add(item);
        }

        foreach (T item in list2)
        {
            result.Add(item);
        }

        return result;
    }

    public static IEnumerable<T> MyUnion<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
    {
        ArgumentNullException.ThrowIfNull(list1, nameof(list1));
        ArgumentNullException.ThrowIfNull(list2, nameof(list2));

        List<T> result = new List<T>();
        foreach (T item in list1)
        {
            result.Add(item);
        }

        foreach (T item in list2)
        {
            if(result.Contains(item))
            {
                continue;
            }
            result.Add(item);
        }
        return result;
    }

    public static IEnumerable<T> MyIntersect<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
    {
        ArgumentNullException.ThrowIfNull(list1, nameof(list1));
        ArgumentNullException.ThrowIfNull(list2, nameof(list2));

        List<T> result = new List<T>();

        foreach (T item in list1)
        {
            foreach (T item2 in list2)
            {
                if (item.Equals(item2))
                {
                    result.Add(item);
                    break;
                }
            }
        }
        return result;
    }

    public static IEnumerable<T> MyExcept<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
    {
        ArgumentNullException.ThrowIfNull(list1, nameof(list1));
        ArgumentNullException.ThrowIfNull(list2, nameof(list2));

        List<T> result = new List<T>();

        foreach (T item1 in list1)
        {
            bool found = false;

            foreach (var item2 in list2)
            {
                if (item1!.Equals(item2))
                {
                    found = true;
                    break;
                }
            }
            if (!found) result.Add(item1);
        }

        return result;
    }

    public static IEnumerable<T> MyDistinct<T>(this IEnumerable<T> source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        List<T> result = new List<T>();

        foreach(T item in source)
        {
            if (!result.Contains(item))
            {
                result.Add(item);
            }
        }
        return result;
    }

    public static IEnumerable<T> MySkip<T>(this IEnumerable<T> source, int count)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), "Count cannot be negative.");

        List<T> result = new List<T>();

        int index = 0;

        foreach (T item in source)
        {
            if (index >= count)
            {
                result.Add(item);
            }
            index++;
        }

        return result;
    }

    public static IEnumerable<T> MyTake<T>(this IEnumerable<T> source, int count)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), "Count cannot be negative.");

        List<T> result = new List<T>();

        int index = 0;

        foreach(T item in source)
        {
            if (index < count)
            {
                result.Add(item);
            index++;
            }
        }
        return result;
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
}