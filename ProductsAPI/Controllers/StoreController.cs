using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class StoreController : Controller
    {
        readonly private ApplicationContext _context;

        public StoreController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult getAllStores()
        {
            try
            {
                List<Store> stores = _context.Stores.ToList();

                if(stores.Count  == 0)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(stores);
                }
            } 
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult getStore(Guid id)
        {
            try
            {
                var store = _context.Stores.SingleOrDefault(s => s.Id == id);
                if(store == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok($"Store found! \nStore Name: {store.Name}\nAddress: {store.Address}\nId: {store.Id}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CreateStore(Store store)
        {
            try
            {
                _context.Stores.Add(store);
                _context.SaveChanges();
                return Ok($"Store Created! \nStore Name: {store.Name}\nAddress: {store.Address}\nId: {store.Id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutStore(Store store, Guid id)
        { 
            var updated_store = _context.Stores.SingleOrDefault(s => s.Id == id);

            if(updated_store == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    updated_store.Name = store.Name;
                    updated_store.Address = store.Address;
                    _context.SaveChanges();
                    return Ok($"Store Updated! \nStore Name: {store.Name}\nAddress: {store.Address}\nId: {store.Id}");
                }
                catch(Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }


        [HttpPatch("{id}")]
        public IActionResult PatchStore(Store store, Guid id)
        {
            var updated_store = _context.Stores.SingleOrDefault(s => s.Id == id);
            if( updated_store == null)
            {
                return NotFound();
            }
            else
            {
                if(store.Name != null)
                {
                    updated_store.Name = store.Name;
                }

                if(store.Address != null)
                {
                    updated_store.Address = store.Address;
                }
                _context.SaveChanges();

                return Ok($"Store Updated! \nStore Name: {store.Name}\nAddress: {store.Address}\nId: {store.Id}");
            }
        }
    }
}
