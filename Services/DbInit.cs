using CatalogueApp.Models;
namespace CatalogueApp.Services
{
    public static class DbInit
    {
        public static void initData(CatalogueDbRepository CatalogueDb){
            CatalogueDb.Categories.Add(new Category{Name="pc portable"});
            CatalogueDb.Categories.Add(new Category{Name="Tablette"});
            CatalogueDb.Categories.Add(new Category{Name="pc bureau"});
            CatalogueDb.Products.Add(new Product{Name="Dell",Price=1000,CategoryID=1});
            CatalogueDb.Products.Add(new Product{Name="HP",Price=800,CategoryID=1});
            CatalogueDb.Products.Add(new Product{Name="Appel",Price=673,CategoryID=2});
          
            CatalogueDb.SaveChanges();
        }
    }
}