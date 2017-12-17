using Dapper.Contrib.Extensions;

namespace DapperInfrastructure.Data.Entities
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
