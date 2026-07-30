using MyFristProject.Dots;
using MyFristProject.Entity;
namespace MyFristProject.services
{
    public interface IProdect
    {
        Task<Prodect> AddProdect(ProdectDto prodect);
        Task<List<Prodect>> GetProdects();
        Task<Prodect> GetByIdProdects(int id);
        Task<Prodect?> PutProdect(ProdectDto? prodect, int? id);
        Task<string?> DeleteProdect(int id);
    }
}