using Dapper.Contrib.Extensions;

namespace DapperInfrastructure.Data.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }
    }
}
