using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Models.ViewModels;
namespace SportStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository _repository;
        public int PageSize = 4; // Specifying max items per page 
        public HomeController(IStoreRepository repository)
        {
            _repository = repository;
        }
        public ViewResult Index(string? category, int productPage = 1)
        => View(new ProductsListViewModel 
        {
        Products = _repository.Products
            .Where(p => category == null || p.Category == category)
            .OrderBy(p => p.ProductId)
            .Skip((productPage - 1) * PageSize)
            .Take(PageSize),

        PagingInfo = new PagingInfo 
        {
            CurrentPage = productPage,
            ItemsPerPage = PageSize,
            TotalItems = category == null ?  _repository.Products.Count() 
                                              : _repository.Products.Where(e => e.Category == category).Count()
        },

        CurrentCategory = category
        });
    }
}
