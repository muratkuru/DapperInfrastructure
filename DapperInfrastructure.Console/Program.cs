using DapperInfrastructure.Data;
using DapperInfrastructure.Data.Entities;

namespace DapperInfrastructure.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionProvider connectionProvider = new ConnectionProvider("DapperExampleDb");
            GenericRepository<Category> categoryRepository = new GenericRepository<Category>(connectionProvider);
            GenericRepository<Product> productRepository = new GenericRepository<Product>(connectionProvider);

            long addCategory = categoryRepository.Add(new Category
            {
                Name = "Added category"
            });

            bool updateCategory = categoryRepository.Update(new Category
            {
                Id = (int)addCategory,
                Name = "Updated category"
            });

            bool deleteProduct = productRepository.Delete(5);

            var categories = categoryRepository.GetAll();

            foreach (var category in categories)
            {
                System.Console.WriteLine(category.Name);

                var products = productRepository.GetAllByQuery("SELECT * FROM Products WHERE CategoryId = @Id", 
                    new
                    {
                        Id = category.Id
                    });

                foreach (var product in products)
                {
                    System.Console.WriteLine(" -- {0}", product.Name);
                }
            }
        }
    }
}
