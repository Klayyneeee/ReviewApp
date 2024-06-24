using Microsoft.EntityFrameworkCore;
using ReviewApp.Application.Dto;
using ReviewApp.Domain.Interface;
using ReviewApp.Domain.Properties.Models;
using ReviewApp.Infrastructure.Data;
using ReviewApp.Domain.Properties.Models;
namespace ReviewApp.Infrastructure.Repository
{
    public class GoodRepository : IGoodRepository
    {
        private readonly DataContext _context;
        public GoodRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateGoods(int categoryId, Goods good)
        {
            var category = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            var goodCategory = new GoodsCategory()
            {
                Category = category,
                Goods = good,
            };
            _context.Add(goodCategory);
            _context.Add(good);
            return Save();
        }

        public bool DeleteGoods(Goods good)
        {
            _context.Remove(good);
            return Save();
        }

        public ICollection<Goods> GetGoods()
        {
            return _context.Goods.OrderBy(p => p.Id).ToList();

        }

        public Goods GetGoods(int id)
        {
            return _context.Goods.Where(p => p.Id == id).FirstOrDefault();
        }

        public Goods GetGoods(string name)
        {
            return _context.Goods.Where(p => p.Name == name).FirstOrDefault();
        }

        public object GetGoodTrimToUpper(GoodDto goodCreate)
        {
            throw new NotImplementedException();
        }

        public bool GoodExists(int GoodId)
        {
            return _context.Goods.Any(p => p.Id == GoodId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateGoods(int categoryId, Goods good)
        {
            _context.Update(good);
            return Save();
        }
    }
}
