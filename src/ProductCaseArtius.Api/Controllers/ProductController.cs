using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProductCaseArtius.Communication.Requests;
using ProductCaseArtius.Communication.Responses;
using System.Threading.Tasks;
using ProductService = ProductCaseArtius.UseCases.Product.ProductService;

namespace ProductCaseArtius.Api.Controllers
{
    [ApiController]
    [Route("produto")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;
        public ProductController(ProductService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize] // Requer autenticação para criar produtos
        public async Task<IActionResult> Post([FromBody] RequestProductJson request)
        {
            var result = await _service.AddAsync(request);
            return Created($"/produto/{result.Id}", result);
        }

        [HttpGet]
        [AllowAnonymous] // Permite acesso sem autenticação para listar produtos
        public async Task<IActionResult> Get()
        {
            var products = await _service.ListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        [AllowAnonymous] // Permite acesso sem autenticação para buscar produto por ID
        [ProducesResponseType(typeof(ResponseProductJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var product = await _service.GetByIdAsync(id);
            
            if (product == null)
                return NotFound($"Produto com ID {id} não foi encontrado.");
            
            return Ok(product);
        }

        [HttpDelete("{id}")]
        [Authorize] // Requer autenticação para deletar produtos
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _service.DeleteAsync(id);
            
            if (!deleted)
                return NotFound($"Produto com ID {id} não foi encontrado.");
            
            return NoContent(); // Status 204 - Deletado com sucesso
        }
    }
}
