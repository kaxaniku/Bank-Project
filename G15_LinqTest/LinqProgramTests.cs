using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace G15_Linq.Tests
{
    [TestFixture]
    public class LinqProgramTests
    {
        [Test]
        public void Paginate_ShouldReturnCorrectPage()
        {
            // Arrange
            var products = Enumerable.Range(1, 50).Select(i => $"Product {i}").ToList();
            int pageNumber = 2;
            int pageSize = 10;

            // Act
            var result = LinqProgram.Paginate(products, pageNumber, pageSize).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(10));
            Assert.That(result.First(), Is.EqualTo("Product 11"));
            Assert.That(result.Last(), Is.EqualTo("Product 20"));
        }

        [Test]
        public void Paginate_ShouldReturnEmpty_WhenPageExceedsTotalPages()
        {
            // Arrange
            var products = Enumerable.Range(1, 10).Select(i => $"Product {i}").ToList();
            int pageNumber = 5;
            int pageSize = 10;

            // Act
            var result = LinqProgram.Paginate(products, pageNumber, pageSize).ToList();

            // Assert
            Assert.That(result, Is.Empty);
        }
    }
}
