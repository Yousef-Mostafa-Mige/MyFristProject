using Microsoft.AspNetCore.Mvc;
using MyFristProject.Dots;
using MyFristProject.Entity;
using MyFristProject.services;

namespace MyFristProject.controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class Prodectcontroller(IProdect Product) : ControllerBase
    {
        [HttpPost("addprodect")]
        public async Task<ActionResult<Prodect>> AddProduct(ProdectDto prodect)
        {
            var Getmethode = await Product.AddProdect(prodect);
            if (Getmethode is null)
            {
                return BadRequest("enter your prodect");
            };
            return Ok(Getmethode);
        }        
        [HttpGet("allprodects")]
        public async Task<ActionResult<List<Prodect>>> GetProduct()
        {
            var Getmethode = await Product.GetProdects();
            if (Getmethode is null)
            {
                return BadRequest("no prodects here");
            };
            return Ok(Getmethode);
        }        
        [HttpGet("getbyid/{id}")]
        public async Task<ActionResult<Prodect>> Getbyid(int id)
        {
            var Getmethode = await Product.GetByIdProdects(id);
            if (Getmethode is null)
            {
                return NotFound("Product not found");
            }
            return Ok(Getmethode);
        }  
        [HttpPut("ubdate/{id}")]
        public async Task<ActionResult<Prodect>> put(ProdectDto prodectdo,int id)
        {
            var Getmethode = await Product.PutProdect(prodectdo,id);
            if (Getmethode is null)
            {
                return NotFound("Product not found");
            }
            return Ok(Getmethode);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Prodect>> delete(int id)
        {
            var Getmethode = await Product.DeleteProdect(id);
            if (Getmethode is null)
            {
                return NotFound("Product not found");
            }
            return Ok(Getmethode);
        }  
       
    }
    
}