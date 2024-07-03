using Microsoft.AspNetCore.Mvc;

namespace LinqEnsayo2
{
    public class ProductController : Controller
{
    private static List<Product> _products = new List<Product>
    {
        new Product { ProductId = 1, Name = "Product A", Price = 10.5m, CategoryId = 1 },
        new Product { ProductId = 2, Name = "Product B", Price = 20.0m, CategoryId = 2 },
        new Product { ProductId = 3, Name = "Product C", Price = 30.0m, CategoryId = 1 },
        new Product { ProductId = 4, Name = "Product D", Price = 40.0m, CategoryId = 2 },
    };

    private static List<Category> _categories = new List<Category>
    {
        new Category { CategoryId = 1, CategoryName = "Category 1" },
        new Category { CategoryId = 2, CategoryName = "Category 2" },
    };

    public IActionResult ProductList()
    {

        var sortedProducts = _products
            .OrderBy(p => p.Price)
            .ThenBy(p => p.Name);

        var productNames = _products
            .Select(p => p.Name)
            .ToList();

        var allCategoriesWithProducts = _categories
            .SelectMany(c => c.Products ?? Enumerable.Empty<Product>());

        int productCount = _products.Count();
        long longProductCount = _products.LongCount();

        decimal totalPrice = _products
            .Select(p => p.Price)
            .Aggregate((acc, price) => acc + price);

        bool hasProductA = _products
            .Select(p => p.Name)
            .Contains("Product A");

        bool anyExpensiveProducts = _products
            .Any(p => p.Price > 25);

        var productsByCategory = _products
            .GroupBy(p => p.CategoryId);

        var productWithCategory = _products
            .Join(_categories,
                  p => p.CategoryId,
                  c => c.CategoryId,
                  (p, c) => new { p.Name, c.CategoryName });

        var categoriesWithProducts = _categories
            .GroupJoin(_products,
                       c => c.CategoryId,
                       p => p.CategoryId,
                       (c, ps) => new { c.CategoryName, Products = ps });

        var distinctCategories = _products
            .Select(p => p.CategoryId)
            .Distinct();

        var distinctByCategory = _products
            .DistinctBy(p => p.CategoryId);

        var extraProducts = new List<Product>
        {
            new Product { ProductId = 5, Name = "Product E", Price = 50.0m, CategoryId = 1 }
        };

        var allProducts = _products
            .Union(extraProducts);

        var allProductsByName = _products
            .UnionBy(extraProducts, p => p.Name);

        var concatenatedProducts = _products
            .Concat(extraProducts);

        return View(sortedProducts);
    }
}
}