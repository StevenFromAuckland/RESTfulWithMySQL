using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HillLabTest.Models;
using HillLabTestEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HillLabTest.Controllers
{
    [Route("api/[Controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly ILogger<ProductsController> _logger;
        private readonly IProductContextWrapper _productContextWrapper;
        private IMapper _mapper;

        public ProductsController(ILogger<ProductsController> logger, IProductContextWrapper productContextWrapper, IMapper mapper)
        {
            _logger = logger;
            _productContextWrapper = productContextWrapper;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var result = await _productContextWrapper.Product.GetAllProducts();
            var mappedResult = _mapper.Map<IEnumerable<ProductDTO>>(result);
            return mappedResult;


        }
        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ProductDTO> GetProduct(int id)
        {
            var result = await _productContextWrapper.Product.GetProductById(id);
            var mappedResult = _mapper.Map<ProductDTO>(result);
            return mappedResult;

        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]ProductDTO productDTO)
        {
            try
            {
                if (productDTO == null)
                {
                    return BadRequest("Product is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model");
                }

                var product = _mapper.Map<Product>(productDTO);

                await _productContextWrapper.Product.CreateProduct(product);
                int result = await _productContextWrapper.Save();

                var newProduct = _mapper.Map<ProductDTO>(product);

                return CreatedAtRoute("GetProductById", new { id = newProduct.ProductId }, newProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody]ProductDTO productDTO)
        {
            try
            {
                if (productDTO == null)
                {
                    return BadRequest("Product is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model");
                }

                var product = _mapper.Map<Product>(productDTO);

                var productDB = await _productContextWrapper.Product.GetProductById(product.ProductId);
                if (productDB == null)
                {
                    return NotFound();
                }

                productDB.ProductName = product.ProductName;
                productDB.Quantity = product.Quantity;
                productDB.Unit = product.Unit;
                productDB.CategoryId = product.CategoryId;

                _productContextWrapper.Product.UpdateProduct(productDB);
                int result = await _productContextWrapper.Save();
                if (result > 0)
                    return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return StatusCode(500, "Internal server error");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _productContextWrapper.Product.GetProductById(id);
                if (product == null)
                    return NotFound();

                _productContextWrapper.Product.DeleteProduct(product);
                int result = await _productContextWrapper.Save();
                if (result > 0)
                    return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return StatusCode(500, "Internal server error");
        }

    }
}
