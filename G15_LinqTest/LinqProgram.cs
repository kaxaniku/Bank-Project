using G15_LinqProject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace G15_Linq
{
    internal class LinqProgram
    {
        private static void Main(string[] args)
        {
            var numbers = new[] { 1, 2, 3, 4, 5, 6 };
            var evens = numbers.MyWhere(n => n % 2 == 0);
        }

        public static IEnumerable<T> Paginate<T>(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }

    public static class ProductProvider
    {
        public static List<string> GetProducts()
        {
            return Enumerable.Range(1, 100).Select(i => $"Product {i}").ToList();
        }
    }
}
