namespace ReviewApp.Domain.Properties.Models
{
    public class GoodsCategory
    {
        public int GoodId { get; set; }
        public int CategoryId { get; set; }
        public Goods Goods { get; set; }
        public Category Category { get; set; }
    }
}
