using System;
using System.Collections.Generic;
using System.Linq;

namespace G15_Linq
{
    internal class LinqProgram
    {
        static void Main(string[] args)
        {
            // Fetch the list of products
            var products = ProductProvider.GetProducts();

            // Define pagination settings
            int pageSize = 20; // Number of items per page
            int pageNumber = 1; // Start from the first page
            int totalProducts = products.Count();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            // Display paginated products
            while (pageNumber <= totalPages)
            {
                // Use LINQ to paginate the products
                var paginatedProducts = Paginate(products, pageNumber, pageSize);

                Console.WriteLine($"Page {pageNumber} of {totalPages}:");
                foreach (var product in paginatedProducts)
                {
                    Console.WriteLine(product);
                }

                // Prompt user to continue or exit
                Console.WriteLine("Press any key to continue to the next page, or 'q' to quit...");
                var key = Console.ReadKey(true);
                if (key.KeyChar == 'q' || key.KeyChar == 'Q') break;

                pageNumber++;
                Console.Clear();
            }
        }

        /// <summary>
        /// Paginates a collection using LINQ.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="source">The collection to paginate.</param>
        /// <param name="pageNumber">The current page number (1-based).</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A collection of items for the specified page.</returns>
        private static IEnumerable<T> Paginate<T>(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            return source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }
    }

    // Mock ProductProvider for demonstration purposes
    public static class ProductProvider
    {
        public static List<string> GetProducts()
        {
            // Simulate a list of 100 products
            return Enumerable.Range(1, 100).Select(i => $"Product {i}").ToList();
        }
    }
}
