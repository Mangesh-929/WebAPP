using Microsoft.AspNetCore.Mvc;
using Product.Data.Models.Domain;
using Product.Data.Repository;

namespace Product.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductM product)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(product);
                bool addProductResult = await _productRepo.AddAsync(product);
                if(addProductResult)
                    TempData["msg"] = "Successfully Added";
                else
                    TempData["msg"] = "Could Not Added";


            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could Not Added";
            }
            return RedirectToAction(nameof(Add));
            
        }
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            //if (product == null)
            //    throw NotFound
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductM product)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(product);
                var UpdateResult = await _productRepo.UpdateAsync(product);
                if (UpdateResult)
                    TempData["msg"] = "Updated successfully";
                else
                    TempData["msg"] = "Could Not Updated";
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could Not Updated";
            }
            return View(product);

        }
       

        public async Task<IActionResult> DisplayAll()
        {
            var products = await _productRepo.GetAllAsync();
            return View(products);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _productRepo.DeleteAsync(id);
            return RedirectToAction(nameof(DisplayAll));
        }

        
    }
}
