using Microsoft.EntityFrameworkCore;
using MyFristProject.Data;
using MyFristProject.Dots;
using MyFristProject.Entity;
using MyFristProject.services;

namespace MyFristProject.Services
{
    public class ProdectServices(AppDbContext Context) : IProdect
    {
        public async Task<Prodect> AddProdect(ProdectDto prodect)
        {
            var NewProdect = new Prodect
            {
                Name = prodect.Name,
                Price = prodect.Price,
            };

            await Context.AddAsync(NewProdect);
            await Context.SaveChangesAsync();

            return NewProdect;
        }
        public async Task<Prodect> GetByIdProdects(int id)
        {
            var prodect = await Context.Products.FirstOrDefaultAsync(u => u.Id == id);
            if (prodect is null)
            {
                return null!;
            }
            return prodect!;
        }
        public async Task<ProdectDto> GetByNameProdects(string name)
        {
            var prodect = await Context.Products.FirstOrDefaultAsync(u => u.Name == name);
            if (prodect is null)
            {
                return null!;
            }
            return new ProdectDto
            {
                Name = prodect.Name,
                Price = prodect.Price
                
            };
        }


        public async Task<List<Prodect>> GetProdects()
        {
            return await Context.Products.ToListAsync();
        }

        public async Task<Prodect?> PutProdect(ProdectDto? prodect, int? id)
        {
            var updateProduct = await Context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (updateProduct == null)
            {
                return null;
            }

            updateProduct.Name = prodect!.Name;
            updateProduct.Price = prodect!.Price;

            await Context.SaveChangesAsync();

            return updateProduct;
        }
        public async Task<string?> DeleteProdect(int id)
        {
            var prodect = await Context.Products.FirstOrDefaultAsync(u => u.Id == id);
            if (prodect is null)
            {
                return null;
            }
             Context.Products.Remove(prodect);
             await Context.SaveChangesAsync();
            var musssege= "it's deleted";
            return musssege ;
        }
    }
}