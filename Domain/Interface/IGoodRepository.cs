using ReviewApp.Application.Dto;
using ReviewApp.Domain.Properties.Models;

namespace ReviewApp.Domain.Interface
{
    public interface IGoodRepository
    {
        ICollection<Goods> GetGoods();
        Goods GetGoods(int id);
        Goods GetGoods(string name);
        bool GoodExists(int GoodId);
        bool CreateGoods(int categoryId, Goods good);
        bool UpdateGoods(int categoryId, Goods good);
        bool DeleteGoods(Goods good);
        bool Save();
        object GetGoodTrimToUpper(GoodDto goodCreate);
    }
}
