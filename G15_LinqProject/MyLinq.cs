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
        throw new NotImplementedException();
    }

    public static bool MyAll<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<T> MyConcat<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<T> MyUnion<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<T> MyIntersect<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<T> MyExcept<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<T> MyDistinct<T>(this IEnumerable<T> source)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<T> MySkip<T>(this IEnumerable<T> source, int count)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<T> MyTake<T>(this IEnumerable<T> source, int count)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<T> MyTakeWhile<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        throw new NotImplementedException();
    }
}