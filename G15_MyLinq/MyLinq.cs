namespace G15_MyLinq
{
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

            var values = new List<T>();

            foreach (var item in list1)
            {
                yield return item;
            }
            foreach (var item in list2)
            {
                yield return item;
            }
        }

        public static IEnumerable<T> MyUnion<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
        {
            ArgumentNullException.ThrowIfNull(list1, nameof(list1));
            ArgumentNullException.ThrowIfNull(list2, nameof(list2));

            var uniqueValues = new HashSet<T>();

            foreach (var item in list1)
            {
                uniqueValues.Add(item);
            }
            foreach (var item in list2)
            {
                uniqueValues.Add(item);
            }

            return uniqueValues;
        }

        public static IEnumerable<T> MyIntersect<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
        {
            ArgumentNullException.ThrowIfNull(list1, nameof(list1));
            ArgumentNullException.ThrowIfNull(list2, nameof(list2));

            var commonValues = new HashSet<T>(list2);

            foreach (var item in list1)
            {
                if (commonValues.Remove(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> MyExcept<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
        {
            ArgumentNullException.ThrowIfNull(list1, nameof(list1));
            ArgumentNullException.ThrowIfNull(list2, nameof(list2));

            var hashList = new HashSet<T>(list2);
            var values = new HashSet<T>();

            foreach (var item in list1)
            {
                if (!hashList.Contains(item) && values.Add(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> MyDistinct<T>(this IEnumerable<T> source)
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));

            var uniqueValues = new HashSet<T>();

            foreach (var item in source)
            {
                if (uniqueValues.Add(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> MySkip<T>(this IEnumerable<T> source, int count)
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));
            if (count < 0)
                throw new ArgumentOutOfRangeException("Count must be greate than 0");

            int skipped = 0;

            foreach (var item in source)
            {
                if (skipped < count)
                {
                    skipped++;
                    continue;
                }

                yield return item;
            }
        }

        public static IEnumerable<T> MyTake<T>(this IEnumerable<T> source, int count)
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));
            if (count < 0)
                throw new ArgumentOutOfRangeException("Count must be greate than 0");

            int taken = 0;

            foreach (var item in source)
            {
                if (taken >= count)
                {
                    yield break;
                }

                yield return item;
                taken++;
            }
        }

        public static IEnumerable<T> MyTakeWhile<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));
            ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

            foreach (var item in source)
            {
                if (!predicate(item))
                {
                    yield break;
                }

                yield return item;
            }
        }
    }
}
