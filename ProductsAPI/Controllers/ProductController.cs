using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ProductsAPI.Models
{

    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {

        readonly private ApplicationContext _context;

        public ProductController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _context.Products.ToList();
            if (products.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(products);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                Product product = new Product();
                product = _context.Products.SingleOrDefault(p => p.Id == id);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok($"Product Found! \nProduct:{product.Name}\nPrice: R${product.Price}\nId:{product.Id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return Ok($"Product Created! \nProduct:{product.Name}\nPrice: R${product.Price}\nId:{product.Id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            var searched_product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (searched_product != null)
            {
                try
                {
                    _context.Products.Remove(searched_product);
                    _context.SaveChanges();
                    return Ok($"Product Deleted successfully! \nProduct: {searched_product.Name} | Id: {searched_product.Id}");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public ActionResult PutProduct(Product product, Guid id)
        {
            var updated_product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (updated_product == null)
            {
                return NotFound();
            }
            else
            {
                updated_product.Name = product.Name;
                updated_product.Price = product.Price;
             

                try
                {
                    _context.SaveChanges();
                    return Ok($"Product Updated! \nProduct:{updated_product.Name}\nPrice: R${updated_product.Price}\nId:{updated_product.Id}");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }

        [HttpPatch("{id}")]
        public IActionResult PatchProduct(Product product, Guid id)
        {
            var updated_product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (updated_product == null)
            {
                return NotFound();
            }
            else
            {
                if(product.Name != null)
                {
                    updated_product.Name = product.Name;
                }

                if(product.Price > 0)
                {
                    updated_product.Price = product.Price;
                }

                try
                {
                    _context.SaveChanges();
                    return Ok($"Product Updated! \nProduct:{updated_product.Name}\nPrice: R${updated_product.Price}\nId:{updated_product.Id}");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }



    }
}
