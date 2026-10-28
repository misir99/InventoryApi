using InventoryApi.Models;
using InventoryApi.Repositories;
using Microsoft.AspNetCore.Mvc;



namespace InventoryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAll()
        {
            var products = await _repo.GetAllAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            await _repo.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }


    }
}
