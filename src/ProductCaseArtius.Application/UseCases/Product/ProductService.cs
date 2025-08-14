using ProductCaseArtius.Communication.Requests;
using ProductCaseArtius.Communication.Responses;
using ProductCaseArtius.Domain.Entities;
using ProductCaseArtius.Domain.Repositories;
using ProductEntity = ProductCaseArtius.Domain.Entities.Product;

namespace ProductCaseArtius.UseCases.Product
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ResponseProductJson> AddAsync(RequestProductJson request)
        {
            var product = new ProductEntity
            {
                Name = request.Name,
                Price = request.Price,
                Category = request.Category
            };
            
            var addedProduct = await _productRepository.AddAsync(product);
            
            return new ResponseProductJson
            {
                Id = addedProduct.Id,
                Name = addedProduct.Name,
                Price = addedProduct.Price,
                Category = addedProduct.Category
            };
        }

        public async Task<List<ResponseProductJson>> ListAsync()
        {
            var products = await _productRepository.GetAllAsync();
            
            return products.Select(p => new ResponseProductJson
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Category = p.Category
            }).ToList();
        }

        public async Task<ResponseProductJson?> GetByIdAsync(long id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            
            if (product == null)
                return null;
            
            return new ResponseProductJson
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Category = product.Category
            };
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            
            if (product == null)
                return false;
            
            await _productRepository.DeleteAsync(id);
            return true;
        }
    }
}
