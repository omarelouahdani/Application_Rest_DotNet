using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CatalogueApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CatalogueApp.Controllers
{
    [Route("/api/products")]
    public class ProductRestController:Controller
    {
        public CatalogueDbRepository CatalogueRepository { get; set; }
        public ProductRestController(CatalogueDbRepository repository){
            this.CatalogueRepository=repository;
        }
        [HttpGet]
        public IEnumerable<Product> listCats(){
            return CatalogueRepository.Products.Include(p=>p.Category);
        }
        [HttpPost]
        public Product save([FromBody]Product product){
            CatalogueRepository.Products.Add(product);
            CatalogueRepository.SaveChanges();
            return product;
        }
        [HttpGet("{Id}")]
        public Product getOne(long Id){
            return CatalogueRepository.Products.Include(p=>p.Category).FirstOrDefault(p=>p.Id==Id);
        }
        [HttpPut("{Id}")]
        public Product update(long Id,[FromBody]Product product){
            product.Id=Id;
            CatalogueRepository.Products.Update(product);
            CatalogueRepository.SaveChanges();
            return product;
        }
        [HttpDelete("{Id}")]
        public void update(long Id){
            Product product=CatalogueRepository.Products.FirstOrDefault(p=>p.Id==Id);
            CatalogueRepository.Remove(product);
            CatalogueRepository.SaveChanges();
        }
        [HttpGet("search")]
        public IEnumerable<Product> search(string q){
            return CatalogueRepository.Products.Include(p=>p.Category).Where(p=>p.Name.Contains(q));
        }
        [HttpGet("pagenate")]
        public IEnumerable<Product> page(int page=0,int size=5){
            int skipValue=(page-1)*size;
            return CatalogueRepository.Products.Include(p=>p.Category).Skip(skipValue).Take(size);
        }
    }
}