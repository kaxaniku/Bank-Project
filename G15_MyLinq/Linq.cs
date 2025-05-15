using System.Linq;

namespace GT_20250512_linq
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var products = ProductProvider.GetProducts();


        }
    }

    public record Product()
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public static class ProductProvider
    {
        public static List<Product> GetProducts()
        {
            return new List<Product>()
       {
           new Product() { Id = 1, Name = "Apple iPhone 14", Price = 999.99m, Quantity = 50 },
           new Product() { Id = 2, Name = "Samsung Galaxy S23", Price = 849.99m, Quantity = 30 },
           new Product() { Id = 3, Name = "Sony WH-1000XM5 Headphones", Price = 399.99m, Quantity = 100 },
           new Product() { Id = 4, Name = "Dell XPS 13 Laptop", Price = 1299.99m, Quantity = 20 },
           new Product() { Id = 5, Name = "Apple MacBook Pro 16", Price = 2499.99m, Quantity = 15 },
           new Product() { Id = 6, Name = "Logitech MX Master 3 Mouse", Price = 99.99m, Quantity = 200 },
           new Product() { Id = 7, Name = "Amazon Echo Dot (5th Gen)", Price = 49.99m, Quantity = 300 },
           new Product() { Id = 8, Name = "Sony PlayStation 5", Price = 499.99m, Quantity = 25 },
           new Product() { Id = 9, Name = "Microsoft Xbox Series X", Price = 499.99m, Quantity = 20 },
           new Product() { Id = 10, Name = "Apple AirPods Pro (2nd Gen)", Price = 249.99m, Quantity = 150 },
           new Product() { Id = 11, Name = "Google Pixel 7", Price = 599.99m, Quantity = 40 },
           new Product() { Id = 12, Name = "HP Spectre x360", Price = 1399.99m, Quantity = 10 },
           new Product() { Id = 13, Name = "Bose QuietComfort 45", Price = 329.99m, Quantity = 80 },
           new Product() { Id = 14, Name = "NVIDIA GeForce RTX 4090", Price = 1599.99m, Quantity = 5 },
           new Product() { Id = 15, Name = "Asus ROG Zephyrus G14", Price = 1499.99m, Quantity = 12 },
           new Product() { Id = 16, Name = "Canon EOS R5 Camera", Price = 3899.99m, Quantity = 8 },
           new Product() { Id = 17, Name = "DJI Mini 3 Pro Drone", Price = 759.99m, Quantity = 18 },
           new Product() { Id = 18, Name = "Fitbit Charge 5", Price = 149.99m, Quantity = 120 },
           new Product() { Id = 19, Name = "GoPro HERO11 Black", Price = 499.99m, Quantity = 25 },
           new Product() { Id = 20, Name = "Razer DeathAdder V3 Pro", Price = 149.99m, Quantity = 90 },
           new Product() { Id = 21, Name = "Samsung 980 Pro SSD 1TB", Price = 129.99m, Quantity = 60 },
           new Product() { Id = 22, Name = "LG OLED C2 55-inch TV", Price = 1299.99m, Quantity = 10 },
           new Product() { Id = 23, Name = "Anker PowerCore 26800mAh", Price = 79.99m, Quantity = 150 },
           new Product() { Id = 24, Name = "HyperX Cloud II Gaming Headset", Price = 99.99m, Quantity = 110 },
           new Product() { Id = 25, Name = "Corsair K95 RGB Platinum Keyboard", Price = 199.99m, Quantity = 70 },
           new Product() { Id = 26, Name = "Sony A7 IV Mirrorless Camera", Price = 2499.99m, Quantity = 6 },
           new Product() { Id = 27, Name = "Microsoft Surface Pro 9", Price = 1099.99m, Quantity = 15 },
           new Product() { Id = 28, Name = "Apple Watch Series 8", Price = 399.99m, Quantity = 100 },
           new Product() { Id = 29, Name = "Samsung Galaxy Watch 5", Price = 279.99m, Quantity = 90 },
           new Product() { Id = 30, Name = "Lenovo ThinkPad X1 Carbon", Price = 1599.99m, Quantity = 10 },
           new Product() { Id = 31, Name = "SteelSeries Arctis Nova Pro", Price = 349.99m, Quantity = 50 },
           new Product() { Id = 32, Name = "Seagate Expansion 5TB HDD", Price = 119.99m, Quantity = 80 },
           new Product() { Id = 33, Name = "Roku Streaming Stick 4K", Price = 49.99m, Quantity = 200 },
           new Product() { Id = 34, Name = "Philips Hue Smart Bulbs (4-pack)", Price = 199.99m, Quantity = 60 },
           new Product() { Id = 35, Name = "JBL Flip 6 Bluetooth Speaker", Price = 129.99m, Quantity = 100 },
           new Product() { Id = 36, Name = "WD My Passport 2TB", Price = 89.99m, Quantity = 150 },
           new Product() { Id = 37, Name = "Apple iPad Pro 12.9-inch", Price = 1099.99m, Quantity = 20 },
           new Product() { Id = 38, Name = "Samsung Galaxy Tab S8", Price = 699.99m, Quantity = 25 },
           new Product() { Id = 39, Name = "Sony Bravia XR 65-inch TV", Price = 1999.99m, Quantity = 8 },
           new Product() { Id = 40, Name = "Amazon Kindle Paperwhite", Price = 139.99m, Quantity = 120 },
           new Product() { Id = 41, Name = "Ring Video Doorbell 4", Price = 199.99m, Quantity = 50 },
           new Product() { Id = 42, Name = "Nest Learning Thermostat", Price = 249.99m, Quantity = 40 },
           new Product() { Id = 43, Name = "Eufy RoboVac 11S", Price = 229.99m, Quantity = 30 },
           new Product() { Id = 44, Name = "Sony WF-1000XM4 Earbuds", Price = 279.99m, Quantity = 90 },
           new Product() { Id = 45, Name = "Garmin Forerunner 955", Price = 499.99m, Quantity = 25 },
           new Product() { Id = 46, Name = "Apple Magic Keyboard", Price = 99.99m, Quantity = 200 },
           new Product() { Id = 47, Name = "Samsung T7 Portable SSD 1TB", Price = 129.99m, Quantity = 70 },
           new Product() { Id = 48, Name = "Sony Alpha ZV-E10 Camera", Price = 799.99m, Quantity = 15 },
           new Product() { Id = 49, Name = "Razer BlackShark V2 Pro", Price = 179.99m, Quantity = 80 },
           new Product() { Id = 50, Name = "Apple HomePod Mini", Price = 99.99m, Quantity = 150 }
       };
        }
    }
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

            List<T> result = new();
            result.AddRange(list1);
            result.AddRange(list2);

            return result;
        }

        public static IEnumerable<T> MyUnion<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
        {
            ArgumentNullException.ThrowIfNull(list1, nameof(list1));
            ArgumentNullException.ThrowIfNull(list2, nameof(list2));

            HashSet<T> result = new HashSet<T>(list1);

            foreach (T item in list2)
            {
                result.Add(item);
            }


            return result;
        }

        public static IEnumerable<T> MyIntersect<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
        {
            ArgumentNullException.ThrowIfNull(list1, nameof(list1));
            ArgumentNullException.ThrowIfNull(list2, nameof(list2));

            HashSet<T> result = new HashSet<T>();
            foreach (T item in list2)
            {
                if (list1.Contains(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public static IEnumerable<T> MyExcept<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
        {
            ArgumentNullException.ThrowIfNull(list1, nameof(list1));
            ArgumentNullException.ThrowIfNull(list2, nameof(list2));

            HashSet<T> result = new HashSet<T>(list1);
            foreach (T item in list2)
            {
                result.Remove(item);
            }

            return result;
        }

        public static IEnumerable<T> MyDistinct<T>(this IEnumerable<T> source)
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));

            HashSet<T> result = new HashSet<T>();

            foreach (T item in source)
            {
                if (result.Add(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> MySkip<T>(this IEnumerable<T> source, int count)
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be non-negative.");
            }

            if (count > source.Count())
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Count can't be bigger than source data");
            }


            foreach (T item in source)
            {
                if (count-- > 0)
                {
                    continue;
                }
                yield return item;
            }
        }

        public static IEnumerable<T> MyTake<T>(this IEnumerable<T> source, int count)
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));

            foreach (T item in source)
            {
                if (count-- == 0)
                {
                    yield break;
                }
                yield return item;
            }
        }

        public static IEnumerable<T> MyTakeWhile<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));
            ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

            foreach (T item in source)
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