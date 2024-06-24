using System.ComponentModel.DataAnnotations;
namespace ReviewApp.Domain.Properties.Models
{
    public class Goods
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GoodsCategory> GoodsCategories { get; set; }
    }
}
