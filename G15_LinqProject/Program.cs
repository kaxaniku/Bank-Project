namespace G15_LinqProject;
internal class Program
{
    static void Main(string[] args)
    {
        //var products = ProductProvider.GetProducts();

        //int pageSize = 20;
        //int pageNumber = 1;
        //int totalProducts = products.Count();
        //int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

        //while (pageNumber <= totalPages)
        //{
        //    var paginatedProducts = products
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize);
        //    Console.WriteLine($"Page {pageNumber} of {totalPages}:");
        //    foreach (var product in paginatedProducts)
        //    {
        //        Console.WriteLine(product);
        //    }
        //    pageNumber++;

        //    Console.WriteLine("Press any key to continue to the next page...");
        //    Console.ReadKey(true);
        //    Console.Clear();
        //}

        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
        List<int> numbers2 = new List<int> { 4, 5, 6, 7, 8 };

        numbers.MyTakeWhile(x => x != 4).ToList().ForEach(Console.WriteLine);
    }
}
