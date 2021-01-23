using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CatalogueApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace CatalogueApp.Controllers
{
    [Route("/api/categories")]
    public class CategoryRestController:Controller
    {
        public CatalogueDbRepository CatalogueRepository { get; set; }
        public CategoryRestController(CatalogueDbRepository repository){
            this.CatalogueRepository=repository;
        }
        [HttpGet]
        public IEnumerable<Category> listCats(){
            return CatalogueRepository.Categories;
        }
        [HttpPost]
        public Category save([FromBody]Category category){
            CatalogueRepository.Categories.Add(category);
            CatalogueRepository.SaveChanges();
            return category;
        }
        [HttpGet("{Id}")]
        public Category getOne(long Id){
            return CatalogueRepository.Categories.FirstOrDefault(c=>c.Id==Id);
        }
        [HttpPut("{Id}")]
        public Category update(long Id,[FromBody]Category category){
            category.Id=Id;
            CatalogueRepository.Categories.Update(category);
            CatalogueRepository.SaveChanges();
            return category;
        }
        [HttpDelete("{Id}")]
        public void update(long Id){
            Category category=CatalogueRepository.Categories.FirstOrDefault(c=>c.Id==Id);
            CatalogueRepository.Remove(category);
            CatalogueRepository.SaveChanges();
        }
        [HttpGet("{Id}/products")]
        public IEnumerable<Product> products(long Id){
            return CatalogueRepository.Categories.Include(c=>c.Products).FirstOrDefault(c=>c.Id==Id).Products;
        }
        [HttpGet("search")]
        public IEnumerable<Category> search(string q){
            return CatalogueRepository.Categories.Include(c=>c.Products).Where(c=>c.Name.Contains(q));
        }
    }
}